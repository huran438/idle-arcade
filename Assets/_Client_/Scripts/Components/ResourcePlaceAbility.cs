using System;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Components
{
    [Serializable, SFGenerateComponent]
    public struct ResourcePlaceAbility : ISFComponent
    {
        public string[] layers;
        public float radius;
        public QueryTriggerInteraction queryTriggerInteraction;
        public float duration;
        public float cooldown;
        
        
        [HideInInspector]
        public float _cooldown;
    }
}