using UnityEngine;

namespace _Client_.Scripts.Services.Input
{
    public class InputService : IInputService
    {
        public float Horizontal => (SnapX) ? SnapFloat(Input.x, AxisOptions.Horizontal) : Input.x;
        public float Vertical => (SnapY) ? SnapFloat(Input.y, AxisOptions.Vertical) : Input.y;
        public Vector2 Direction => new(Horizontal, Vertical);
        
        private float handleRange;
        public float HandleRange
        {
            get => handleRange;
            set => handleRange = Mathf.Abs(value);
        }

        private float deadZone;
        public float DeadZone
        {
            get => deadZone;
            set => deadZone = Mathf.Abs(value);
        }
        
        private float moveThreshold;
        public float MoveThreshold
        {
            get => moveThreshold;
            set => moveThreshold = Mathf.Abs(value);
        }

        public bool SnapX { get; set; }
        public bool SnapY { get; set; }

        public AxisOptions AxisOptions { get; set; }

        public Vector2 Input { get; set; } = Vector2.zero;
        
        public void FormatInput()
        {
            var vector2 = Input;
            switch (AxisOptions)
            {
                case AxisOptions.Horizontal:
                {
                    vector2.y = 0f;
                    break;
                }

                case AxisOptions.Vertical:
                {
                    vector2.x = 0f;
                    break;
                }
            }

            Input = vector2;
        }
        
        public void HandleInput(float magnitude, Vector2 normalized, Vector2 radius, Camera camera)
        {
            if (magnitude > deadZone)
            {
                if (magnitude > 1)
                {
                    Input = normalized;
                }
            }
            else
            {
                Input = Vector2.zero;
            }
        }

        private float SnapFloat(float value, AxisOptions snapAxis)
        {
            if (value == 0)
                return value;

            if (AxisOptions == AxisOptions.Both)
            {
                float angle = Vector2.Angle(Input, Vector2.up);
                if (snapAxis == AxisOptions.Horizontal)
                {
                    if (angle < 22.5f || angle > 157.5f)
                        return 0;
                    else
                        return (value > 0) ? 1 : -1;
                }
                else if (snapAxis == AxisOptions.Vertical)
                {
                    if (angle > 67.5f && angle < 112.5f)
                        return 0;
                    else
                        return (value > 0) ? 1 : -1;
                }

                return value;
            }
            else
            {
                if (value > 0)
                    return 1;
                if (value < 0)
                    return -1;
            }

            return 0;
        }
    }
}