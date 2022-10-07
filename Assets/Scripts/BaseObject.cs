using UnityEngine;

public class BaseObject : MonoBehaviour
{
    [SerializeField] protected MeshRenderer _meshRenderer;

    public void SetTransformByHit(RaycastHit hit)
    {
        // var offset = _meshRenderer.bounds.ClosestPoint(transform.position - hit.point);
        transform.position = hit.point;
    }
    
}