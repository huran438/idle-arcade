using System;
using SFramework.Core.Runtime;
using UnityEngine;

namespace _Client_.Test
{
    [Serializable]
    public class TestContainerA : ISFDatabaseNode
    {
        public string Name => _name;
        public ISFDatabaseNode[] Children => _nestedContainers;
        
        [SerializeField]
        private string _name;
        
        [SerializeField]
        private TestContainerB[] _nestedContainers;
    }
}