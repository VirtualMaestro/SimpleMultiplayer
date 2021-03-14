// GENERATED AUTOMATICALLY FROM 'Assets/Client/Inputs/InputMapping.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMapping : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMapping()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMapping"",
    ""maps"": [
        {
            ""name"": ""PlayerControls"",
            ""id"": ""a01cfa2e-212f-4873-9751-5a373f76d722"",
            ""actions"": [
                {
                    ""name"": ""TrackCursor"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cf59b3e8-1da9-49af-8016-57c2508b6287"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TrackScreenContact"",
                    ""type"": ""Button"",
                    ""id"": ""fab732da-683c-49b4-b4df-35f29a26946b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""dc2b6849-89e6-45b6-bd76-555a4370a559"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseScheme"",
                    ""action"": ""TrackCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c86273b-db71-4e04-83fd-1367def419a2"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""TouchScheme"",
                    ""action"": ""TrackCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a9de7a22-388e-499a-991f-88e0a8a04a88"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""MouseScheme"",
                    ""action"": ""TrackScreenContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8645f08-8187-43e1-bb7b-8d3caa7aba6f"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""TouchScheme"",
                    ""action"": ""TrackScreenContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""TouchScheme"",
            ""bindingGroup"": ""TouchScheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""MouseScheme"",
            ""bindingGroup"": ""MouseScheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerControls
        m_PlayerControls = asset.FindActionMap("PlayerControls", throwIfNotFound: true);
        m_PlayerControls_TrackCursor = m_PlayerControls.FindAction("TrackCursor", throwIfNotFound: true);
        m_PlayerControls_TrackScreenContact = m_PlayerControls.FindAction("TrackScreenContact", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerControls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_TrackCursor;
    private readonly InputAction m_PlayerControls_TrackScreenContact;
    public struct PlayerControlsActions
    {
        private @InputMapping m_Wrapper;
        public PlayerControlsActions(@InputMapping wrapper) { m_Wrapper = wrapper; }
        public InputAction @TrackCursor => m_Wrapper.m_PlayerControls_TrackCursor;
        public InputAction @TrackScreenContact => m_Wrapper.m_PlayerControls_TrackScreenContact;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @TrackCursor.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnTrackCursor;
                @TrackCursor.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnTrackCursor;
                @TrackCursor.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnTrackCursor;
                @TrackScreenContact.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnTrackScreenContact;
                @TrackScreenContact.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnTrackScreenContact;
                @TrackScreenContact.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnTrackScreenContact;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TrackCursor.started += instance.OnTrackCursor;
                @TrackCursor.performed += instance.OnTrackCursor;
                @TrackCursor.canceled += instance.OnTrackCursor;
                @TrackScreenContact.started += instance.OnTrackScreenContact;
                @TrackScreenContact.performed += instance.OnTrackScreenContact;
                @TrackScreenContact.canceled += instance.OnTrackScreenContact;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);
    private int m_TouchSchemeSchemeIndex = -1;
    public InputControlScheme TouchSchemeScheme
    {
        get
        {
            if (m_TouchSchemeSchemeIndex == -1) m_TouchSchemeSchemeIndex = asset.FindControlSchemeIndex("TouchScheme");
            return asset.controlSchemes[m_TouchSchemeSchemeIndex];
        }
    }
    private int m_MouseSchemeSchemeIndex = -1;
    public InputControlScheme MouseSchemeScheme
    {
        get
        {
            if (m_MouseSchemeSchemeIndex == -1) m_MouseSchemeSchemeIndex = asset.FindControlSchemeIndex("MouseScheme");
            return asset.controlSchemes[m_MouseSchemeSchemeIndex];
        }
    }
    public interface IPlayerControlsActions
    {
        void OnTrackCursor(InputAction.CallbackContext context);
        void OnTrackScreenContact(InputAction.CallbackContext context);
    }
}
