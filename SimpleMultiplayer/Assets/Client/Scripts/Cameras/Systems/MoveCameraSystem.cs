using Client.Scripts.Cameras.Components;
using Client.Scripts.Grids.Components;
using Client.Scripts.Levels.Components;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace Client.Scripts.Cameras.Systems
{
    public class MoveCameraSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MoveCameraEvent> _moveCameraFilter = null;
        private readonly EcsFilter<CameraComponent> _cameraFilter = null;
        private readonly EcsFilter<GridHolderComponent> _gridHolderFilter = null;
        private readonly EcsFilter<LevelSettingsComponent> _levelSettingsFilter = null;
        
        public void Run()
        {
            if (_moveCameraFilter.IsEmpty()) return;

            var camera = _cameraFilter.Single().Camera;
            var move = _moveCameraFilter.Single();
            var settings = _levelSettingsFilter.Single().LevelSettings;
            var gridPosition = _gridHolderFilter.Single().View.transform.position;
            var width = settings.columns * settings.cellSize;
            var height = settings.rows * settings.cellSize;
            var newPosition = camera.transform.position;
            newPosition.x = Mathf.Clamp(newPosition.x + move.DeltaX, gridPosition.x - 1, width + 1);
            newPosition.z = Mathf.Clamp(newPosition.z + move.DeltaY, gridPosition.z - height/2, gridPosition.z);
            
            camera.transform.position = newPosition;
        }
        
        // Bounds GetMaxBounds(GameObject g) {
        //     var b = new Bounds(g.transform.position, Vector3.zero);
        //     foreach (Renderer r in g.GetComponentsInChildren<Renderer>()) {
        //         b.Encapsulate(r.bounds);
        //     }
        //     return b;
        // }
    }
}