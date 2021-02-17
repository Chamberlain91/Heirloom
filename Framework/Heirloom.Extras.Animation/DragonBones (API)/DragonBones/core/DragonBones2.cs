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
using System.Collections.Generic;

namespace DragonBones
{
    /// <private/>
    internal class DragonBones2
    {
        public static bool YDown = true;
        public static readonly string VERSION = "5.6.300";

        private readonly WorldClock _clock = new WorldClock();
        private readonly List<EventObject> _events = new List<EventObject>();
        //private readonly List<BaseObject> _objects = new List<BaseObject>();
        private IEventDispatcher<EventObject> _eventManager = null;

        public DragonBones2(IEventDispatcher<EventObject> eventManager)
        {
            _eventManager = eventManager;
        }

        public void AdvanceTime(float passedTime)
        {
            //if (_objects.Count > 0)
            //{
            //    for (var i = 0; i < _objects.Count; ++i)
            //    {
            //        var obj = _objects[i];
            //        obj.ReturnToPool();
            //    }

            //    _objects.Clear();
            //}

            if (_events.Count > 0)
            {
                for (var i = 0; i < _events.Count; ++i)
                {
                    var eventObject = _events[i];
                    var armature = eventObject.armature;
                    if (armature._armatureData != null)
                    {
                        // May be armature disposed before advanceTime.
                        armature.EventDispatcher.DispatchDBEvent(eventObject.type, eventObject);
                        if (eventObject.type == EventObject.SOUND_EVENT)
                        {
                            _eventManager.DispatchDBEvent(eventObject.type, eventObject);
                        }
                    }

                    BufferObject(eventObject);
                }

                _events.Clear();
            }

            _clock.AdvanceTime(passedTime);
        }

        public void BufferEvent(EventObject value)
        {
            if (!_events.Contains(value))
            {
                _events.Add(value);
            }
        }

        public void BufferObject(BaseObject value)
        {
            //if (!_objects.Contains(value))
            //{
            //    _objects.Add(value);
            //}

            value.ReturnToPool();
        }

        //public static implicit operator bool(DragonBones exists)
        //{
        //    return exists != null;
        //}

        public WorldClock Clock => _clock;

        public IEventDispatcher<EventObject> EventManager => _eventManager;
    }
}
