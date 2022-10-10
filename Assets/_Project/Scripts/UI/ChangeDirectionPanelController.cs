using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class ChangeDirectionPanelController : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private ObjectPlacementController _placementController;

        private void Awake()
        {
            FillDropdownMenu();
            _dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }

        private void FillDropdownMenu()
        {
            _dropdown.ClearOptions();
            var options = Enum.GetNames(typeof(DirectionType)).ToList();
            _dropdown.AddOptions(options);
        }
        
        private void OnDropdownValueChanged(int value)
        {
            _placementController.ChangeDirection((DirectionType)value);
        }
    }
}