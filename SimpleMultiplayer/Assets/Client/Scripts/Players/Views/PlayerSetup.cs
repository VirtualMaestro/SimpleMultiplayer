using Client.Scripts.Players.Components;
using Leopotam.Ecs;
using StubbUnity.Unity.View;

namespace Client.Scripts.Players.Views
{
    public class PlayerSetup : EcsViewLink
    {
        public override void Initialize()
        {
            GetEntity().Get<PlayerComponent>().Player = gameObject;
        }
    }
}