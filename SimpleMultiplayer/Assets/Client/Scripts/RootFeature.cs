using Client.Scripts.Cameras.Components;
using Client.Scripts.Cameras.Systems;
using Client.Scripts.Grids.Components;
using Client.Scripts.Grids.Systems;
using Client.Scripts.Inputs.Systems;
using Client.Scripts.Players.Components;
using Client.Scripts.Players.Systems;
using Client.Scripts.UIs;
using Leopotam.Ecs;
using StubbUnity.StubbFramework;

namespace Client.Scripts
{
    public class RootFeature : EcsFeature
    {
        public RootFeature(EcsWorld world, string name = null, bool isEnable = true) : base(world, name, isEnable)
        {
        }

        protected override void SetupSystems()
        {
            Add(new InputSystem());
            Add(new CreateGridSystem());
            Add(new CreatePlayerSystem());
            
            Add(new UIHandlerSystem());
            Add(new TileClickSystem());
            Add(new MovePlayerSystem());
            Add(new MoveCameraSystem());

            OneFrame<CreateGridEvent>();
            OneFrame<MovePlayerEvent>();
            OneFrame<TileClickEvent>();
            OneFrame<MoveCameraEvent>();
            OneFrame<CreatePlayerEvent>();
        }
    }
}