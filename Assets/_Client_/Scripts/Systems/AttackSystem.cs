using _Client_.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Systems
{
    public class AttackSystem : SFEcsSystem
    {
        private readonly EcsFilterInject<Inc<AttackAbility, TransformRef>> _filter = default;
        private readonly EcsPoolInject<DamageEvent> _damageEventPool = default;
        private readonly EcsPoolInject<Health> _healthPool = default;
        private readonly EcsPoolInject<Restoring> _restoringPool = default;
        private readonly EcsPoolInject<Label> _labelPool = default;
        private readonly EcsWorldInject _world = default;
        private readonly Collider[] attackResults = new Collider[10];

        protected override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var attackAbility = ref _filter.Pools.Inc1.Get(entity);
                ref var transformRef = ref _filter.Pools.Inc2.Get(entity);

                if (attackAbility._attackCooldown > 0f)
                {
                    attackAbility._attackCooldown -= Time.deltaTime;
                    return;
                }

                var attackLayerMask = LayerMask.GetMask(attackAbility.attackLayers);

                var found = Physics.OverlapSphereNonAlloc(transformRef.reference.position, attackAbility.attackRadius,
                    attackResults, attackLayerMask, attackAbility.queryTriggerInteraction);
                
                if (found <= 0) continue;

                for (int i = 0; i < found; i++)
                {
                    if (attackResults[i].attachedRigidbody == null) continue;
                    var rigidbody = attackResults[i].attachedRigidbody;
                    if (SFEntityMappingService.GetEntity(rigidbody.gameObject, _world.Value, out var attackedEntity))
                    {
                        if (entity == attackedEntity || !_healthPool.Value.Has(attackedEntity)) continue;
                        if (_restoringPool.Value.Has(attackedEntity)) continue;

                        _damageEventPool.Value.Add(attackedEntity) = new DamageEvent
                        {
                            amount = attackAbility.damageAmount
                        };

                        attackAbility._attackCooldown = attackAbility.attackCooldown;

                        if (_labelPool.Value.Has(entity))
                        {
                            ref var label = ref _labelPool.Value.Get(entity);

                            label.SetText($"Attack: {attackAbility.damageAmount}");
                        }
                    }
                }
            }
        }
    }
}