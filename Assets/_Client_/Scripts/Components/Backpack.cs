using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Components
{
    [Serializable, SFGenerateComponent]
    public struct Backpack : ISFComponent, IEcsAutoInit<Backpack>
    {
        public Transform bottomPoint;
        public float itemsSpace;
        public int maxSize;
        public Stack<EcsPackedEntity> stackPackedEntities;
        public void AutoInit(ref Backpack c)
        {
            c.stackPackedEntities = new Stack<EcsPackedEntity>();
        }
    }
}