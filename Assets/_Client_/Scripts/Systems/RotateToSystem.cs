using _Client_.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using SFramework.ECS.Runtime.Tweens.SFramework.Modules.SF_ECS_Tweens.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Systems
{
    public class RotateToSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<RotateTo, RigidbodyRef>> _filter;
        private readonly EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var to = ref _filter.Pools.Inc1.Get(entity);
                ref var rigidbody = ref _filter.Pools.Inc2.Get(entity).reference;

                to._elapsedTime += to.unscaledTime ? Time.fixedUnscaledDeltaTime : Time.fixedDeltaTime;

                var t = Mathf.Clamp((to._elapsedTime - to.cooldown) / to.duration, 0.0f, 1.0f);
                
                rigidbody.rotation = Quaternion.Euler(SFMathFXHelper.CurvedValueECS(to.animationCurve,
                    to.startValue,
                    to.endValue, t));

                if (to._elapsedTime >= to.cooldown + to.duration)
                {
                    switch (to.loopType)
                    {
                        case TweenLoopType.None:
                            rigidbody.rotation = Quaternion.Euler(to.endValue);
                            _filter.Pools.Inc1.Del(entity);
                            break;
                        case TweenLoopType.Repeat:
                            to._elapsedTime -= to.duration;
                            break;
                        case TweenLoopType.Continuous:
                            var next = to.endValue - to.startValue;
                            to.startValue = to.endValue;
                            to.endValue = to.endValue + next;
                            to._elapsedTime = 0f;
                            break;
                        case TweenLoopType.YoYo:
                            (to.startValue, to.endValue) = (to.endValue, to.startValue);
                            to._elapsedTime = 0f;
                            break;
                    }
                }
            }
        }
    }
}