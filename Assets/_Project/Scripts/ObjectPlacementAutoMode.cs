using UnityEngine;

namespace _Project.Scripts
{
    public class ObjectPlacementAutoMode : IObjectPlacementMode
    {
        public PlacedObject PlacedObject { get; set; }
        public ObjectProjector ObjectProjector { get; set; }
        private Camera _cameraMain;

        public ObjectPlacementAutoMode()
        {
            _cameraMain = Camera.main;
        }
        
        public void Update()
        {
            if (MakeRaycast(out RaycastHit hit, new Vector3(Screen.width / 2f,Screen.height / 2f)))
            {
                ObjectProjector.SetTransformByHit(hit);
            }
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