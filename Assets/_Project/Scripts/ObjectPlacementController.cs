using System;
using UnityEngine;

public class ObjectPlacementController : MonoBehaviour
{
    [SerializeField] private PlacedObject _placedPrefab;

    public PlacedObject SpawnedObject
    {
        get
        {
            if (!_spawnedObject)
                _spawnedObject = Instantiate(_placedPrefab, Vector3.zero, Quaternion.identity);
            return _spawnedObject;
        }
        private set => _spawnedObject = value;
    }

    private PlacedObject _spawnedObject;
    private IObjectPlacementMode _currentHandlerMode;
    private IObjectPlacementMode GetPlacementModeByType(PlacementType type)
    {
        return type switch
        {
            PlacementType.Free => new ObjectPlacementFreeMode(),
            PlacementType.Auto => new ObjectPlacementAutoMode(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public void ChangePlacementMode(PlacementType type)
    {
        _currentHandlerMode = GetPlacementModeByType(type);
        _currentHandlerMode.PlacedObject = SpawnedObject;
    }
    
    private void Start()
    {
        ChangePlacementMode(PlacementType.Free);
    }

    private void Update()
    {
        _currentHandlerMode?.Update();
    }

    
}


public enum PlacementType
{
    Free,
    Auto
}

public interface IObjectPlacementMode
{
    public PlacedObject PlacedObject { get; set; }
    public void Update();
}


public class ObjectPlacementFreeMode : IObjectPlacementMode
{
    public PlacedObject PlacedObject { get; set; }
    public void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        Vector2 touchPosition = Input.mousePosition;

        if (MakeRaycast(out RaycastHit hit, touchPosition))
        {
            PlacedObject.SetTransformByHit(hit);
        }
    }
    
    private bool MakeRaycast(out RaycastHit hit, Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        return Physics.Raycast(ray, out hit, Mathf.Infinity);
    }
    
}

public class ObjectPlacementAutoMode : IObjectPlacementMode
{
    public PlacedObject PlacedObject { get; set; }
    public void Update()
    {
        if (Input.touchCount == 0) return;
        Vector2 touchPosition = Input.GetTouch(0).position;

        if (MakeRaycast(out RaycastHit hit, touchPosition))
        {
            PlacedObject.SetTransformByHit(hit);
        }
    }
    
    private bool MakeRaycast(out RaycastHit hit, Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        return Physics.Raycast(ray, out hit, Mathf.Infinity);
    }
}


