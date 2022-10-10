using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class ObjectProjector : MonoBehaviour
    {
        [field: SerializeField] public MeshFilter MeshFilter { get; private set; }
        [field: SerializeField] public Transform ModelRoot { get; private set; }

        public Bounds ModelBounds => MeshFilter.mesh.bounds;

        public void SetModelLocalPosition(Vector3 position)
        {
            ModelRoot.localPosition = position;
        }

        public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
        {
            transform.SetPositionAndRotation(pos, rot);
        }

        public void SetPosition(Vector3 newPosition)
        {
            transform.position = newPosition;
        }

        public void SetDirection(DirectionType directionType)
        {
            ModelRoot.localRotation = Quaternion.Euler(GetEulerByDirection(directionType));
        }

        private Vector3 GetEulerByDirection(DirectionType directionType)
        {
            return directionType switch
            {
                DirectionType.Right => new Vector3(0, -90, 0),
                DirectionType.Left => new Vector3(0, 90, 0),
                DirectionType.Up => new Vector3(90, 0, 0),
                DirectionType.Down => new Vector3(-90, 0, 0),
                DirectionType.Forward => Vector3.zero,
                DirectionType.Upwards => new Vector3(0, -180, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(directionType), directionType, null)
            };
        }
    }
}