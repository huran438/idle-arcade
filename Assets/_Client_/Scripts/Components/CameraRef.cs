using System;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Components
{
    [Serializable, SFGenerateComponent]
    public struct CameraRef : ISFComponent
    {
        public Camera reference;
        
        [HideInInspector]
        public int _targetPriority;

        [HideInInspector]
        public Vector3 _positionOffset;

        [HideInInspector]
        public Vector3 _rotationOffset;

        [HideInInspector]
        public Transform _targetTransform;
    }
}