using Client.Scripts.Cameras.Components;
using Client.Scripts.Cameras.Systems;
using Client.Scripts.Grids.Components;
using Client.Scripts.Grids.Systems;
using Client.Scripts.Inputs.Components;
using Client.Scripts.Inputs.Systems;
using Client.Scripts.Players.Components;
using Client.Scripts.Players.Systems;
using Client.Scripts.UIs;
using StubbUnity.StubbFramework.Core;

namespace Client.Scripts
{
    public class MainFeature : EcsFeature
    {
        public MainFeature()
        {
            Add(new ClickInputSystem());
            Add(new DragInputSystem());
            Add(new CreateGridSystem());
            Add(new CreatePlayerSystem());
            
            Add(new UIHandlerSystem());
            Add(new TileClickSystem());
            Add(new MovePlayerSystem());
            Add(new MoveCameraSystem());

            OneFrame<DragEvent>();
            OneFrame<ScreenClickEvent>();
            OneFrame<CreateGridEvent>();
            OneFrame<MovePlayerEvent>();
            OneFrame<TileClickEvent>();
            OneFrame<MoveCameraEvent>();
            OneFrame<CreatePlayerEvent>();
        }
    }
}