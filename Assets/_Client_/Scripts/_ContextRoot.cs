using _Client_.Scripts.Services.Input;
using _Client_.Scripts.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.Core.Runtime;
using SFramework.ECS.Runtime;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Client_.Scripts
{
    public class _ContextRoot : SFContextRoot
    {
        [SerializeField]
        private AssetReferenceGameObject[] _debugObjects;

        [SerializeField]
        private ScriptableObject[] _dataBindings;

        private EcsSystems _fixedUpdateSystems;
        private EcsSystems _updateSystems;
        private EcsSystems _lateUpdateSystems;
        private EcsSystems _debugSystems;

        protected override void PreInit()
        {
            Application.targetFrameRate = Screen.currentResolution.refreshRate;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

#if UNITY_EDITOR
            foreach (var assetReferenceGameObject in _debugObjects)
            {
                assetReferenceGameObject.InstantiateAsync();
            }
#endif
        }

        protected override void Setup(SFContainer container)
        {
            // Bind Data
            foreach (var d in _dataBindings)
            {
                container.Bind(d);
            }


            // Bind Services
            container.Bind<ISFWorldsService>(new SFWorldsService());
            container.Bind<IInputService>(new InputService());
        }

        protected override void Init(ISFContainer container)
        {
            var _worldsService = container.Resolve<ISFWorldsService>();

            _fixedUpdateSystems = new EcsSystems(_worldsService.Default, container);
            _updateSystems = new EcsSystems(_worldsService.Default, container);
            _lateUpdateSystems = new EcsSystems(_worldsService.Default, container);

            _fixedUpdateSystems
                .Add(new PlayerMovementSystem())
                .Add(new ResourceGrabSystem())
                .Add(new ResourcePlaceSystem())
                .Add(new MoveToSystem())
                .Add(new RotateToSystem())
                .Add(new AttackSystem())
                .Add(new BackpackItemsSystem())
                .Inject()
                .Init();

            _updateSystems
                .Add(new DamageSystem())
                .Add(new HealthSystem())
                .Add(new FaceToCameraSystem())
                .Inject()
                .Init();

            _lateUpdateSystems
                .Add(new CameraMovementSystem())
                .Inject()
                .Init();

#if UNITY_EDITOR
            _debugSystems = new EcsSystems(_worldsService.Default);
            _debugSystems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem()).Init();
#endif
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        private void Update()
        {
            _updateSystems?.Run();
        }

        private void LateUpdate()
        {
            _lateUpdateSystems?.Run();


#if UNITY_EDITOR
            _debugSystems.Run();
#endif
        }

        private void OnDestroy()
        {
            if (_fixedUpdateSystems != null)
            {
                _fixedUpdateSystems.Destroy();
                _fixedUpdateSystems = null;
            }

            if (_updateSystems != null)
            {
                _updateSystems.Destroy();
                _updateSystems = null;
            }

            if (_lateUpdateSystems != null)
            {
                _lateUpdateSystems.Destroy();
                _lateUpdateSystems = null;
            }

            if (_debugSystems != null)
            {
                _debugSystems.Destroy();
                _debugSystems = null;
            }
        }
    }
}