using _Client_.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Systems
{
    public class FaceToCameraSystem : SFEcsSystem
    {
        private readonly EcsFilterInject<Inc<FaceToCamera, TransformRef>> _faceToCameraFilter = default;
        private readonly EcsFilterInject<Inc<CameraRef, TransformRef>> _cameraFilter = default;

        protected override void Tick(ref IEcsSystems systems)
        {
            foreach (var faceToCameraEntity in _faceToCameraFilter.Value)
            {
                ref var faceToCameraTransformRef = ref _faceToCameraFilter.Pools.Inc2.Get(faceToCameraEntity);

                foreach (var cameraEntity in _cameraFilter.Value)
                {
                    ref var cameraTransformRef = ref _cameraFilter.Pools.Inc2.Get(cameraEntity);
                    faceToCameraTransformRef.reference.rotation = Quaternion.Euler(0, cameraTransformRef.reference.eulerAngles.y, 0);
                }
            }
        }
    }
}