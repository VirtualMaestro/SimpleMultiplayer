using Client.Scripts.Cameras.Components;
using Client.Scripts.Inputs.Components;
using Client.Scripts.Levels.Components;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;

namespace Client.Scripts.Inputs.Systems
{
    public class DragInputSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<LevelSettingsComponent> _levelSettingsFilter = null;
        private readonly EcsFilter<DragEvent> _dragEventFilter = null;

        public void Run()
        {
            if (_dragEventFilter.IsEmpty())
                return;

            var dragEvent = _dragEventFilter.Get1(0);
            var mouseSensitive = _levelSettingsFilter.Single().LevelSettings.mouseSensitive;

            ref var move = ref _world.NewEntity().Get<MoveCameraEvent>();
            move.DeltaX = dragEvent.DeltaX * mouseSensitive;
            move.DeltaY = dragEvent.DeltaY * mouseSensitive;
        }
    }
}