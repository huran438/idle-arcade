using System.Collections.Generic;
using SFramework.Core.Runtime;
using UnityEngine;

namespace _Client_.Test
{
    [CreateAssetMenu]
    public class TestDatabase : ScriptableObject, ISFDatabase, ISFDatabaseGenerator
    {
        public string Title => "Test Database";
        public ISFDatabaseNode[] Nodes => _containers;

        [SerializeField]
        private TestContainerA[] _containers;
        
        public void GetGenerationData(out SFGenerationData[] generationData)
        {
            var prefabs = new HashSet<string>();
            
            foreach (var layer0 in _containers)
            {
                foreach (var layer1 in layer0.Children)
                {
                   prefabs.Add($"{layer0.Name}/{layer1.Name}");
                }
            }

            generationData = new[]
            {
                new SFGenerationData
                {
                    FileName = "SFPrefabs",
                    Properties = prefabs
                }
            };
        }
    }
}