using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace _Project.Scripts
{
    public class ObjectPlacementController : MonoBehaviour
    {
        [SerializeField] private PlacedObject _samplePrefab;
        [SerializeField] private ObjectProjector _objectProjectorPrefab;
        private ObjectProjector _sampleProjector;
        private PlacedObject _sample;
        private IObjectPlacementMode _currentHandlerMode;

        private void Awake()
        {
#if UNITY_EDITOR
            Initialize();
#else
            ARSession.stateChanged += ArSessionStateChanged;
#endif
        }

        private void ArSessionStateChanged(ARSessionStateChangedEventArgs state)
        {
            if (state.state == ARSessionState.Ready)
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            if (!_sampleProjector) _sampleProjector = Instantiate(_objectProjectorPrefab);
            if (!_sample) _sample = Instantiate(_samplePrefab, Vector3.zero, Quaternion.identity);
            _sample.gameObject.SetActive(false);
            _sampleProjector.gameObject.SetActive(false);
            
            ChangePlacementMode(PlacementType.Auto);
            ChangeDirection(DirectionType.Right);
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
            _currentHandlerMode.PlacedObject = _sample;
            _currentHandlerMode.ObjectProjector = _sampleProjector;
        }

        public void ChangeDirection(DirectionType directionType)
        {
            _currentHandlerMode.SetDirection(directionType);
        }

        private void Update()
        {
            _currentHandlerMode?.Update();
        }

        private void OnClick(Vector2 position)
        {
            if (!InputActionHandler.IsPointerOverUIObject())
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