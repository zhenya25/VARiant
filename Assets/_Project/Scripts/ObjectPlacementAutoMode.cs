using UnityEngine;

namespace _Project.Scripts
{
    public class ObjectPlacementAutoMode : IObjectPlacementMode
    {
        public PlacedObject PlacedObject { get; set; }
        public ObjectProjector ObjectProjector { get; set; }
        private Camera _cameraMain;
        private Transform _cameraTransform;
        
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;
        private Vector3 _targetLocalOffset;
        public ObjectPlacementAutoMode()
        {
            _cameraMain = Camera.main;
            _cameraTransform = _cameraMain.transform;
        }

        public void Update()
        {
            if (!MakeRaycast(out RaycastHit hit)) return;

            Vector3 direction;
            Vector3 localOffset;
            var dotUp = Vector3.Dot(Vector3.up, hit.normal);
            var dotDown = Vector3.Dot(-Vector3.up, hit.normal);

            if (dotUp > 0.5f)
            {
                var cameraForward = _cameraMain.transform.forward;
                direction = new Vector3(-cameraForward.x, 0, -cameraForward.z);
                localOffset = new Vector3(0,
                    ObjectProjector.ModelBounds.extents.y * ObjectProjector.ModelRoot.localScale.y, 0);
            }
            else if (dotDown > 0.5f)
            {
                var cameraForward = _cameraMain.transform.forward;
                direction = new Vector3(-cameraForward.x, 0, -cameraForward.z);
                localOffset = new Vector3(0,
                    -ObjectProjector.ModelBounds.extents.y * ObjectProjector.ModelRoot.localScale.y, 0);
            }
            else
            {
                localOffset = new Vector3(0, 0,
                    ObjectProjector.ModelBounds.extents.z * ObjectProjector.ModelRoot.localScale.z);
                direction = new Vector3(hit.normal.x, 0, hit.normal.z);
            }

            var position = hit.point;
            var rotation = Quaternion.LookRotation(direction);
            
            ObjectProjector.SetPositionAndRotation(position, rotation);
            ObjectProjector.SetModelLocalPosition(localOffset);
            ObjectProjector.gameObject.SetActive(true);
        }

        public void Click(Vector2 touchPosition)
        {
            var transform = ObjectProjector.ModelRoot;
            PlacedObject.SetPositionAndRotation(transform.position, transform.rotation);
            PlacedObject.gameObject.SetActive(true);
        }

        public void SetDirection(DirectionType directionType)
        {
            ObjectProjector.SetDirection(directionType);
        }

        private bool MakeRaycast(out RaycastHit hit)
        {
#if UNITY_EDITOR
            Debug.DrawRay(_cameraTransform.position, _cameraTransform.forward * 10f, Color.magenta);
#endif
            return Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit, Mathf.Infinity);
        }
    }
}