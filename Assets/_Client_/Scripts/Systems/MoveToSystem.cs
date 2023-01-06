using _Client_.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using SFramework.ECS.Runtime.Tweens.SFramework.Modules.SF_ECS_Tweens.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Systems
{
    public class MoveToSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveTo, RigidbodyRef>> _filter;
        private readonly EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var moveTo = ref _filter.Pools.Inc1.Get(entity);
                ref var rigidbody = ref _filter.Pools.Inc2.Get(entity).reference;

                moveTo._elapsedTime += moveTo.unscaledTime ? Time.fixedUnscaledDeltaTime : Time.fixedDeltaTime;

                var t = Mathf.Clamp((moveTo._elapsedTime - moveTo.cooldown) / moveTo.duration, 0.0f, 1.0f);
                
                rigidbody.position = SFMathFXHelper.CurvedValueECS(moveTo.animationCurve,
                    moveTo.startValue,
                    moveTo.endValue, t);

                if (moveTo._elapsedTime >= moveTo.cooldown + moveTo.duration)
                {
                    switch (moveTo.loopType)
                    {
                        case TweenLoopType.None:
                            rigidbody.position = moveTo.endValue;
                            _filter.Pools.Inc1.Del(entity);
                            break;
                        case TweenLoopType.Repeat:
                            moveTo._elapsedTime -= moveTo.duration;
                            break;
                        case TweenLoopType.Continuous:
                            var next = moveTo.endValue - moveTo.startValue;
                            moveTo.startValue = moveTo.endValue;
                            moveTo.endValue = moveTo.endValue + next;
                            moveTo._elapsedTime = 0f;
                            break;
                        case TweenLoopType.YoYo:
                            (moveTo.startValue, moveTo.endValue) = (moveTo.endValue, moveTo.startValue);
                            moveTo._elapsedTime = 0f;
                            break;
                    }
                }
            }
        }
    }
}