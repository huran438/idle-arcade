using SFramework.ECS.Runtime;
using SFramework.ECS.Runtime.Tweens.SFramework.Modules.SF_ECS_Tweens.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Components
{
    public struct MoveTo : ISFComponent
    {
        public float cooldown;
        public float duration;
        public TweenLoopType loopType;
        public TweenAnimationCurve animationCurve;
        public Vector3 startValue;
        public Vector3 endValue;
        public bool unscaledTime;
        public float _elapsedTime;
    }
}