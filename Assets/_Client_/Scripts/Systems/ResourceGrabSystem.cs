using _Client_.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using SFramework.ECS.Runtime.Tweens.SFramework.Modules.SF_ECS_Tweens.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Systems
{
    public class ResourceGrabSystem : SFEcsSystem
    {
        private readonly EcsFilterInject<Inc<ResourceGrabAbility, Backpack, TransformRef>> _filter;
        private readonly Collider[] _collisions = new Collider[10];
        private readonly EcsWorldInject _world;
        private readonly EcsPoolInject<MoveTo> _moveToPool;
        private readonly EcsPoolInject<CanGrab> _canGrabPool;

        protected override void Tick(ref IEcsSystems systems)
        {
            foreach (var abilityEntity in _filter.Value)
            {
                ref var ability = ref _filter.Pools.Inc1.Get(abilityEntity);
                ref var backpack = ref _filter.Pools.Inc2.Get(abilityEntity);
                ref var transformRef = ref _filter.Pools.Inc3.Get(abilityEntity);

                if (ability._cooldown > 0)
                {
                    ability._cooldown -= Time.fixedDeltaTime;
                    continue;
                }

                if (backpack.stackPackedEntities.Count >= backpack.maxSize) continue;

                var layerMask = LayerMask.GetMask(ability.layers);
                var found = Physics.OverlapSphereNonAlloc(transformRef.reference.position, ability.radius, _collisions,
                    layerMask, ability.queryTriggerInteraction);

                for (int i = 0; i < found; i++)
                {
                    var collider = _collisions[i];
                    var gameObject = collider.gameObject;

                    if (!SFEntityMappingService.GetEntityPacked(gameObject, _world.Value, out var itemPackedEntity))
                        continue;
                    if (!itemPackedEntity.Unpack(_world.Value, out var itemEntity)) continue;

                    if (!_canGrabPool.Value.Has(itemEntity)) continue;
                    if (_moveToPool.Value.Has(itemEntity)) continue;

                    if (backpack.stackPackedEntities.Contains(itemPackedEntity)) continue;
                    backpack.stackPackedEntities.Push(itemPackedEntity);
                    ref var itemRigidbody = ref _filter.Pools.Inc3.Get(itemEntity);
                    _moveToPool.Value.Add(itemEntity) = new MoveTo
                    {
                        cooldown = 0,
                        duration = ability.duration,
                        loopType = TweenLoopType.None,
                        animationCurve = TweenAnimationCurve.EaseInOut,
                        startValue = itemRigidbody.reference.position,
                        endValue = backpack.bottomPoint.position +
                                   new Vector3(0, backpack.itemsSpace * backpack.stackPackedEntities.Count),
                        unscaledTime = false
                    };
                    
                    _canGrabPool.Value.Del(itemEntity);

                    ability._cooldown = ability.cooldown;
                    
                    break;
                }
            }
        }
    }
}