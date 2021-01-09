using System.Collections.Generic;

using Heirloom;

namespace Examples.MazeAI
{
    public sealed class Player
    {
        public static readonly IntVector InvalidCoordiante = new IntVector(-1, -1);

        private const float TimeStep = 1 / 5F;
        private float _time;

        private Vector _sourcePosition;

        public readonly Queue<IntVector> MoveQueue;

        public Player()
        {
            MoveQueue = new Queue<IntVector>();
        }

        public IntVector TargetPosition { get; set; }

        public Vector Position { get; set; }

        public IntVector Goal { get; set; }

        public void Update(Maze maze, float dt)
        {
            // User input, click to move player
            if (Input.CheckButton(MouseButton.Left, ButtonState.Pressed))
            {
                // Convert mouse to maze coordinates
                var coordinate = (IntVector) Vector.Floor(((Input.MousePosition - (8, 8)) / (2 * 16)));
                if (!maze.Graph.ContainsVertex(coordinate)) { Goal = (-1, -1); }
                else
                {
                    Goal = coordinate;
                    MoveQueue.Clear();

                    // Compute path to target
                    var path = maze.Graph.FindPath(TargetPosition, Goal, co => IntVector.ManhattanDistance(co, Goal));
                    foreach (var step in path) { MoveQueue.Enqueue(step); }
                }
            }

            _time += dt;

            // "Animate" the player towards the target
            if (_time > TimeStep)
            {
                _time -= TimeStep;

                if (MoveQueue.Count > 0)
                {
                    // Mark starting position, and set target
                    _sourcePosition = Position;
                    TargetPosition = MoveQueue.Dequeue();
                }
                else
                {
                    // No more moves, so sit still
                    _sourcePosition = TargetPosition;
                }
            }

            // Animate to target position
            var t = Calc.SmoothStep(_time / TimeStep);
            Position = Vector.Lerp(_sourcePosition, TargetPosition, t);
        }
    }
}
