using _Client_.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using SFramework.ECS.Runtime.Tweens.SFramework.Modules.SF_ECS_Tweens.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Systems
{
    public class ResourcePlaceSystem : SFEcsSystem
    {
        private readonly EcsFilterInject<Inc<ResourcePlaceAbility, Backpack, TransformRef>> _filter;
        private readonly Collider[] _collisions = new Collider[10];
        private readonly EcsWorldInject _world;
        private readonly EcsPoolInject<MoveTo> _moveToPool;
        private readonly EcsPoolInject<RotateTo> _rotateToPool;
        private readonly EcsPoolInject<Storage> _storagePool;

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

                if (backpack.stackPackedEntities.Count == 0) continue;

                var layerMask = LayerMask.GetMask(ability.layers);
                var found = Physics.OverlapSphereNonAlloc(transformRef.reference.position, ability.radius, _collisions,
                    layerMask, ability.queryTriggerInteraction);

                for (int i = 0; i < found; i++)
                {
                    var collider = _collisions[i];
                    var gameObject = collider.gameObject;

                    if (!SFEntityMappingService.GetEntityPacked(gameObject, _world.Value, out var storagePackedEntity))
                        continue;
                    if (!storagePackedEntity.Unpack(_world.Value, out var storageEntity)) continue;
                    if (!_storagePool.Value.Has(storageEntity)) continue;
                    ref var storage = ref _storagePool.Value.Get(storageEntity);
                    if (storage.stackPackedEntities.Count >= storage.maxSize) continue;
                    if (backpack.stackPackedEntities.Count == 0) continue;
                    
                    var itemPackedEntity = backpack.stackPackedEntities.Peek();
                    
                    if (itemPackedEntity.Unpack(_world.Value, out var itemEntity))
                    {
                        if (_moveToPool.Value.Has(itemEntity)) continue;
                        if (_rotateToPool.Value.Has(itemEntity)) continue;

                        storage.stackPackedEntities.Push(backpack.stackPackedEntities.Pop());

                        ref var itemRigidbody = ref _filter.Pools.Inc3.Get(itemEntity);

                        _moveToPool.Value.Add(itemEntity) = new MoveTo
                        {
                            cooldown = 0,
                            duration = ability.duration,
                            loopType = TweenLoopType.None,
                            animationCurve = TweenAnimationCurve.EaseInOut,
                            startValue = itemRigidbody.reference.position,
                            endValue = storage.startPoint.position +
                                       storage.direction * storage.itemsSpace * storage.stackPackedEntities.Count,
                            unscaledTime = false
                        };

                        _rotateToPool.Value.Add(itemEntity) = new RotateTo
                        {
                            cooldown = 0,
                            duration = ability.duration,
                            loopType = TweenLoopType.None,
                            animationCurve = TweenAnimationCurve.EaseInOut,
                            startValue = itemRigidbody.reference.rotation.eulerAngles,
                            endValue = Vector3.zero,
                            unscaledTime = false
                        };
                    }

                    ability._cooldown = ability.cooldown;

                    break;
                }
            }
        }
    }
}