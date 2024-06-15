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
        
        private BuildingTypeSo _activeBuildingType;
        private BuildingTypeListSo _buildingTypeListSo;
        private void Awake()
        {
            Instance = this;
            _buildingTypeListSo = Resources.Load<BuildingTypeListSo>(nameof(BuildingTypeListSo));
        }
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject()) return;
            if(_activeBuildingType!=null && CanSpawnBuilding(_activeBuildingType,Utility.GetMouseWorldPosition()))
                Instantiate(_activeBuildingType.prefab, Utility.GetMouseWorldPosition(), Quaternion.identity);
        }
        public void SetBuildingType(BuildingTypeSo buildingType)
        {
            _activeBuildingType = buildingType;

            OnActiveBuildingTypeChanged?.Invoke(this,
                new OnActiveBuildingTypeChangedEventArgs { ActiveBuildingType = _activeBuildingType });
        }
        public BuildingTypeSo GetActiveBuildingType => _activeBuildingType;

        private bool CanSpawnBuilding(BuildingTypeSo buildingType, Vector3 position)
        {
            var boxCollider = buildingType.prefab.GetComponent<BoxCollider2D>();

            var collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3) boxCollider.offset, boxCollider.size, 0);

            var isAreaClear = collider2DArray.Length == 0;
            if (!isAreaClear) return false;
            
            //within minConstructionRadius
            collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
            foreach (var col in collider2DArray)
            {
                //Colliders inside the construction radius
                var buildingTypeHolder = col.GetComponent<BuildingTypeHolder>();
                if (buildingTypeHolder == null) continue;
                //Has a buildingTypeHolder
                if (buildingTypeHolder.buildingType == buildingType)
                    //There's already a building of this type within the construction radius.
                    return false;
            }
            
            //within maxConstructionRadius
            const float maxConstructionRadius = 25f;
            collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
            foreach (var col in collider2DArray)
            {
                //Colliders inside the construction radius
                var buildingTypeHolder = col.GetComponent<BuildingTypeHolder>();
                if (buildingTypeHolder != null) return true;
            }
            return false;
        }
    }
}