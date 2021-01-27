using Client.Scripts.Players.SO;
using UnityEngine;

namespace Client.Scripts.Levels.SO
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "SMP/Level settings")]
    public class LevelSettingsSO : ScriptableObject
    {
        public int columns;
        public int rows;
        public float cellSize = 1f;
        public GameObject tileWalkablePrefab;
        public GameObject tileImpassablePrefab;
        public Material backgroundMaterial;
        public GameObject[] obstaclePrefabs;
        public float mouseSensitive = -0.02f;
        public PlayerSettingsSO playerSettings;
    }
}
