using System;
using SFramework.Core.Runtime;
using UnityEngine;

namespace _Client_.Test
{
    [Serializable]
    public class TestContainerB : ISFDatabaseNode
    {
        public string Name => _name;
        public ISFDatabaseNode[] Children => null;
        
        [SerializeField]
        private string _name;
    }
}