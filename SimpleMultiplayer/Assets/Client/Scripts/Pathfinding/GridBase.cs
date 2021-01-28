using System.Runtime.CompilerServices;

namespace Client.Scripts.Pathfinding
{
    public class GridBase
    {
        private readonly Node[,] _grid;

        public int Columns { get; }
        public int Rows { get; }

        public GridBase(int cols, int rows)
        {
            Columns = cols;
            Rows = rows;
            _grid = new Node[Columns, Rows];
        }

        public void AddNode(Node node)
        {
            if (_CanBeSet(node.X, node.Y))
            {
                _grid[node.X, node.Y] = node;
            }
        }
        
        public Node GetNode(int x, int z)
        {
            return _CanBeSet(x, z) ? _grid[x, z] : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool _CanBeSet(int x, int z)
        {
            return x < Columns && x >= 0 && z >= 0 && z < Rows;
        }
    }
}
