﻿using Leopotam.Ecs;

namespace StubbUnity.StubbFramework.Extensions
{
    public static class SystemsExtension
    {
        public static void AddFeature(this EcsSystems systems, EcsFeature feature)
        {
            systems.Add(feature);
            systems.Add(feature.InternalSystems);
            feature.Parent = systems;
        }
    }
}