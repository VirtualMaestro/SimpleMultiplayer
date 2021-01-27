using Leopotam.Ecs;
using StubbUnity.StubbFramework.Remove.Systems;
using StubbUnity.StubbFramework.Scenes.Components;
using StubbUnity.StubbFramework.Scenes.Events;
using StubbUnity.StubbFramework.Scenes.Systems;
using StubbUnity.StubbFramework.View.Systems;

namespace StubbUnity.StubbFramework
{
    public class SystemTailFeature : EcsFeature
    {
        public SystemTailFeature(EcsWorld world, string name = null) : base(world,name ?? "TailSystems")
        {}

        protected override void SetupSystems()
        {
            Add(new LoadScenesSystem());
            Add(new LoadingScenesProgressSystem());

            Add(new UnloadScenesByNamesSystem());
            Add(new UnloadAllScenesSystem());
            Add(new UnloadNonNewScenesSystem());
            Add(new UnloadSceneSystem());
            
            OneFrame<SceneChangedStateComponent>();
            
            Add(new ChangeSceneStateByNameSystem());
            Add(new ActivateSceneSystem());
            Add(new DeactivateSceneSystem());
            
            Add(new RemoveEcsViewLinkSystem());
            Add(new RemoveEntitySystem());
            
            OneFrame<ActivateSceneComponent>();
            OneFrame<DeactivateSceneComponent>();
            OneFrame<SceneLoadedComponent>();

            OneFrame<LoadScenesEvent>();
            OneFrame<ActivateSceneByNameEvent>();
            OneFrame<DeactivateSceneByNameEvent>();
            OneFrame<UnloadNonNewScenesEvent>();
            OneFrame<UnloadAllScenesEvent>();
            OneFrame<UnloadScenesByNamesEvent>();
        }
    }
}