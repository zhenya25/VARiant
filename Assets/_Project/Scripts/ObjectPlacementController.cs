using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts
{
    public class ObjectPlacementController : MonoBehaviour
    {
        [SerializeField] private PlacedObject _placedPrefab;

        public PlacedObject SpawnedObject
        {
            get
            {
                if (!_spawnedObject)
                    _spawnedObject = Instantiate(_placedPrefab, Vector3.zero, Quaternion.identity);
                return _spawnedObject;
            }
            private set => _spawnedObject = value;
        }

        private PlacedObject _spawnedObject;
        private IObjectPlacementMode _currentHandlerMode;
        private IObjectPlacementMode GetPlacementModeByType(PlacementType type)
        {
            return type switch
            {
                PlacementType.Free => new ObjectPlacementFreeMode(),
                PlacementType.Auto => new ObjectPlacementAutoMode(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public void ChangePlacementMode(PlacementType type)
        {
            _currentHandlerMode = GetPlacementModeByType(type);
            _currentHandlerMode.PlacedObject = SpawnedObject;
        }
    
        private void Start()
        {
            ChangePlacementMode(PlacementType.Free);
        }

        private void Update()
        {
            _currentHandlerMode?.Update();
        }
        
        private void OnClick(InputAction.CallbackContext ctx)
        {
            _currentHandlerMode.Click(InputActionHandler.LastTouchPosition);
        }
        private void OnEnable()
        {
            InputActionHandler.SubscribeToClick(OnClick); 
        }

        private void OnDisable()
        {
            InputActionHandler.UnsubscribeToClick(OnClick); 
        }
    }
}