using Client.Scripts.Extensions;
using Client.Scripts.Levels.Components;
using Client.Scripts.Players.Components;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace Client.Scripts.Players.Systems
{
    public class CreatePlayerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CreatePlayerEvent> _createPlayerFilter = null;
        private readonly EcsFilter<LevelSettingsComponent> _levelSettingsFilter = null;
        
        public void Run()
        {
            if (_createPlayerFilter.IsEmpty()) return;

            var levelSettings = _levelSettingsFilter.Single().LevelSettings;
            var playerPrefab = levelSettings.playerSettings.playerPrefab;
            var view = Object.Instantiate(playerPrefab);
            view.transform.SetPositionFromGrid(0, 0, levelSettings.cellSize);
        }
    }
}