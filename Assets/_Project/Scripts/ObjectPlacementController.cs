using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class ObjectPlacementController : MonoBehaviour
    {
        [SerializeField] private PlacedObject _placedPrefab;
        [SerializeField] private ObjectProjector _objectProjectorPrefab;
        private ObjectProjector _objectProjector;

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

        private void Awake()
        {
            _objectProjector = Instantiate(_objectProjectorPrefab);
        }

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
            _currentHandlerMode.ObjectProjector = _objectProjector;
        }
    
        private void Start()
        {
            ChangePlacementMode(PlacementType.Free);
        }

        private void Update()
        {
            _currentHandlerMode?.Update();
        }
        
        private void OnClick(Vector2 position)
        {
            _currentHandlerMode.Click(position);
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