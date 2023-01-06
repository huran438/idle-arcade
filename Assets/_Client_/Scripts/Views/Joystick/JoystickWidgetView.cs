using _Client_.Scripts.Services.Input;
using SFramework.Core.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Client_.Scripts.Views.Joystick
{
    public sealed class JoystickWidgetView : SFView, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SFInject]
        private readonly IInputService _inputService;

        [SerializeField]
        private float _handleRange = 1;

        [SerializeField]
        private float _deadZone;

        [SerializeField]
        private AxisOptions _axisOptions = AxisOptions.Both;

        [SerializeField]
        private bool _snapX;

        [SerializeField]
        private bool _snapY;

        [SerializeField]
        private RectTransform _background;

        [SerializeField]
        private RectTransform _handle;

        private RectTransform _baseRect;
        private Canvas _canvas;

        protected override void Init()
        {
            _inputService.HandleRange = _handleRange;
            _inputService.DeadZone = _deadZone;
            _inputService.SnapX = _snapX;
            _inputService.SnapY = _snapY;
            _inputService.AxisOptions = _axisOptions;

            _baseRect = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();

            var center = new Vector2(0.5f, 0.5f);
            _background.pivot = center;
            _handle.anchorMin = center;
            _handle.anchorMax = center;
            _handle.pivot = center;
            _handle.anchoredPosition = Vector2.zero;

            _background.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
            _background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            _background.gameObject.SetActive(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var position = RectTransformUtility.WorldToScreenPoint(null, _background.position);
            var radius = _background.sizeDelta * 0.5f;
            _inputService.Input = (eventData.position - position) / (radius * _canvas.scaleFactor);
            _inputService.FormatInput();
            HandleInput(_inputService.Input.magnitude, _inputService.Input.normalized, radius, null);
            _handle.anchoredPosition = _inputService.Input * radius * _handleRange;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _inputService.Input = Vector2.zero;
            _handle.anchoredPosition = Vector2.zero;
            _background.gameObject.SetActive(false);
        }

        private void HandleInput(float magnitude, Vector2 normalized, Vector2 radius, Camera camera)
        {
            _inputService.HandleInput(magnitude, normalized, radius, camera);
        }

        private Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_baseRect, screenPosition, null,
                    out var localPoint)) return Vector2.zero;
            var pivotOffset = _baseRect.pivot * _baseRect.sizeDelta;
            return localPoint - (_background.anchorMax * _baseRect.sizeDelta) + pivotOffset;
        }
    }
}