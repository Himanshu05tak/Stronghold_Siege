using System;
using UnityEngine;
using _Scripts.ScriptableScript;

namespace _Scripts.Data.Components
{
    public class Building : MonoBehaviour
    {
        private BuildingTypeSo _buildingType;
        private HealthSystem _healthSystem;
        private Transform _buildingDemolishBtn;
        private Transform _buildingRepairBtn;
        
        private void Awake()
        {
            _buildingDemolishBtn = transform.Find("PF_BuildingDemolishBtn");
            _buildingRepairBtn = transform.Find("PF_BuildingRepairBtn");
            HideBuildingDemolishBtn();
            HideBuildingRepairBtn();
        }
        private void Start()
        {
            _buildingType = GetComponent<BuildingTypeHolder>().buildingType;
            _healthSystem = GetComponent<HealthSystem>();

            _healthSystem.SetHealthAmountMax(_buildingType.healthAmountMax, true);
            
            _healthSystem.OnDamaged += HealthSystem_OnDamaged;
            _healthSystem.OnDied += HealthSystem_OnDied;
            _healthSystem.OnHealed += HealthSystem_OnHealed;
        }

        private void HealthSystem_OnHealed(object sender, EventArgs e)
        {
           if(_healthSystem.IsFullHealth())
               HideBuildingRepairBtn();
           else
               ShowBuildingRepairBtn();
        }

        private void HealthSystem_OnDamaged(object sender, EventArgs e)
        {
            ShowBuildingRepairBtn();
        }

        private void HealthSystem_OnDied(object sender, EventArgs e)
        {
            Destroy(gameObject);
        }

        private void OnMouseEnter()
        {
            ShowBuildingDemolishBtn();
            ShowBuildingRepairBtn();
        }

        private void OnMouseExit()
        {
            HideBuildingDemolishBtn();
            HideBuildingRepairBtn();
        }

        private void ShowBuildingDemolishBtn()
        {
            if (_buildingDemolishBtn != null)
                _buildingDemolishBtn.gameObject.SetActive(true);
        }
        
        private void HideBuildingDemolishBtn()
        {
            if (_buildingDemolishBtn != null)
                _buildingDemolishBtn.gameObject.SetActive(false);
        }
        private void ShowBuildingRepairBtn()
        {
            if (_buildingRepairBtn != null)
                _buildingRepairBtn.gameObject.SetActive(true);
        }
        
        private void HideBuildingRepairBtn()
        {
            if (_buildingRepairBtn != null)
                _buildingRepairBtn.gameObject.SetActive(false);
        }
    }
}
