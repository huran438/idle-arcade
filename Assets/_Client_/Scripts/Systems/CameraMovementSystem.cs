using _Client_.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Systems
{
    public class CameraMovementSystem : SFEcsSystem
    {
        private readonly EcsFilterInject<Inc<CameraRef, TransformRef>> _filterCameraControl = default;
        private readonly EcsFilterInject<Inc<CameraTarget, TransformRef>> _filterCameraTarget = default;
        
        private readonly EcsPoolInject<CameraRef> _cameraControlPool = default;
        private readonly EcsPoolInject<CameraTarget> _cameraTargetPool = default;
        private readonly EcsPoolInject<TransformRef> _transformRefPool = default;

        protected override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _filterCameraControl.Value)
            {
                ref var transform = ref _transformRefPool.Value.Get(entity).reference;
                ref var cameraControl = ref _cameraControlPool.Value.Get(entity);

                if (!cameraControl._targetTransform)
                {
                    cameraControl._targetPriority = -1;
                    cameraControl._positionOffset = Vector3.zero;
                    cameraControl._rotationOffset = Vector3.zero;
                }
                
                foreach (var cameraTargetEntity in _filterCameraTarget.Value)
                {
                    ref var cameraTargetTransform = ref _transformRefPool.Value.Get(cameraTargetEntity).reference;
                    ref var cameraTarget = ref _cameraTargetPool.Value.Get(cameraTargetEntity);

                    if (cameraTarget.priority > cameraControl._targetPriority)
                    {
                        cameraControl._targetPriority = cameraTarget.priority;
                        cameraControl._targetTransform = cameraTargetTransform;
                        cameraControl._positionOffset = cameraTarget.positionOffset;
                        cameraControl._rotationOffset = cameraTarget.rotationOffset;
                    }
                }
                
                transform.position = cameraControl._targetTransform.position + cameraControl._positionOffset;
                transform.eulerAngles = cameraControl._rotationOffset;
            }
        }
    }
}