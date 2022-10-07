using UnityEngine;

public class ObjectPlacementController : MonoBehaviour
{
    [SerializeField] private BaseObject _placedPrefab;
    [SerializeField] private LayerMask _layerMask;

    public BaseObject SpawnedObject
    {
        get
        {
            if (!_spawnedObject)
                _spawnedObject = Instantiate(_placedPrefab, Vector3.zero, Quaternion.identity);
            return _spawnedObject;
        }
        private set => _spawnedObject = value;
    }

    private BaseObject _spawnedObject;


    private void Update()
    {
        if (Input.touchCount == 0) return;
        Vector2 touchPosition = Input.GetTouch(0).position;

        if (MakeRaycast(out RaycastHit hit, touchPosition))
        {
            SpawnedObject.SetTransformByHit(hit);
        }
    }

    private bool MakeRaycast(out RaycastHit hit, Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        return Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask);
    }
}