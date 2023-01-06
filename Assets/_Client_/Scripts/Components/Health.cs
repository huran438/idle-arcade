using System;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Components
{
    [Serializable, SFGenerateComponent]
    public struct Health : ISFComponent, IEcsAutoInit<Health>
    {
        public int maxAmount;
        public float restoreCooldown;
        
        [HideInInspector]
        public int _amount;
        
        [HideInInspector]
        public float _restoreCooldown;
        
        public void AutoInit(ref Health c)
        {
            c._amount = c.maxAmount;
            c._restoreCooldown = c.restoreCooldown;
        }
    }
}