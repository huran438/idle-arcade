using _Client_.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Systems
{
    public class DamageSystem : SFEcsSystem
    {
        private readonly EcsFilterInject<Inc<Health, DamageEvent>> _filter = default;

        protected override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var health = ref _filter.Pools.Inc1.Get(entity);
                ref var damageEvent = ref _filter.Pools.Inc2.Get(entity);
                health._amount = Mathf.Clamp(health._amount - damageEvent.amount, 0, health.maxAmount);
                _filter.Pools.Inc2.Del(entity);
            }
        }
    }
}