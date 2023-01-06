using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Components
{
    [Serializable, SFGenerateComponent]
    public struct Storage : ISFComponent, IEcsAutoInit<Storage>
    {
        public int maxSize;
        public float itemsSpace;
        public Transform startPoint;
        public Vector3 direction;
        public Stack<EcsPackedEntity> stackPackedEntities;
        public void AutoInit(ref Storage c)
        {
            c.stackPackedEntities = new Stack<EcsPackedEntity>();
        }
    }
}