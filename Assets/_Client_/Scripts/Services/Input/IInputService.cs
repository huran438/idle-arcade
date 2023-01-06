using SFramework.Core.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Services.Input
{
    public interface IInputService : ISFService
    {
        public float Horizontal { get; }
        public float Vertical { get; }
        public Vector2 Direction { get; }
        public float HandleRange { get; set; }
        public float DeadZone { get; set; }
        public AxisOptions AxisOptions { get; set; }
        public bool SnapX { get; set; }
        public bool SnapY { get; set; }
        public Vector2 Input { get; set; }
        public float MoveThreshold { get; set; }

        void FormatInput();
        void HandleInput(float magnitude, Vector2 normalized, Vector2 radius, Camera camera);
    }
}
