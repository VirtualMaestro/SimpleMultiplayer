using Client.Scripts.Cameras.Components;
using Leopotam.Ecs;
using StubbUnity.Unity.View;
using UnityEngine;

namespace Client.Scripts.Cameras.Views
{
    public class CameraEcsViewLink : EcsViewLink
    {
        public override void Initialize()
        {
            GetEntity().Get<CameraComponent>().Camera = gameObject.GetComponent<Camera>();
        }
    }
}