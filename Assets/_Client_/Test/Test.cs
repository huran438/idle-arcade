using System;
using SFramework.Core.Runtime;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Test
{
    [Serializable, SFGenerateComponent]
    public struct Test : ISFComponent
    {
        public float FLOAT;
        public int INTEGER;
        public bool BOOLEAN;
        public string STRING;
        public char CHAR;
        public TEST_ENUM ENUM;
        public TEST_ENUM_FLAGS FLAGS;
        public Vector2 VECTOR_2;
        public Vector3 VECTOR_3;
        public Vector4 VECTOR_4;
        public Color COLOR;

        [SFType(typeof(TestDatabase))]
        public string SF_TYPE;

        [SFType(typeof(TestDatabase))]
        public string[] SF_TYPE_ARRAY;
        
        public GameObject GAME_OBJECT;
    }

    public enum TEST_ENUM
    {
        A,
        B,
        C
    }

    [Flags]
    public enum TEST_ENUM_FLAGS
    {
        A,
        B,
        C
    }
}