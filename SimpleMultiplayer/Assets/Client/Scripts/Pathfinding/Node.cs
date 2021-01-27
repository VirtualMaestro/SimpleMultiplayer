namespace Client.Scripts.Pathfinding
{
    public class Node
    {
        public readonly int X;
        public readonly int Z;
        public readonly bool IsWalkable;

        //Node's costs
        public float HCost;
        public float GCost;

        // Total cost  (GCost + HCost)
        public float Cost => GCost + HCost;

        public Node ParentNode;

        public Node(int col, int row, bool isWalkable = true)
        {
            X = col;
            Z = row;
            IsWalkable = isWalkable;
        }

        public override string ToString()
        {
            return $"Node position: [{X}/{Z}], Walkable: {IsWalkable}, Costs: H={HCost}, G={GCost}";
        }
    }
}