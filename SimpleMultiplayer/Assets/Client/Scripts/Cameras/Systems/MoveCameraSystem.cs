using Client.Scripts.Cameras.Components;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace Client.Scripts.Cameras.Systems
{
    public class MoveCameraSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MoveCameraEvent> _moveCameraFilter = null;
        private readonly EcsFilter<CameraComponent> _cameraFilter = null;
        
        public void Run()
        {
            if (_moveCameraFilter.IsEmpty()) return;

            var camera = _cameraFilter.Single().Camera;
            var move = _moveCameraFilter.Single();
            
            camera.transform.position += new Vector3(move.DeltaX, 0, move.DeltaY);
        }
    }
}