using System;
using SFramework.ECS.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Client_.Scripts.Components
{
    [Serializable, SFGenerateComponent]
    public struct RigidbodyRef : ISFComponent
    {
        [FormerlySerializedAs("instance")]
        public Rigidbody reference;
    }
}
