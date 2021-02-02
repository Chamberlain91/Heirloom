using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using Heirloom.Collections;
using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Mathematics;
using Heirloom.Utilities;

namespace Heirloom.Examples.GraphVis
{
    internal sealed class Program : GameWrapper
    {
        public Graph<Node, int> Graph { get; }

        public Spring[] Springs { get; }
        public Node[] Nodes { get; }

        public IReadOnlyCollection<Node>[] Communities { get; }

        public Program()
            : base(CreateWindowGraphics())
        {
            // https://github.com/gephi/gephi/wiki/Datasets
            Graph = LoadTGF(Files.OpenStream("lesmiserables.tgf"));

            // 
            Communities = Graph.DetectCommunities();
            for (var i = 0; i < Communities.Length; i++)
            {
                Log.Info($"{i}: {string.Join(", ", Communities[i])}");

                var hue = i / (float) Communities.Length * 300F;
                var color = Color.FromHSV(hue, 0.8F, 0.8F);
                foreach (var member in Communities[i])
                {
                    member.Color = color;
                }
            }

            var n = 0;
            foreach (var v in Graph.Vertices)
            {
                v.Position = 10 * Vector.GetParabolicSpiralPoint(n++);
            }

            // 
            Nodes = Graph.Vertices.ToArray();
            Springs = Graph.Edges.Select(edge => new Spring
            {
                A = edge.Item1,
                B = edge.Item2,
                Length = 20,
                K = 1.25F
            }).ToArray();
        }

        protected override void Update(float dt)
        {
            UpdateApplication(dt);
            DrawApplication();

            // Place picture on screen
            Graphics.Screen.Refresh();
        }

        private void UpdateApplication(float dt)
        {
            ApplyColumbsLaw(); // Repulsion
            ApplyHookesLaw();  // Springs

            // Integrate
            IntegrateParticles(dt);
        }

        private void DrawApplication()
        {
            Graphics.ResetState();
            Graphics.Clear(Color.DarkGray);

            // 
            var bounds = Rectangle.FromPoints(Nodes.Select(n => n.Position));
            var scale = Calc.Min(Graphics.Surface.Width, Graphics.Surface.Height) / (64 + Calc.Max(bounds.Width, bounds.Height));
            Graphics.SetCamera(bounds.Center, scale);

            // Draw communities
            foreach (var community in Communities)
            {
                Graphics.Color = community.First().Color * Color.DarkGray;
                var hull = Polygon.CreateConvexHull(community.Select(member => member.Position));
                hull = Polygon.Inflate(hull, 10);

                Graphics.DrawPolygon(hull);

                Graphics.Color = community.First().Color;
                Graphics.DrawPolygonOutline(hull, 2);
            }

            // Draw edges
            Graphics.Color = Color.Gray;
            foreach (var (a, b) in Graph.Edges)
            {
                Graphics.DrawLine(a.Position, b.Position);
            }

            // Draw vertices
            foreach (var v in Graph.Vertices)
            {
                Graphics.Color = v.Color;
                Graphics.DrawCircle(v.Position, 3);
            }
        }

        private void IntegrateParticles(float dt)
        {
            foreach (var node in Nodes)
            {
                node.Velocity += node.Force / 10F;
                node.Velocity *= 0.90F;

                node.Position += node.Velocity / 10F;

                node.Force = Vector.Zero;
            }
        }

        private void ApplyColumbsLaw()
        {
            for (var i = 0; i < Nodes.Length; i++)
            {
                for (var j = i + 1; j < Nodes.Length; j++)
                {
                    var particleA = Nodes[i];
                    var particleB = Nodes[j];

                    var offset = particleA.Position - particleB.Position;
                    var distance = offset.Length;
                    var dir = offset.Normalized;

                    var strength = 10000F / (0.5F * (distance * distance) + 0.0001F);
                    particleA.Force += dir * strength;
                    particleB.Force -= dir * strength;
                }
            }
        }

        private void ApplyHookesLaw()
        {
            foreach (var spring in Springs)
            {
                var offset = spring.B.Position - spring.A.Position;
                var displacement = spring.Length - offset.Length;
                var dir = offset.Normalized;

                // Apply spring force
                spring.A.Force -= dir * spring.K * displacement * 0.5F;
                spring.B.Force += dir * spring.K * displacement * 0.5F;
            }
        }

        public sealed class Node : System.IEquatable<Node>
        {
            public readonly string Name;

            public Vector Position;

            public Vector Velocity;

            public Vector Force;

            public Color Color;

            public Node(string name)
            {
                Name = name ?? throw new System.ArgumentNullException(nameof(name));
            }

            #region Equality

            public override bool Equals(object obj)
            {
                return Equals(obj as Node);
            }

            public bool Equals(Node other)
            {
                return other != null &&
                       Name == other.Name;
            }

            public override int GetHashCode()
            {
                return System.HashCode.Combine(Name);
            }

            public override string ToString()
            {
                return Name;
            }

            public static bool operator ==(Node left, Node right)
            {
                return EqualityComparer<Node>.Default.Equals(left, right);
            }

            public static bool operator !=(Node left, Node right)
            {
                return !(left == right);
            }

            #endregion
        }

        public sealed class Spring
        {
            public Node A;

            public Node B;

            public float Length;

            public float K;
        }

        private static Graph<Node, int> LoadTGF(Stream stream)
        {
            using var reader = new StreamReader(stream);

            const int READ_VERTICES = 0;
            const int READ_EDGES = 1;

            var graph = new Graph<Node, int>(directed: false);
            var names = new Dictionary<int, Node>();

            var mode = READ_VERTICES;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (line == "#") { mode = READ_EDGES; }
                else
                if (mode == READ_VERTICES)
                {
                    var match = Regex.Match(line, @"(\d+)\s+(.+)");
                    if (match.Success)
                    {
                        var id = int.Parse(match.Groups[1].Value);
                        var name = match.Groups[2].Value;

                        var node = new Node(name);
                        graph.AddVertex(node);
                        names[id] = node;
                    }
                    else
                    {
                        Log.Warning("Unable to read line from TFG file");
                    }
                }
                else
                {
                    var match = Regex.Match(line, @"(\d+)\s+(\d+)");
                    if (match.Success)
                    {
                        var a = names[int.Parse(match.Groups[1].Value)];
                        var b = names[int.Parse(match.Groups[2].Value)];
                        graph.AddEdge(a, b);
                    }
                    else
                    {
                        Log.Warning("Unable to read line from TFG file");
                    }
                }
            }

            return graph;
        }

        private static GraphicsContext CreateWindowGraphics()
        {
            var window = new Window("Path Finding", (640, 640), MultisampleQuality.High);
            window.Position = (IntVector) (Display.Primary.Size - window.Size) / 2;

            return window.Graphics;
        }

        static void Main(string[] args)
        {
            Application.Run<Program>();
        }
    }
}
