using _Client_.Scripts.Components;
using _Client_.Scripts.Services.Input;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.Core.Runtime;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Systems
{
    public class PlayerMovementSystem : SFEcsSystem
    {
        [SFInject]
        private IInputService _inputService;

        private readonly EcsFilterInject<Inc<Player, Movement, TransformRef>> _filter = default;

        protected override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var player = ref _filter.Pools.Inc1.Get(entity);
                ref var movement = ref _filter.Pools.Inc2.Get(entity);
                ref var transform = ref _filter.Pools.Inc3.Get(entity).reference;


                var cameraTransform = Camera.main.transform;

                var cameraForward = cameraTransform.forward;
                cameraForward.y = 0f;

                movement._direction = (_inputService.Horizontal * cameraTransform.right +
                                       _inputService.Vertical * cameraForward).normalized;

                if (movement._direction != Vector3.zero)
                    transform.rotation = Quaternion.LookRotation(movement._direction);

                var motion = movement._direction * movement.speed;
                player.characterController.Move(motion * Time.fixedDeltaTime);
            }
        }
    }
}