using _Client_.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Systems
{
    public class BackpackItemsSystem : SFEcsSystem
    {
        private readonly EcsFilterInject<Inc<Backpack>> _filter;
        private readonly EcsWorldInject _world;
        private readonly EcsPoolInject<RigidbodyRef> _rigidbodyRefPool;
        private readonly EcsPoolInject<MoveTo> _moveToPool;


        protected override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var backpack = ref _filter.Pools.Inc1.Get(entity);

                int index = 0;

                foreach (var itemPackedEntity in backpack.stackPackedEntities)
                {
                    if (itemPackedEntity.Unpack(_world.Value, out var itemEntity))
                    {
                        if (_moveToPool.Value.Has(itemEntity)) continue;

                        ref var rigidbodyRef = ref _rigidbodyRefPool.Value.Get(itemEntity);

                        var offset = new Vector3(0, backpack.itemsSpace * index);
                        var nextPosition = backpack.bottomPoint.position + offset;

                        rigidbodyRef.reference.position = nextPosition;
                        rigidbodyRef.reference.rotation = backpack.bottomPoint.rotation;
                    }

                    index++;
                }
            }
        }
    }
}