using System.Collections.Generic;
using System.Linq;

using Heirloom.Collections;
using Heirloom.Drawing;
using Heirloom.Mathematics;
using Heirloom.Utilities;

namespace Heirloom.CacheLRU
{
    public sealed class AtlasCachePrototype : GameWrapper
    {
        public readonly RectanglePacker<Image> Packer = new RectanglePacker<Image>(512, 512, PackingAlgorithm.Skyline);
        public readonly HashSet<Image> ActiveImages = new HashSet<Image>();
        public readonly Image AtlasImage = new Image(512, 512);

        public readonly WeightedCollection<int> SizeDistr = new WeightedCollection<int> {
            { 8, 8F }, { 16, 4F }, { 32, 2F }, { 64, 1F }
        };

        private float _addTimer;
        private float _remTimer;

        private readonly Queue<float> _frameTimes = new Queue<float>();

        public AtlasCachePrototype(GraphicsContext graphics, int frameRate = -1)
            : base(graphics, frameRate)
        {
            // Graphics.Performance.ShowOverlay = true;
        }

        protected override void Update(float dt)
        {
            // 
            if (_frameTimes.Count >= 1000) { _frameTimes.Dequeue(); }
            _frameTimes.Enqueue(dt);

            Graphics.Clear(Color.DarkGray);

            // 
            Graphics.Color = Color.White;
            Graphics.DrawImage(AtlasImage, Matrix.Identity);

            var validArea = 0F;
            var totalArea = 0F;

            // 
            foreach (var image in Packer.Elements)
            {
                totalArea += image.Size.Area;

                if (!ActiveImages.Contains(image)) { Graphics.Color = Color.Red; }
                else
                {
                    Graphics.Color = Color.Green;
                    validArea += image.Size.Area;
                }

                // 
                Graphics.DrawRectOutline(Packer.GetRectangle(image));
            }

            var ratio = validArea / totalArea;

            Graphics.Color = Color.White;
            Graphics.DrawText($"fragment: {ratio:0.00}", (10, 10), Font.SansSerifBold, 16);

            if (_frameTimes.Count > 0)
            {
                var stats = Statistics.Compute(_frameTimes);

                Graphics.Color = Color.Black;
                Graphics.DrawRect((0, 512, 512, 32));

                Graphics.DrawText($"mean: {stats.Mean * 1000:0.0}", (10, 496), Font.Default, 16);

                var x1 = 0F;
                var y1 = stats.Rescale(_frameTimes.First(), 0F, 32F);
                var step = 512F / _frameTimes.Count;
                foreach (var time in _frameTimes)
                {
                    var x2 = x1 + step;
                    var y2 = stats.Rescale(time, 0F, 32F);

                    Graphics.Color = Color.White;
                    Graphics.DrawLine((x1, 512 + y1), (x2, 512 + y2));

                    x1 = x2;
                    y1 = y2;
                }
            }

            // 
            Graphics.Screen.Refresh();

            _addTimer -= dt;
            _remTimer -= dt;

            if (ratio < 0.5F)
            {
                CompactAtlas();
            }

            if (_remTimer <= 0F)
            {
                if (Input.IsMouseDown(MouseButton.Right) && ActiveImages.Count > 0)
                {
                    // Randomly kill an image (pretend it was disposed)
                    var image = Calc.Random.Choice<Image>(ActiveImages.ToList());
                    ActiveImages.Remove(image);
                }

                foreach (var image in ActiveImages)
                {
                    if (!Packer.Contains(image) && !InsertAtlas(image))
                    {
                        // Critical Failure, Evict Atlas...
                        // todo: on multi-page, find LRU page and evict it
                        //       the age of a page could be the average age of images in the page?
                        AtlasImage.Clear(Color.Transparent);
                        Packer.Clear();
                    }
                }

                _remTimer = 0.01F;
            }

            if (Input.IsMouseDown(MouseButton.Left) && _addTimer <= 0F)
            {
                //var w = SizeDistr.GetValue(Calc.Random);
                //var h = SizeDistr.GetValue(Calc.Random);

                var w = Calc.Random.Next(4, 32);
                var h = Calc.Random.Next(4, 32);

                // Create new image
                var image = Image.CreateCheckerboardPattern(w, h, Calc.Random.NextColorHue(0.5F, 0.5F), 4);
                ActiveImages.Add(image); // Our fake gc-alive set

                // Insert image into atlas
                if (!InsertAtlas(image))
                {
                    // Critical Failure, Evict Atlas...
                    // todo: on multi-page, find LRU page and evict it
                    //       the age of a page could be the average age of images in the page?
                    AtlasImage.Clear(Color.Transparent);
                    Packer.Clear();
                }

                // Every 1/20th second
                _addTimer = 0.01F;
            }
        }

        private void CompactAtlas()
        {
            AtlasImage.Clear(Color.Transparent);
            Packer.Clear();

            foreach (var image in ActiveImages.OrderByDescending(i => i.Size.Area))
            {
                InsertAtlas(image);
            }
        }

        private bool InsertAtlas(Image image)
        {
            if (Packer.TryAdd(image, image.Size))
            {
                // Copy image into atlas
                var rectangle = Packer.GetRectangle(image);
                WriteImageToAtlas(image, rectangle);
                return true;
            }
            else
            {
                // Unable to insert
                return false;
            }
        }

        private void WriteImageToAtlas(Image image, IntRectangle rectangle)
        {
            foreach (var co in Rasterizer.Rectangle(rectangle))
            {
                var color = image.GetPixel(co - rectangle.Position);
                AtlasImage.SetPixel(co, color);
            }
        }
    }
}
