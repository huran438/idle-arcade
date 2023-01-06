using System;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Components
{
    [Serializable, SFGenerateComponent]
    public struct AttackAbility : ISFComponent
    {
        public string[] attackLayers;
        public int damageAmount;
        public float attackRadius;
        public float attackCooldown;
        public QueryTriggerInteraction queryTriggerInteraction;
        [HideInInspector]
        public float _attackCooldown;
    }
}