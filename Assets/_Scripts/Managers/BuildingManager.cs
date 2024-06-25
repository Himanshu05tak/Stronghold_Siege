using System;
using _Scripts.Data.Components;
using _Scripts.ScriptableScript;
using _Scripts.UI;
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

        [SerializeField] private Building hqBuilding;
        
        private BuildingTypeSo _activeBuildingType;
        private BuildingTypeListSo _buildingTypeListSo;
        private void Awake()
        {
            Instance = this;
            _buildingTypeListSo = Resources.Load<BuildingTypeListSo>(nameof(BuildingTypeListSo));
        }
        private void Update()
        {
            SetupBuilding();
        }

        private void SetupBuilding()
        {
            if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject()) return;
            if (_activeBuildingType == null) return;
            if (CanSpawnBuilding(_activeBuildingType, UtilsClass.GetMouseWorldPosition(), out var errorMessage))
            {
                if (ResourceManager.Instance.CanAfford(_activeBuildingType.constructionResourceAmounts))
                {
                    ResourceManager.Instance.SpendResources(_activeBuildingType.constructionResourceAmounts);
                    Instantiate(_activeBuildingType.prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
                }
                else
                    TooltipUI.Instance.Show(
                        "<color=#ffff00>Cannot afford " + _activeBuildingType.GetConstructionResourcesCostString(),
                        new TooltipUI.TooltipTimer { timer = 2f });
            }
            else
            {
                TooltipUI.Instance.Show(errorMessage, new TooltipUI.TooltipTimer { timer = 2f });
            }
        }

        public void SetBuildingType(BuildingTypeSo buildingType)
        {
            _activeBuildingType = buildingType;

            OnActiveBuildingTypeChanged?.Invoke(this,
                new OnActiveBuildingTypeChangedEventArgs { ActiveBuildingType = _activeBuildingType });
        }
        public BuildingTypeSo GetActiveBuildingType => _activeBuildingType;

        private bool CanSpawnBuilding(BuildingTypeSo buildingType, Vector3 position, out string errorMessage)
        {
            var boxCollider = buildingType.prefab.GetComponent<BoxCollider2D>();

            var collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3) boxCollider.offset, boxCollider.size, 0);

            var isAreaClear = collider2DArray.Length == 0;
            if (!isAreaClear)
            {
                errorMessage = "<color=#ff0000>Area is not clear!";
                return false;
            }

            
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
                {
                    errorMessage = "<color=#00ff00>Too close to another building of the same type!";
                    return false;
                }                   
            }
            
            //within maxConstructionRadius
            const float maxConstructionRadius = 25f;
            collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
            foreach (var col in collider2DArray)
            {
                //Colliders inside the construction radius
                var buildingTypeHolder = col.GetComponent<BuildingTypeHolder>();
                if (buildingTypeHolder == null) continue;
                errorMessage = "";
                return true;
            }
            errorMessage = "Too far from any other building!";
            return false;
        }

        public Building GetHqBuilding() => hqBuilding;
    }
}