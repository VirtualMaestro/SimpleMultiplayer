using Client.Scripts.Inputs.Components;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Logging;
using StubbUnity.Unity.View;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Client.Scripts.Inputs
{
    public class InputManager : EcsViewLink
    {
        private const float DragThreshold = 10.0f;
        
        private Vector2 _prevScreenPosition;
        private Vector2 _deltaScreenPosition;
        private bool _isDragging;
        private bool _isPressed;

        private Vector2 _startDragPoint;

        public void TrackPosition(InputAction.CallbackContext context)
        {
            var currentScreenPosition = context.ReadValue<Vector2>();
            _deltaScreenPosition = _prevScreenPosition - currentScreenPosition;
            _prevScreenPosition = currentScreenPosition;

            _deltaScreenPosition.x = Mathf.Clamp(_deltaScreenPosition.x, -1, 1);
            _deltaScreenPosition.y = Mathf.Clamp(_deltaScreenPosition.y, -1, 1);
            
            if (_isPressed && _isDragging == false)
            {
                var diff = _prevScreenPosition - _startDragPoint;
                _isDragging = diff.magnitude > DragThreshold;            
            }
            else if (_isDragging)
            {
                _Drag(_deltaScreenPosition.x, _deltaScreenPosition.y);
            }
        }

        public void TrackScreenContact(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _startDragPoint = _prevScreenPosition;
                _isPressed = true;
            }
            else if (context.performed)
            {
                if (_isDragging == false)
                {
                    World.NewEntity().Get<ScreenClickEvent>().Position = _prevScreenPosition;           
                }
                
                _isPressed = false;
                _isDragging = false;
            }
        }

        private void _Drag(float x, float y)
        {
            ref var dragEvent = ref World.NewEntity().Get<DragEvent>();
            dragEvent.DeltaX = x;
            dragEvent.DeltaY = y;
        }

        #region Device events

        public void DeviceLost(PlayerInput input)
        {
            log.Warn($"Device lost!");
        }

        public void DeviceRegained(PlayerInput input)
        {
            log.Warn($"Device regained!");
        }

        public void DeviceChanged(PlayerInput input)
        {
            log.Warn($"Device changed!");
        }

        #endregion
    }
}