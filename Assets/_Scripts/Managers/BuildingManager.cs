using System;
using _Scripts.Scriptables;
using _Scripts.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Managers
{
    public class BuildingManager : MonoBehaviour
    {
        public static BuildingManager Instance { get; private set; }

        public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

        public class OnActiveBuildingTypeChangedEventArgs : EventArgs
        {
            public BuildingTypeSo ActiveBuildingType;
        }
        
        
        private BuildingTypeListSo _buildingTypeListSo;
        private BuildingTypeSo _activeBuildingType;
        private Camera _mainCamera;

        private void Awake()
        {
            Instance = this;
            _buildingTypeListSo = Resources.Load<BuildingTypeListSo>(nameof(BuildingTypeListSo));
        }

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject()) return;
            if(_activeBuildingType!=null)
                Instantiate(_activeBuildingType.prefab, Utility.GetMouseWorldPosition(), Quaternion.identity);
        }

       

        public void SetBuildingType(BuildingTypeSo buildingType)
        {
            _activeBuildingType = buildingType;

            OnActiveBuildingTypeChanged?.Invoke(this,
                new OnActiveBuildingTypeChangedEventArgs { ActiveBuildingType = _activeBuildingType });
        }

        public BuildingTypeSo GetActiveBuildingType => _activeBuildingType;
    }
}