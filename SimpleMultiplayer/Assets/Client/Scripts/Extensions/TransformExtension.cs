using UnityEngine;

namespace Client.Scripts.Extensions
{
    public static class TransformExtension
    {
        public static Vector2 GetGridPosition(this Transform transform, float cellSize)
        {
            var position = transform.position;
            return new Vector2( position.x / cellSize, position.z / cellSize);
        }

        public static void SetPositionFromGrid(this Transform transform, int nodeX, int nodeZ, float cellSize)
        {
            var position = transform.position;
            position.x = nodeX * cellSize;
            position.z = nodeZ * cellSize;
            
            transform.position = position;
        }
    }
}