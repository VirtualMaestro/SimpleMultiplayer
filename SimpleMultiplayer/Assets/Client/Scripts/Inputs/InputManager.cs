using System;
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
        private Vector2 _prevScreenPosition;
        private Vector2 _deltaScreenPosition;
        private bool _isDragging;

        public void TrackPosition(InputAction.CallbackContext context)
        {
            var currentScreenPosition = context.ReadValue<Vector2>();
            _deltaScreenPosition = _prevScreenPosition - currentScreenPosition;
            _prevScreenPosition = currentScreenPosition;

            _deltaScreenPosition.x = Mathf.Clamp(_deltaScreenPosition.x, -1, 1);
            _deltaScreenPosition.y = Mathf.Clamp(_deltaScreenPosition.y, -1, 1);
            
            Debug.Log($"_deltaScreenPosition: {_deltaScreenPosition}");
        }

        public void TrackScreenContact(InputAction.CallbackContext context)
        {
            if (context.performed && _isDragging == false)
                World.NewEntity().Get<ScreenClickEvent>().Position = _prevScreenPosition;
        }

        public void DragMap(InputAction.CallbackContext context)
        {
            if (context.performed)
                _isDragging = true;
            else if (context.canceled)
                _isDragging = false;
        }

        private void Update()
        {
            if (_isDragging && (_deltaScreenPosition.x != 0 || _deltaScreenPosition.y != 0))
            {
                log.Info($"isDrugging");
                ref var dragEvent = ref World.NewEntity().Get<DragEvent>();
                dragEvent.DeltaX = _deltaScreenPosition.x;
                dragEvent.DeltaY = _deltaScreenPosition.y;
                
                _deltaScreenPosition.Set(0,0);
            }
        }

        #region Device events

        public void DeviceLost(PlayerInput input)
        {
            Debug.Log($"Device lost!");
        }

        public void DeviceRegained(PlayerInput input)
        {
            Debug.Log($"Device regained!");
        }

        public void DeviceChanged(PlayerInput input)
        {
            Debug.Log($"Device changed!");
        }

        #endregion
    }
}