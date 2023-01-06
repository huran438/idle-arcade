using System;
using SFramework.ECS.Runtime;
using TMPro;

namespace _Client_.Scripts.Components
{
    [Serializable, SFGenerateComponent]
    public struct Label : ISFComponent
    {
        public TextMeshPro textMesh;
        
        public void SetText(string text)
        {
            textMesh.SetText(text);
        }
    }
}