using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Client.Scripts.Pathfinding
{
    public static class Pathfinder
    {
        private const float Tolerance = 0.00001f;
        private static readonly HashSet<Node> ClosedSet = new HashSet<Node>();
        private static readonly List<Node> OpenSet = new List<Node>();

        private static GridBase _grid;

        public static bool FindPath(GridBase grid, in Node start, in Node end, ref List<Node> resultPath)
        {
            _grid = grid;
            
            OpenSet.Add(start);

            while (OpenSet.Count > 0)
            {
                var currentNode = OpenSet[0];

                foreach (var node in OpenSet)
                {
                    // Calculate costs for the current node
                    if (node.Cost < currentNode.Cost ||
                        Math.Abs(node.Cost - currentNode.Cost) < Tolerance && node.HCost < currentNode.HCost)
                    {
                        if (currentNode.X != node.X || currentNode.Z != node.Z)
                        {
                            currentNode = node;
                        }
                    }
                }

                OpenSet.Remove(currentNode);
                ClosedSet.Add(currentNode);

                // the target node was reached
                if (currentNode.X == end.X && currentNode.Z == end.Z)
                {
                    _RetracePath(start, currentNode, ref resultPath);
                    
                    _Reset();
                    return true;
                }

                var neighbours = _GetNeighbours(currentNode);
                foreach (var neighbour in neighbours)
                {
                    if (ClosedSet.Contains(neighbour)) 
                        continue;
                    
                    // new movement cost for neighbours
                    var moveCostToNeighbour = currentNode.GCost + _GetDistance(currentNode, neighbour);

                    // if it's lower than the neighbour's cost
                    if (!(moveCostToNeighbour < neighbour.GCost) && OpenSet.Contains(neighbour)) 
                        continue;
                    
                    // calculate new costs
                    neighbour.GCost = moveCostToNeighbour;
                    neighbour.HCost = _GetDistance(neighbour, end);
                    neighbour.ParentNode = currentNode;
                    
                    if (!OpenSet.Contains(neighbour))
                        OpenSet.Add(neighbour);
                }
            }

            _Reset();
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void _Reset()
        {
            _grid = null;
            ClosedSet.Clear();
            OpenSet.Clear();
        }

        private static void _RetracePath(Node startNode, Node endNode, ref List<Node> path)
        {
            var currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                
                currentNode = currentNode.ParentNode;
            }

            path.Reverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static List<Node> _GetNeighbours(Node node)
        {
            var neighbours = new List<Node>();

            for (var x = -1; x <= 1; x++)
            {
                for (var z = -1; z <= 1; z++)
                {
                    if (x == 0 && z == 0) 
                        continue;

                    if (_FindNeighbour(node.X + x, node.Z + z, node.X, node.Z, out var neighbourNode))
                    {
                        neighbours.Add(neighbourNode);
                    }                    
                }
            }

            return neighbours;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool _FindNeighbour(int searchPosX, int searchPosZ, int currentPosX, int currentPosZ, out Node neighbourNode)
        {
            neighbourNode = _grid.GetNode(searchPosX, searchPosZ);

            if (neighbourNode == null || !neighbourNode.IsWalkable) 
                return false;
            
            // If this neighbour node is diagonal it is has to be checked two neighbours from both sides for walkable
            // in order to move diagonally
            var originalX = searchPosX - currentPosX;
            var originalZ = searchPosZ - currentPosZ;

            if (Math.Abs(originalX) != 1 || Math.Abs(originalZ) != 1) 
                return true;
            
            var neighbour1 = _grid.GetNode(currentPosX + originalX, currentPosZ);
            if (neighbour1 == null || !neighbour1.IsWalkable)
                return false;

            var neighbour2 = _grid.GetNode(currentPosX, currentPosZ + originalZ);
            return neighbour2 != null && neighbour2.IsWalkable;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int _GetDistance(Node posA, Node posB)
        {
            var distX = Math.Abs(posA.X - posB.X);
            var distZ = Math.Abs(posA.Z - posB.Z);

            if (distX > distZ)
            {
                return 14 * distZ + 10 * (distX - distZ);
            }

            return 14 * distX + 10 * (distZ - distX);
        }
    }
}
