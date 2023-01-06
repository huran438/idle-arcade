using System;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Components
{
    [Serializable, SFGenerateComponent]
    public struct CameraTarget : ISFComponent
    {
        public int priority;
        public Vector3 positionOffset;
        public Vector3 rotationOffset;
    }
}