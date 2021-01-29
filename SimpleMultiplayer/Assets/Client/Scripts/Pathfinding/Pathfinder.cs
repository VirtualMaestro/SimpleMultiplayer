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
                    // Choose new optimal node with less cost
                    if (node.FCost < currentNode.FCost ||
                        Math.Abs(node.FCost - currentNode.FCost) < Tolerance && node.HCost < currentNode.HCost)
                    {
                        if (currentNode.X != node.X || currentNode.Y != node.Y)
                        {
                            currentNode = node;
                        }
                    }
                }

                OpenSet.RemoveAt(0);
                ClosedSet.Add(currentNode);

                // the target node was reached
                if (currentNode.X == end.X && currentNode.Y == end.Y)
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
                    var moveCostToNeighbour = currentNode.GCost + _calculateHeuristicCost(currentNode, neighbour);

                    // if it's lower than the neighbour's cost
                    if (!(moveCostToNeighbour < neighbour.GCost) && OpenSet.Contains(neighbour)) 
                        continue;
                    
                    // calculate new costs
                    neighbour.GCost = moveCostToNeighbour;
                    neighbour.HCost = _calculateHeuristicCost(neighbour, end);
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
                for (var y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) 
                        continue;

                    if (_ValidateNeighbour(node.X + x, node.Y + y, node.X, node.Y, out var neighbourNode))
                    {
                        neighbours.Add(neighbourNode);
                    }                    
                }
            }

            return neighbours;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool _ValidateNeighbour(int searchPosX, int searchPosY, int currentPosX, int currentPosY, out Node neighbourNode)
        {
            neighbourNode = _grid.GetNode(searchPosX, searchPosY);

            if (neighbourNode == null || !neighbourNode.IsWalkable) 
                return false;
            
            // If this neighbour node is diagonal it is has to be checked two neighbours from both sides for walkable
            // in order to move diagonally
            var originalX = searchPosX - currentPosX;
            var originalY = searchPosY - currentPosY;

            // if searched node isn't diagonal
            if (Math.Abs(originalX) != 1 || Math.Abs(originalY) != 1) 
                return true;
            
            // if it is diagonal then check its neighbors whether they are walkable
            var neighbour1 = _grid.GetNode(currentPosX + originalX, currentPosY);
            if (neighbour1 == null || !neighbour1.IsWalkable)
                return false;

            var neighbour2 = _grid.GetNode(currentPosX, currentPosY + originalY);
            return neighbour2 != null && neighbour2.IsWalkable;
        }

        /// Used heuristic method 'Diagonal Shortcut'
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int _calculateHeuristicCost(Node posA, Node posB)
        {
            var distX = Math.Abs(posA.X - posB.X);
            var distY = Math.Abs(posA.Y - posB.Y);

            if (distX > distY)
            {
                return 14 * distY + 10 * (distX - distY);
            }

            return 14 * distX + 10 * (distY - distX);
        }
    }
}
