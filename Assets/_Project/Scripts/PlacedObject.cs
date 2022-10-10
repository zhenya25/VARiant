using UnityEngine;

namespace _Project.Scripts
{
    public class PlacedObject : MonoBehaviour
    {
        [field:SerializeField] public MeshRenderer MeshRenderer { get; private set; }

        public void SetPosition(Vector3 newPosition)
        {
            transform.position = newPosition;
        }

        public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
        {
            transform.SetPositionAndRotation(pos,rot);
        }
        
    }
}