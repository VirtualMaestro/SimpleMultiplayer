namespace Client.Scripts.Pathfinding
{
    public class Node
    {
        public readonly int X;
        public readonly int Y;
        public readonly bool IsWalkable;

        // Heuristic cost
        public float HCost;
        // Path cost to this node
        public float GCost;

        // Total cost  (GCost + HCost)
        public float FCost => GCost + HCost;

        public Node ParentNode;

        public Node(int col, int row, bool isWalkable = true)
        {
            X = col;
            Y = row;
            IsWalkable = isWalkable;
        }

        public override string ToString()
        {
            return $"Node position: [{X}/{Y}], Walkable: {IsWalkable}, Costs: F={FCost}, H={HCost}, G={GCost}";
        }
    }
}