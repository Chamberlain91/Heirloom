using System;

using Meadows.Desktop;

namespace Meadows.Examples.Othello
{
    public sealed class Program : Application
    {
        public Window Window { get; }

        public Program()
        {
            Window = new Window("Othello", (571, 1206));
        }

        private static void Main(string[] args)
        {
            // 571x1206
            Run<Program>();
        }
    }

    public enum OthelloGridState : byte
    {
        Nothing,
        Black,
        White
    }

    public enum OthelloColor : byte
    {
        Black,
        White
    }

    public sealed class OthelloBoardState
    // todo: perhaps use a mutation stack (aka, undo system)
    // this should optimize performance and memory usage
    {
        private readonly OthelloGridState[,] _grid;

        public OthelloBoardState()
        {
            _grid = new OthelloGridState[8, 8];
        }

        public bool CanPlacePiece(int x, int y)
        {
            return GetGridState(x, y) == OthelloGridState.Nothing;
        }

        public bool HasPiece(int x, int y)
        {
            return GetGridState(x, y) != OthelloGridState.Nothing;
        }

        public OthelloGridState GetGridState(int x, int y)
        {
            return _grid[y, x];
        }

        public void PlacePiece(int x, int y, OthelloColor color)
        {
            if (CanPlacePiece(x, y))
            {
                // Place piece
                if (color == OthelloColor.Black) { _grid[y, x] = OthelloGridState.Black; }
                else { _grid[y, x] = OthelloGridState.White; }
            }
            else
            {
                throw new InvalidOperationException("Unable to place piece, position occupied.");
            }
        }

        public void FlipPiece(int x, int y)
        {
            if (HasPiece(x, y))
            {
                var state = GetGridState(x, y);

                // Invert
                if (state == OthelloGridState.Black) { _grid[y, x] = OthelloGridState.White; }
                else { _grid[y, x] = OthelloGridState.Black; }
            }
            else
            {
                throw new InvalidOperationException("Unable to flip piece, does not exist.");
            }
        }
    }
}
