using System.Runtime.CompilerServices;

namespace Client.Scripts.Pathfinding
{
    public class GridBase
    {
        private readonly int _columns;
        private readonly int _rows;
        private readonly Node[,] _grid;

        public int Columns => _columns;
        public int Rows => _rows;
        
        public GridBase(int cols, int rows)
        {
            _columns = cols;
            _rows = rows;
            _grid = new Node[_columns, _rows];
        }

        public void AddNode(Node node)
        {
            if (_CanBeSet(node.X, node.Z))
            {
                _grid[node.X, node.Z] = node;
            }
        }
        
        public Node GetNode(int x, int z)
        {
            return _CanBeSet(x, z) ? _grid[x, z] : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool _CanBeSet(int x, int z)
        {
            return x < _columns && x >= 0 && z >= 0 && z < _rows;
        }
    }
}
