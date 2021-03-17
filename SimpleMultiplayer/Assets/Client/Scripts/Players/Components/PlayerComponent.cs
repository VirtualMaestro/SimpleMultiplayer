using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Scripts.Players.Components
{
    public struct PlayerComponent : IEcsAutoReset<PlayerComponent>
    {
        public GameObject Player;
        public int MoveAnimationId;
        
        public void AutoReset(ref PlayerComponent c)
        {
            DOTween.Kill(c.MoveAnimationId);
            c.MoveAnimationId = -1;
        }
    }
}