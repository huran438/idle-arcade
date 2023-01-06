using UnityEditor;

namespace _Client_.Test.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(TestComponent))]
    public class TestComponentEditor : UnityEditor.Editor
    {
        
    }
}