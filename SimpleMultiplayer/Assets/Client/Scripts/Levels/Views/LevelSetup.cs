using Client.Scripts.Grids.Components;
using Client.Scripts.Levels.Components;
using Client.Scripts.Levels.SO;
using Client.Scripts.Players.Components;
using Leopotam.Ecs;
using StubbUnity.Unity.View;
using UnityEngine;

namespace Client.Scripts.Levels.Views
{
    public class LevelSetup : EcsViewLink
    {
        [SerializeField] private LevelSettingsSO levelSettings;
        [SerializeField] private GameObject gridHolder;

        public override void Initialize()
        {
            World.NewEntity().Get<LevelSettingsComponent>().LevelSettings = levelSettings;
            World.NewEntity().Get<CreateGridEvent>().GridHolder = gridHolder;
            World.NewEntity().Get<CreatePlayerEvent>();
        }
    }
}