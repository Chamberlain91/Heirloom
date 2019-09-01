using System;
using System.Collections;
using System.Collections.Generic;

using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Runtime
{
    public sealed class Sprite : IEnumerable<Sprite.Frame>
    {
        private readonly List<Frame> _frames;
        private readonly float _defaultDelay;
        private Vector _origin;

        public Sprite(float defaultDelay = 1 / 10F)
        {
            _frames = new List<Frame>();
            _origin = Vector.Zero;

            _defaultDelay = defaultDelay;
        }

        public Sprite(IEnumerable<Image> images, float delay)
            : this(delay)
        {
            foreach (var image in images)
            {
                Add(image);
            }
        }

        public Image this[int index] => Frames[index].Image;

        public IReadOnlyList<Frame> Frames => _frames;

        internal bool HasCustomOrigin { get; private set; }

        public Vector Origin
        {
            get => _origin;

            set
            {
                HasCustomOrigin = !value.Equals(Vector.Zero);
                _origin = value;
            }
        }

        public void Add(Image image)
        {
            Add(image, _defaultDelay);
        }

        public void Add(Image image, float delay)
        {
            // todo: is it count - 1?
            Insert(Frames.Count, image, delay);
        }

        public void Insert(int index, Image image)
        {
            Insert(index, image, _defaultDelay);
        }

        public void Insert(int index, Image image, float delay)
        {
            var frame = new Frame(image, delay);
            _frames.Insert(index, frame);
        }

        public void Remove(int index)
        {
            _frames.RemoveAt(index);
        }

        public IEnumerator<Frame> GetEnumerator()
        {
            return ((IEnumerable<Frame>) _frames).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Frame>) _frames).GetEnumerator();
        }

        public class Frame
        {
            public Image Image;

            public float Delay;

            internal Frame(Image image, float delay)
            {
                Image = image ?? throw new ArgumentNullException(nameof(image));
                Delay = delay;
            }
        }
    }
}
