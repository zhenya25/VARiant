using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts
{
    public static class InputActionHandler
    {
        public static InputActions InputActions { get; private set; } = new InputActions();

        public static Vector2 LastTouchPosition { get; private set; }
        
        [InitializeOnLoadMethod]
        private static void Init()
        {
            InputActions.Main.TouchPosition.performed += (ctx) => 
                LastTouchPosition = ctx.ReadValue<Vector2>();
            
            InputActions.Enable();
        }

        public static void SubscribeToClick(Action<InputAction.CallbackContext> action) 
            => InputActions.Main.Touch.performed += action;
        
        public static void UnsubscribeToClick(Action<InputAction.CallbackContext> action) 
            => InputActions.Main.Touch.performed -= action;

    }
}