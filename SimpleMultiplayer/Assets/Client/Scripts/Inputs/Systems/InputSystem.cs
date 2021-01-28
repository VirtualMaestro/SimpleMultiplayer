using Client.Scripts.Cameras.Components;
using Client.Scripts.Grids.Components;
using Client.Scripts.Grids.Views;
using Client.Scripts.Levels.Components;
using Lean.Touch;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace Client.Scripts.Inputs.Systems
{
    public class InputSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<CameraComponent> _cameraFilter = null;
        private readonly EcsFilter<LevelSettingsComponent> _levelSettingsFilter = null;


        public void Init()
        {
            LeanTouch.OnFingerTap += _OnTapHandler;
            LeanTouch.OnFingerUpdate += _OnDragHandler;
        }

        private void _OnTapHandler(LeanFinger finger)
        {
            if (_cameraFilter.IsEmpty()) return;
            
            var ray = _cameraFilter.Single().Camera.ScreenPointToRay(finger.ScreenPosition);

            if (!Physics.Raycast(ray, out var hit, 100)) return;

            var tileView = hit.collider.GetComponent<TileViewLink>();
            if (tileView == null) return;

            ref var clickEvent = ref _world.NewEntity().Get<TileClickEvent>();
            clickEvent.GridX = tileView.GridX;
            clickEvent.GridY = tileView.GridY;
        }

        private void _OnDragHandler(LeanFinger leanFinger)
        {
            var vec = leanFinger.ScreenDelta;
            if (vec.x == 0 && vec.y == 0) return;
            
            var mouseSensitive = _levelSettingsFilter.Single().LevelSettings.mouseSensitive;
            vec *= mouseSensitive;

            ref var move = ref _world.NewEntity().Get<MoveCameraEvent>();
            move.DeltaX = vec.x;
            move.DeltaY = vec.y;
        }
    }
}