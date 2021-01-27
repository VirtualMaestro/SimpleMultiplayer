using UnityEngine;

namespace Client.Scripts.Players.SO
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "SMP/Player settings", order = 0)]
    public class PlayerSettingsSO : ScriptableObject
    {
        public GameObject playerPrefab;
        public float playerSpeed = 0.5f;
    }
}