using UnityEngine;

namespace _Project.Scripts
{
    public class ObjectPlacementAutoMode : IObjectPlacementMode
    {
        public PlacedObject PlacedObject { get; set; }
        public void Update()
        {
            // if (Input.touchCount == 0) return;
            // Vector2 touchPosition = Input.GetTouch(0).position;
            //
            // if (MakeRaycast(out RaycastHit hit, touchPosition))
            // {
            //     PlacedObject.SetTransformByHit(hit);
            // }
        }

        public void Click(Vector2 touchPosition)
        {
            Debug.Log($"Click: {touchPosition}");
        }

        private bool MakeRaycast(out RaycastHit hit, Vector2 touchPosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            return Physics.Raycast(ray, out hit, Mathf.Infinity);
        }
    }
}