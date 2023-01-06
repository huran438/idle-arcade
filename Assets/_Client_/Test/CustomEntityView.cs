using System;
using UnityEngine;

namespace _Client_.Test
{
    public class CustomEntityView : MonoBehaviour
    {
        public TestComponent[] Components = Array.Empty<TestComponent>();
    }

    [Serializable]
    public class TestComponent
    {
        public string Type;
        public string Data;
    }
}