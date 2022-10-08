using UnityEngine;

namespace _Project.Scripts
{
    public interface IObjectPlacementMode
    {
        public PlacedObject PlacedObject { get; set; }
        public ObjectProjector ObjectProjector { get; set; }
        public void Update();
        public void Click(Vector2 touchPosition);
    }
}