using UnityEngine;

namespace _Project.Scripts
{
    public class ObjectPlacementAutoMode : IObjectPlacementMode
    {
        public PlacedObject PlacedObject { get; set; }
        public ObjectProjector ObjectProjector { get; set; }

        public void Update()
        {
            
        }

        public void Click(Vector2 touchPosition)
        {
            Debug.Log($"Click: {touchPosition}");
            
            if (MakeRaycast(out RaycastHit hit, touchPosition))
            {
                PlacedObject.SetTransformByHit(hit);
            }
            
        }

        private bool MakeRaycast(out RaycastHit hit, Vector2 touchPosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            return Physics.Raycast(ray, out hit, Mathf.Infinity);
        }
    }
}