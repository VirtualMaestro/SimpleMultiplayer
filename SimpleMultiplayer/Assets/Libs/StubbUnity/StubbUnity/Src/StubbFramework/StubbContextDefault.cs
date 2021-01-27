using System.Runtime.CompilerServices;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Debugging;
using StubbUnity.StubbFramework.Extensions;

namespace StubbUnity.StubbFramework
{
    public class StubbContextDefault : IStubbContext
    {
        private EcsWorld _world;
        private EcsSystems _rootSystems;
        private IStubbDebug _debugInfo;

        public bool IsDisposed => _world == null;

        public EcsWorld World
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _world;
        }
        
        public void Init(EcsWorld world, IStubbDebug debug = null)
        {
            Stubb.AddContext(this);

            _world = world;
            _debugInfo = debug;

            _rootSystems = InitSystems();

            _debugInfo?.Init(_rootSystems, _world);

            _rootSystems.ProcessInjects();
            _rootSystems.Init();
        }

        protected virtual EcsSystems InitSystems()
        {
            var rootSystems = new EcsSystems(World, "RootSystems");
            rootSystems.AddFeature(new SystemHeadFeature(World));

            var userSystems = InitUserSystems();
            
            if (userSystems is EcsFeature feature) 
                rootSystems.AddFeature(feature);
            else 
                rootSystems.Add(userSystems);

            rootSystems.AddFeature(new SystemTailFeature(World));

            return rootSystems;
        }

        protected virtual IEcsSystem InitUserSystems()
        {
            return new EcsSystems(World, "UserSystems");
        }


        public void Run()
        {
            _rootSystems.Run();
            _debugInfo?.Debug();
        }

        public void Dispose()
        {
            _rootSystems.Destroy();
            _world.Destroy();

            _world = null;
            _rootSystems = null;
        }
    }
}