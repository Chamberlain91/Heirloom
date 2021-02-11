using System.Collections.Generic;

using DragonBones;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Examples.Anim2D.Anim2D;
using Heirloom.IO;
using Heirloom.Mathematics;
using Heirloom.Utilities;

namespace Heirloom.Examples.Anim2D
{
    public class Program : GameWrapper
    {
        public Armature[] Armatures;
        public Mathematics.Matrix[] Matrices;

        public Program()
            : base(CreateWindowGraphics())
        {
            // Load rooster anim
            LoadDragonBonesData("abc", "Files/NewDragon_ske.json", "Files/NewDragon_tex.json", "Files/NewDragon_tex.png");

            // construct armature
            Armatures = new Armature[1];
            Matrices = new Mathematics.Matrix[Armatures.Length];
            for (var i = 0; i < Armatures.Length; i++)
            {
                Armatures[i] = AnimFactory.Factory.BuildArmature("armatureName", "abc");
                Armatures[i].animation.GotoAndPlayByTime("stand", Calc.Random.NextFloat());

                Matrices[i] = Mathematics.Matrix.CreateTranslation(Calc.Random.NextVectorDisk(0));
            }
        }

        private static void LoadDragonBonesData(string name, string skeletonPath, string atlasPath, string imagePath)
        {
            // load bones
            var bonesJson = Files.ReadText(skeletonPath);
            var bonesData = (Dictionary<string, object>) MiniJSON.Json.Deserialize(bonesJson);
            AnimFactory.Factory.ParseDragonBonesData(bonesData, name);

            // load atlas data
            var atlasJson = Files.ReadText(atlasPath);
            var atlasData = (Dictionary<string, object>) MiniJSON.Json.Deserialize(atlasJson);
            AnimFactory.Factory.ParseTextureAtlasData(atlasData, new Image(imagePath), name);
        }

        protected override void Update(float dt)
        {
            // 
            Graphics.ResetState();
            Graphics.Clear(Color.DarkGray);
            Graphics.Performance.ShowOverlay = true;
            Graphics.SetCamera(Vector.Up * 250, 1.0F);

            // Update animation system
            AnimFactory.Factory.DragonBones.AdvanceTime(dt);

            for (var i = 0; i < Armatures.Length; i++)
            {
                // Render armature
                var armature = Armatures[i].display as AnimArmature;
                foreach (var slot in armature.Slots)
                {
                    slot.Draw(Graphics, Matrices[i]);
                }
            }

            // Place picture on screen
            Graphics.Screen.Refresh();
        }

        private static GraphicsContext CreateWindowGraphics()
        {
            var window = new Window("Graph Visualization", MultisampleQuality.Medium);
            // window.Position = (IntVector) (Display.Primary.Size - window.Size) / 2;
            window.Maximize();

            return window.Graphics;
        }

        static void Main(string[] args)
        {
            Application.Run<Program>();
        }
    }
}
