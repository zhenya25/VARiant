using UnityEngine;

namespace _Project.Scripts
{
    public class ObjectPlacementFreeMode : IObjectPlacementMode
    {
        public PlacedObject PlacedObject { get; set; }
        public ObjectProjector ObjectProjector { get; set; }
        
        private float _projectorOffsetMultiplier = 2f;
        private Camera _cameraMain;
        public ObjectPlacementFreeMode()
        {
            _cameraMain = Camera.main;
        }

        public void Update()
        {
            var transform = _cameraMain.transform;
            ObjectProjector.SetPosition(transform.position + transform.forward * _projectorOffsetMultiplier);
        }

        public void Click(Vector2 touchPosition)
        {
            PlacedObject.SetPosition(ObjectProjector.transform.position);
        }

        private bool MakeRaycast(out RaycastHit hit, Vector2 touchPosition)
        {
            Ray ray = _cameraMain.ScreenPointToRay(touchPosition);
            return Physics.Raycast(ray, out hit, Mathf.Infinity);
        }
    
    }
}