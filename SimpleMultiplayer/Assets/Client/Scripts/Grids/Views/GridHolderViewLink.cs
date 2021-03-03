using Client.Scripts.Grids.Components;
using Leopotam.Ecs;
using StubbUnity.Unity.View;

namespace Client.Scripts.Grids.Views
{
    public class GridHolderViewLink : EcsViewLink
    {
        public override void Initialize()
        {
            World.NewEntity().Get<GridHolderComponent>().View = this;
        }
    }
}