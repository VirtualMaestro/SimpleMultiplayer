using Leopotam.Ecs;
using StubbUnity.Unity.Contexts;

namespace Client.Scripts
{
    public class RoyalContext : UnityContext
    {
        protected override IEcsSystem InitUserSystems()
        {
            return new RootFeature(World, "RootFeature");
        }
    }
}