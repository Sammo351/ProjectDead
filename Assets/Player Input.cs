// GENERATED AUTOMATICALLY FROM 'Assets/Player Input.inputactions'

// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine.InputSystem;
// using UnityEngine.InputSystem.Utilities;

// public class @PlayerInput : IInputActionCollection, IDisposable
// {
//     public InputActionAsset asset { get; }
//     public @PlayerInput()
//     {
//         asset = InputActionAsset.FromJson(@"{
//     ""name"": ""Player Input"",
//     ""maps"": [
//         {
//             ""name"": ""Player Input"",
//             ""id"": ""695b55fe-8c54-45d1-a705-444b4348ee9f"",
//             ""actions"": [
//                 {
//                     ""name"": ""Movement"",
//                     ""type"": ""Value"",
//                     ""id"": ""d6c9661c-aecf-434b-8ce7-eb355fafd0f0"",
//                     ""expectedControlType"": ""Vector2"",
//                     ""processors"": ""Clamp(min=-1,max=1)"",
//                     ""interactions"": """"
//                 }
//             ],
//             ""bindings"": [
//                 {
//                     ""name"": ""2D Vector"",
//                     ""id"": ""b0685fde-96c3-4575-96cc-836ecee91b20"",
//                     ""path"": ""2DVector"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Movement"",
//                     ""isComposite"": true,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": ""up"",
//                     ""id"": ""b9f1814b-dd06-4d94-8214-ff2b99d1c9aa"",
//                     ""path"": ""<Keyboard>/w"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": ""Keyboard"",
//                     ""action"": ""Movement"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""down"",
//                     ""id"": ""247bbb57-8dcc-4a13-a950-4d1e6b92dadf"",
//                     ""path"": ""<Keyboard>/s"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": ""Keyboard"",
//                     ""action"": ""Movement"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""left"",
//                     ""id"": ""ff6f275c-25db-4875-a057-c585a1d8698f"",
//                     ""path"": ""<Keyboard>/a"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": ""Keyboard"",
//                     ""action"": ""Movement"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""right"",
//                     ""id"": ""8e359f69-44ba-406b-a603-e6dc09fbdabb"",
//                     ""path"": ""<Keyboard>/d"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": ""Keyboard"",
//                     ""action"": ""Movement"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": """",
//                     ""id"": ""1b4c7dc4-c597-4caa-8a99-e5cceee13116"",
//                     ""path"": ""<DualShockGamepad>/leftStick"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": ""Controller"",
//                     ""action"": ""Movement"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": false
//                 }
//             ]
//         }
//     ],
//     ""controlSchemes"": [
//         {
//             ""name"": ""Keyboard"",
//             ""bindingGroup"": ""Keyboard"",
//             ""devices"": [
//                 {
//                     ""devicePath"": ""<Keyboard>"",
//                     ""isOptional"": false,
//                     ""isOR"": false
//                 },
//                 {
//                     ""devicePath"": ""<Mouse>"",
//                     ""isOptional"": false,
//                     ""isOR"": false
//                 }
//             ]
//         },
//         {
//             ""name"": ""Controller"",
//             ""bindingGroup"": ""Controller"",
//             ""devices"": [
//                 {
//                     ""devicePath"": ""<DualShockGamepad>"",
//                     ""isOptional"": true,
//                     ""isOR"": false
//                 }
//             ]
//         }
//     ]
// }");
//         Player Input
//         m_PlayerInput = asset.FindActionMap("Player Input", throwIfNotFound: true);
//         m_PlayerInput_Movement = m_PlayerInput.FindAction("Movement", throwIfNotFound: true);
//     }

//     public void Dispose()
//     {
//         UnityEngine.Object.Destroy(asset);
//     }

//     public InputBinding? bindingMask
//     {
//         get => asset.bindingMask;
//         set => asset.bindingMask = value;
//     }

//     public ReadOnlyArray<InputDevice>? devices
//     {
//         get => asset.devices;
//         set => asset.devices = value;
//     }

//     public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

//     public bool Contains(InputAction action)
//     {
//         return asset.Contains(action);
//     }

//     public IEnumerator<InputAction> GetEnumerator()
//     {
//         return asset.GetEnumerator();
//     }

//     IEnumerator IEnumerable.GetEnumerator()
//     {
//         return GetEnumerator();
//     }

//     public void Enable()
//     {
//         asset.Enable();
//     }

//     public void Disable()
//     {
//         asset.Disable();
//     }

//     Player Input
//     private readonly InputActionMap m_PlayerInput;
//     private IPlayerInputActions m_PlayerInputActionsCallbackInterface;
//     private readonly InputAction m_PlayerInput_Movement;
//     public struct PlayerInputActions
//     {
//         private @PlayerInput m_Wrapper;
//         public PlayerInputActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
//         public InputAction @Movement => m_Wrapper.m_PlayerInput_Movement;
//         public InputActionMap Get() { return m_Wrapper.m_PlayerInput; }
//         public void Enable() { Get().Enable(); }
//         public void Disable() { Get().Disable(); }
//         public bool enabled => Get().enabled;
//         public static implicit operator InputActionMap(PlayerInputActions set) { return set.Get(); }
//         public void SetCallbacks(IPlayerInputActions instance)
//         {
//             if (m_Wrapper.m_PlayerInputActionsCallbackInterface != null)
//             {
//                 @Movement.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMovement;
//                 @Movement.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMovement;
//                 @Movement.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMovement;
//             }
//             m_Wrapper.m_PlayerInputActionsCallbackInterface = instance;
//             if (instance != null)
//             {
//                 @Movement.started += instance.OnMovement;
//                 @Movement.performed += instance.OnMovement;
//                 @Movement.canceled += instance.OnMovement;
//             }
//         }
//     }
//     public PlayerInputActions @PlayerInput => new PlayerInputActions(this);
//     private int m_KeyboardSchemeIndex = -1;
//     public InputControlScheme KeyboardScheme
//     {
//         get
//         {
//             if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
//             return asset.controlSchemes[m_KeyboardSchemeIndex];
//         }
//     }
//     private int m_ControllerSchemeIndex = -1;
//     public InputControlScheme ControllerScheme
//     {
//         get
//         {
//             if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
//             return asset.controlSchemes[m_ControllerSchemeIndex];
//         }
//     }
//     public interface IPlayerInputActions
//     {
//         void OnMovement(InputAction.CallbackContext context);
//     }
// }
