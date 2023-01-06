using _Client_.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Systems
{
    public class HealthSystem : SFEcsSystem
    {
        private readonly EcsFilterInject<Inc<Health>> _filter = default;
        private readonly EcsPoolInject<Restoring> _restoringPool = default;
        private readonly EcsPoolInject<Label> _labelPool = default;

        protected override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var health = ref _filter.Pools.Inc1.Get(entity);

                if (health._amount == 0)
                {
                    if (Mathf.Approximately(health.restoreCooldown, health._restoreCooldown))
                    {
                        if (_labelPool.Value.Has(entity))
                        {
                            ref var label = ref _labelPool.Value.Get(entity);
                            
                            label.SetText("<color=red>Death</color>");
                        }
                    }
                    
                    _restoringPool.Value.Replace(entity);

                    if (health._restoreCooldown > 0)
                    {
                        health._restoreCooldown -= Time.deltaTime;
                        continue;
                    }

                    _restoringPool.Value.Del(entity);
                    health._amount = health.maxAmount;
                    health._restoreCooldown = health.restoreCooldown;
                }
            }
        }
    }
}