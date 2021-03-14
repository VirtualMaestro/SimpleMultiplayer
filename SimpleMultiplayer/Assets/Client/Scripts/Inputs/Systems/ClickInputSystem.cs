using Client.Scripts.Cameras.Components;
using Client.Scripts.Grids.Components;
using Client.Scripts.Grids.Views;
using Client.Scripts.Inputs.Components;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace Client.Scripts.Inputs.Systems
{
    public class ClickInputSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<CameraComponent> _cameraFilter = null;
        private readonly EcsFilter<ScreenClickEvent> _screenClickFilter = null;

        public void Run()
        {
            if (_screenClickFilter.IsEmpty() || _cameraFilter.IsEmpty())
                return;

            var screenPosition = _screenClickFilter.Single();
            var ray = _cameraFilter.Single().Camera.ScreenPointToRay(screenPosition.Position);

            if (!Physics.Raycast(ray, out var hit, 100)) return;

            var tileView = hit.collider.GetComponent<TileViewLink>();
            if (tileView == null) return;

            ref var clickEvent = ref _world.NewEntity().Get<TileClickEvent>();
            clickEvent.GridX = tileView.GridX;
            clickEvent.GridY = tileView.GridY;
        }
    }
}