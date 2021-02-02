using System.Collections.Generic;
using System.Linq;

using Heirloom.Collections;
using Heirloom.Desktop;
using Heirloom.Drawing;
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
            // Generate a random graph with 10% connection density, keeping the largest component.
            Graph = GenerateRandom(20, 0.10F);
            Graph = Graph.CreateSubgraph(Graph.GetComponents().FindMaximal(g => g.Count));

            // Detect communities, giving each a unique color.
            Communities = Graph.DetectCommunities();
            for (var i = 0; i < Communities.Length; i++)
            {
                var hue = i / (float) Communities.Length * 360F;
                var color = Color.FromHSV(hue, 0.8F, 0.8F);
                foreach (var member in Communities[i])
                {
                    member.Color = color;
                }
            }

            // 
            Nodes = Graph.Vertices.ToArray();
            Springs = Graph.Edges.Select(edge => new Spring
            {
                A = edge.Item1,
                B = edge.Item2,
                Length = 10,
                K = 1.25F
            }).ToArray();

            // Position nodes in a parabolic spiral
            for (var i = 0; i < Nodes.Length; i++)
            {
                Nodes[i].Position = 20 * Vector.GetParabolicSpiralPoint(i);
            }

            // Zoom through the first 100 simulation steps to stablize the visualization
            for (var i = 0; i < 100; i++) { UpdateSimulation(); }
        }

        private static Graph<Node, int> GenerateRandom(int size, float probability)
        {
            var graph = new Graph<Node, int>();
            var nodes = new Node[size];

            for (var i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new Node($"Node: {i}");
                graph.AddVertex(nodes[i]);
            }

            for (var i = 0; i < nodes.Length; i++)
            {
                for (var j = i + 1; j < nodes.Length; j++)
                {
                    if (Calc.Random.Chance(probability))
                    {
                        graph.AddEdge(nodes[i], nodes[j]);
                    }
                }
            }

            return graph;
        }

        private void UpdateSimulation()
        {
            ApplyColumbsLaw();    // Repulsion
            ApplyHookesLaw();     // Springs 
            IntegrateParticles(); // Integrate
        }

        protected override void Update(float dt)
        {
            UpdateSimulation();
            DrawApplication();

            // Place picture on screen
            Graphics.Screen.Refresh();
        }

        private void DrawApplication()
        {
            Graphics.ResetState();
            Graphics.Clear(Color.DarkGray);

            // 
            var bounds = Rectangle.FromPoints(Nodes.Select(n => n.Position));
            var scale = Calc.Min(Graphics.Surface.Width, Graphics.Surface.Height) / (64 + Calc.Max(bounds.Width, bounds.Height));
            Graphics.SetCamera(bounds.Center, scale);

            // Draw edges
            Graphics.Color = Color.Gray;
            foreach (var (a, b) in Graph.Edges)
            {
                Graphics.DrawLine(a.Position, b.Position, Graphics.ApproximatePixelScale);
            }

            // Draw vertices
            foreach (var v in Graph.Vertices)
            {
                Graphics.Color = v.Color;
                Graphics.DrawCircle(v.Position, 5);
            }
        }

        private void IntegrateParticles()
        {
            var div = 10 + Calc.Sqrt(Nodes.Length);
            var err = 1F - Springs.Length / (Nodes.Length * (Nodes.Length - 1) / 2F) / 2F;

            foreach (var node in Nodes)
            {
                node.Velocity += node.Force / div;
                node.Velocity *= err;

                // Speed limit, prevent explosions
                if (node.Velocity.Length > 1000) { node.Velocity = Vector.Normalize(node.Velocity) * 1000; }

                node.Position += node.Velocity / div;

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

        private static GraphicsContext CreateWindowGraphics()
        {
            var window = new Window("Graph Visualization", (640, 640), MultisampleQuality.High);
            window.Position = (IntVector) (Display.Primary.Size - window.Size) / 2;

            return window.Graphics;
        }

        static void Main(string[] args)
        {
            Application.Run<Program>();
        }
    }
}
