using System;
using ScriptableObjects;
using UnityEngine;

namespace Manager
{
    public class BuilderManager : MonoBehaviour
    {
        private BuildingTypeListSo _buildingTypeListSo;
        private BuildingTypeSo _buildingType;
        private Camera _mainCamera;

        private void Awake()
        {
            _buildingTypeListSo = Resources.Load<BuildingTypeListSo>(nameof(BuildingTypeListSo));
            _buildingType = _buildingTypeListSo.list[0];
        }

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                Instantiate(_buildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);

            if (Input.GetKeyDown(KeyCode.T))
                _buildingType = _buildingTypeListSo.list[1];
            if (Input.GetKeyDown(KeyCode.Y))
                _buildingType = _buildingTypeListSo.list[0];
        }

        private Vector3 GetMouseWorldPosition()
        {
            var mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;
            return mouseWorldPosition;
        }
    }
}