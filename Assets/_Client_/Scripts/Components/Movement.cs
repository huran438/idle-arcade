using System;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Components
{
    [Serializable, SFGenerateComponent]
    public struct Movement: ISFComponent
    {
        public float speed;

        [HideInInspector]
        public Vector3 _direction;
    }
}