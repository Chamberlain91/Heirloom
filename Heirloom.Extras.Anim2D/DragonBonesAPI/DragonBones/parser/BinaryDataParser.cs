/**
 * The MIT License (MIT)
 *
 * Copyright (c) 2012-2017 DragonBones team and other contributors
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
 * the Software, and to permit persons to whom the Software is furnished to do so,
 * subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace DragonBones
{
    /// <internal/>
    /// <private/>
    internal class BinaryDataParser : ObjectDataParser
    {
        private int _binaryOffset;
        private byte[] _binary;
        private short[] _intArrayBuffer;
        private float[] _floatArrayBuffer;
        private short[] _frameIntArrayBuffer;
        private float[] _frameFloatArrayBuffer;
        private short[] _frameArrayBuffer;
        private ushort[] _timelineArrayBuffer;

        private TimelineData _ParseBinaryTimeline(TimelineType type, uint offset, TimelineData timelineData = null)
        {
            var timeline = timelineData ?? BaseObject.BorrowObject<TimelineData>();
            timeline.type = type;
            timeline.offset = offset;

            _timeline = timeline;

            var keyFrameCount = _timelineArrayBuffer[timeline.offset + (int) BinaryOffset.TimelineKeyFrameCount];

            if (keyFrameCount == 1)
            {
                timeline.frameIndicesOffset = -1;
            }
            else
            {
                // One more frame than animation.
                var totalFrameCount = _animation.frameCount + 1;
                var frameIndices = _data.frameIndices;

                timeline.frameIndicesOffset = frameIndices.Count;
                frameIndices.ResizeList(frameIndices.Count + (int) totalFrameCount);

                for (int i = 0, iK = 0, frameStart = 0, frameCount = 0; i < totalFrameCount; ++i)
                {
                    if (frameStart + frameCount <= i && iK < keyFrameCount)
                    {
                        frameStart = _frameArrayBuffer[_animation.frameOffset + _timelineArrayBuffer[timeline.offset + (int) BinaryOffset.TimelineFrameOffset + iK]];
                        if (iK == keyFrameCount - 1)
                        {
                            frameCount = (int) _animation.frameCount - frameStart;
                        }
                        else
                        {
                            frameCount = _frameArrayBuffer[_animation.frameOffset + _timelineArrayBuffer[timeline.offset + (int) BinaryOffset.TimelineFrameOffset + iK + 1]] - frameStart;
                        }

                        iK++;
                    }

                    frameIndices[timeline.frameIndicesOffset + i] = (uint) (iK - 1);
                }
            }

            _timeline = null; //

            return timeline;
        }

        private void _ParseVertices(Dictionary<string, object> rawData, VerticesData vertices)
        {
            vertices.offset = int.Parse(rawData[DataParser.OFFSET].ToString());

            var weightOffset = _intArrayBuffer[vertices.offset + (int) BinaryOffset.MeshWeightOffset];
            if (weightOffset >= 0)
            {
                var weight = BaseObject.BorrowObject<WeightData>();
                var vertexCount = _intArrayBuffer[vertices.offset + (int) BinaryOffset.MeshVertexCount];
                var boneCount = _intArrayBuffer[weightOffset + (int) BinaryOffset.WeigthBoneCount];
                weight.offset = weightOffset;

                for (var i = 0; i < boneCount; ++i)
                {
                    var boneIndex = _intArrayBuffer[weightOffset + (int) BinaryOffset.WeigthBoneIndices + i];
                    weight.AddBone(_rawBones[boneIndex]);
                }

                var boneIndicesOffset = weightOffset + (short) BinaryOffset.WeigthBoneIndices + boneCount;
                var weightCount = 0;
                for (int i = 0, l = vertexCount; i < l; ++i)
                {
                    var vertexBoneCount = _intArrayBuffer[boneIndicesOffset++];
                    weightCount += vertexBoneCount;
                    boneIndicesOffset += vertexBoneCount;
                }

                weight.count = weightCount;
                vertices.weight = weight;
            }
        }

        protected override void _ParseMesh(Dictionary<string, object> rawData, MeshDisplayData mesh)
        {
            _ParseVertices(rawData, mesh.vertices);
        }
        protected override AnimationData _ParseAnimation(Dictionary<string, object> rawData)
        {
            var animation = BaseObject.BorrowObject<AnimationData>();
            animation.frameCount = (uint) Math.Max(ObjectDataParser._GetNumber(rawData, DataParser.DURATION, 1), 1);
            animation.playTimes = (uint) ObjectDataParser._GetNumber(rawData, DataParser.PLAY_TIMES, 1);
            animation.duration = (float) animation.frameCount / (float) _armature.frameRate;//Must float
            animation.fadeInTime = ObjectDataParser._GetNumber(rawData, DataParser.FADE_IN_TIME, 0.0f);
            animation.scale = ObjectDataParser._GetNumber(rawData, DataParser.SCALE, 1.0f);
            animation.name = ObjectDataParser._GetString(rawData, DataParser.NAME, DataParser.DEFAULT_NAME);
            if (animation.name.Length == 0)
            {
                animation.name = DataParser.DEFAULT_NAME;
            }

            // Offsets.
            var offsets = rawData[DataParser.OFFSET] as List<object>;
            animation.frameIntOffset = uint.Parse(offsets[0].ToString());
            animation.frameFloatOffset = uint.Parse(offsets[1].ToString());
            animation.frameOffset = uint.Parse(offsets[2].ToString());

            _animation = animation;

            if (rawData.ContainsKey(DataParser.ACTION))
            {
                animation.actionTimeline = _ParseBinaryTimeline(TimelineType.Action, uint.Parse(rawData[DataParser.ACTION].ToString()));
            }

            if (rawData.ContainsKey(DataParser.Z_ORDER))
            {
                animation.zOrderTimeline = _ParseBinaryTimeline(TimelineType.ZOrder, uint.Parse(rawData[DataParser.Z_ORDER].ToString()));
            }

            if (rawData.ContainsKey(DataParser.BONE))
            {
                var rawTimeliness = rawData[DataParser.BONE] as Dictionary<string, object>;
                foreach (var k in rawTimeliness.Keys)
                {
                    var rawTimelines = rawTimeliness[k] as List<object>;

                    var bone = _armature.GetBone(k);
                    if (bone == null)
                    {
                        continue;
                    }

                    for (int i = 0, l = rawTimelines.Count; i < l; i += 2)
                    {
                        var timelineType = int.Parse(rawTimelines[i].ToString());
                        var timelineOffset = int.Parse(rawTimelines[i + 1].ToString());
                        var timeline = _ParseBinaryTimeline((TimelineType) timelineType, (uint) timelineOffset);
                        _animation.AddBoneTimeline(bone, timeline);
                    }
                }
            }

            if (rawData.ContainsKey(DataParser.SLOT))
            {
                var rawTimeliness = rawData[DataParser.SLOT] as Dictionary<string, object>;
                foreach (var k in rawTimeliness.Keys)
                {
                    var rawTimelines = rawTimeliness[k] as List<object>;

                    var slot = _armature.GetSlot(k);
                    if (slot == null)
                    {
                        continue;
                    }

                    for (int i = 0, l = rawTimelines.Count; i < l; i += 2)
                    {
                        var timelineType = int.Parse(rawTimelines[i].ToString());
                        var timelineOffset = int.Parse(rawTimelines[i + 1].ToString());
                        var timeline = _ParseBinaryTimeline((TimelineType) timelineType, (uint) timelineOffset);
                        _animation.AddSlotTimeline(slot, timeline);
                    }
                }
            }

            if (rawData.ContainsKey(DataParser.CONSTRAINT))
            {
                var rawTimeliness = rawData[DataParser.CONSTRAINT] as Dictionary<string, object>;
                foreach (var k in rawTimeliness.Keys)
                {
                    var rawTimelines = rawTimeliness[k] as List<object>;

                    var constraint = _armature.GetConstraint(k);
                    if (constraint == null)
                    {
                        continue;
                    }

                    for (int i = 0, l = rawTimelines.Count; i < l; i += 2)
                    {
                        var timelineType = int.Parse(rawTimelines[i].ToString());
                        var timelineOffset = int.Parse(rawTimelines[i + 1].ToString());
                        var timeline = _ParseBinaryTimeline((TimelineType) timelineType, (uint) timelineOffset);
                        _animation.AddConstraintTimeline(constraint, timeline);
                    }
                }
            }

            _animation = null;

            return animation;
        }
        protected override void _ParseArray(Dictionary<string, object> rawData)
        {
            var offsets = rawData[DataParser.OFFSET] as List<object>;

            var l0 = int.Parse(offsets[0].ToString());
            var l1 = int.Parse(offsets[1].ToString());
            var l2 = int.Parse(offsets[3].ToString());
            var l3 = int.Parse(offsets[5].ToString());
            var l4 = int.Parse(offsets[7].ToString());
            var l5 = int.Parse(offsets[9].ToString());
            var l6 = int.Parse(offsets[11].ToString());

            short[] intArray = { };
            float[] floatArray = { };
            short[] frameIntArray = { };
            float[] frameFloatArray = { };
            short[] frameArray = { };
            ushort[] timelineArray = { };

            using (var ms = new MemoryStream(_binary))
            using (var reader = new BinaryDataReader(ms))
            {
                //ToRead
                reader.Seek(_binaryOffset, SeekOrigin.Begin);

                intArray = reader.ReadInt16s(l0, l1 / Helper.INT16_SIZE);
                floatArray = reader.ReadSingles(0, l2 / Helper.FLOAT_SIZE);
                frameIntArray = reader.ReadInt16s(0, l3 / Helper.INT16_SIZE);
                frameFloatArray = reader.ReadSingles(0, l4 / Helper.FLOAT_SIZE);
                frameArray = reader.ReadInt16s(0, l5 / Helper.INT16_SIZE);
                timelineArray = reader.ReadUInt16s(0, l6 / Helper.UINT16_SIZE);

                reader.Close();
                ms.Close();
            }

            _data.binary = _binary;

            //
            _intArrayBuffer = intArray;
            _floatArrayBuffer = floatArray;
            _frameIntArrayBuffer = frameIntArray;
            _frameFloatArrayBuffer = frameFloatArray;
            _frameArrayBuffer = frameArray;
            _timelineArrayBuffer = timelineArray;

            _data.intArray = _intArrayBuffer;
            _data.floatArray = _floatArrayBuffer;
            _data.frameIntArray = _frameIntArrayBuffer;
            _data.frameFloatArray = _frameFloatArrayBuffer;
            _data.frameArray = _frameArrayBuffer;
            _data.timelineArray = _timelineArrayBuffer;
        }

        public override DragonBonesData ParseDragonBonesData(object rawObj, float scale = 1)
        {
            Helper.Assert(rawObj != null && rawObj is byte[], "Data error.");

            var bytes = rawObj as byte[];
            object header = DeserializeBinaryJsonData(bytes, out var headerLength);

            _binary = bytes;
            _binaryOffset = 8 + 4 + headerLength;

            return base.ParseDragonBonesData(header, scale);
        }

        public static Dictionary<string, object> DeserializeBinaryJsonData(byte[] bytes, out int headerLength)
        {
            headerLength = 0;
            Dictionary<string, object> result = null;
            using (var ms = new MemoryStream(bytes))
            using (var reader = new BinaryDataReader(ms))
            {
                ms.Position = 0;
                var tag = reader.ReadBytes(8);

                var array = System.Text.Encoding.ASCII.GetBytes("DBDT");

                if (tag[0] != array[0] ||
                    tag[1] != array[1] ||
                    tag[2] != array[2] ||
                    tag[3] != array[3])
                {
                    Helper.Assert(false, "Non-supported data.");
                    return null;
                }

                headerLength = (int) reader.ReadUInt32();
                var headerBytes = reader.ReadBytes(headerLength);
                var headerString = System.Text.Encoding.UTF8.GetString(headerBytes);
                result = Json.Deserialize(headerString) as Dictionary<string, object>;

                reader.Close();
                ms.Dispose();
            }

            return result;
        }
    }
}
