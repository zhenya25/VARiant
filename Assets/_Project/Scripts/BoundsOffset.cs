using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public struct BoundsOffset
    {
        public Dictionary<DirectionType, Vector3> Offset { get; private set; }

        public BoundsOffset(Bounds bounds)
        {
            Offset = new Dictionary<DirectionType, Vector3>();
        
            Offset.Add(DirectionType.Right,bounds.center + new Vector3(bounds.extents.x,0 ,0));
            Offset.Add(DirectionType.Left,bounds.center + new Vector3(-bounds.extents.x,0 ,0));
            Offset.Add(DirectionType.Up,bounds.center + new Vector3(0,bounds.extents.y ,0));
            Offset.Add(DirectionType.Down,bounds.center + new Vector3(0,-bounds.extents.y ,0));
            Offset.Add(DirectionType.Forward,bounds.center + new Vector3(0,0 ,bounds.extents.z));
            Offset.Add(DirectionType.Upwards,bounds.center + new Vector3(0,0 ,-bounds.extents.z));
        }
    
    }
}