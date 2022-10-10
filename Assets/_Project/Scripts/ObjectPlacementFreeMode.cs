using UnityEngine;

namespace _Project.Scripts
{
    public class ObjectPlacementFreeMode : IObjectPlacementMode
    {
        public PlacedObject PlacedObject { get; set; }
        public ObjectProjector ObjectProjector { get; set; }
        
        private float _projectorOffsetMultiplier = 2f;
        private Camera _cameraMain;
        private Transform _cameraTransform;
        public ObjectPlacementFreeMode()
        {
            _cameraMain = Camera.main;
            _cameraTransform = _cameraMain.transform;
        }

        public void Update()
        {
            var forward = _cameraTransform.forward;
            var position = _cameraTransform.position + forward * _projectorOffsetMultiplier;
            forward.y = 0;
            forward.Normalize();
            var rotation = Quaternion.LookRotation(-forward);
            
            ObjectProjector.SetPositionAndRotation(position,rotation);
            ObjectProjector.gameObject.SetActive(true);
        }

        public void Click(Vector2 touchPosition)
        {
            PlacedObject.SetPositionAndRotation(ObjectProjector.ModelRoot.position,ObjectProjector.ModelRoot.rotation);
            PlacedObject.gameObject.SetActive(true);
        }

        public void SetDirection(DirectionType directionType)
        {
            ObjectProjector.SetDirection(directionType);
        }

    }
}