using UnityEngine;

namespace _Project.Scripts
{
    public class ObjectProjector : MonoBehaviour
    {
        public void SetPosition(Vector3 newPosition)
        {
            transform.position = newPosition;
        }

        public void SetTransformByHit(RaycastHit hit)
        {
            SetPosition(hit.point);   
        }
        
    }
}