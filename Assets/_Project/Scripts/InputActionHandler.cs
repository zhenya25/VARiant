﻿using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts
{
    public static class InputActionHandler
    {
        public static InputActions InputActions { get; private set; } = new InputActions();
        public static Vector2 LastTouchPosition { get; private set; }
        private static Action<Vector2> OnClick;
        
        [InitializeOnLoadMethod]
        private static void Init()
        {
            InputActions.Main.TouchPosition.performed += (ctx) 
                => LastTouchPosition = ctx.ReadValue<Vector2>();

            InputActions.Main.Touch.performed += (ctx) 
                => OnClick?.Invoke(LastTouchPosition);
            
            InputActions.Enable();
        }

        public static void SubscribeToClick(Action<Vector2> action) 
            => OnClick += action;
        
        public static void UnsubscribeToClick(Action<Vector2> action) 
            => OnClick -= action;

    }
}