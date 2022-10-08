using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    [SerializeField] protected MeshRenderer _meshRenderer;

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
    
    public void SetTransformByHit(RaycastHit hit)
    {
        // var offset = _meshRenderer.bounds.ClosestPoint(transform.position - hit.point);
        SetPosition(hit.point);
    }
    
}