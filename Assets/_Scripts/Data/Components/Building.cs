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
        
        private void Awake()
        {
            _buildingDemolishBtn = transform.Find("PF_BuildingDemolishBtn");
            HideBuildingDemolishBtn();
        }
        private void Start()
        {
            _buildingType = GetComponent<BuildingTypeHolder>().buildingType;
            _healthSystem = GetComponent<HealthSystem>();

            _healthSystem.SetHealthAmountMax(_buildingType.healthAmountMax, true);
            _healthSystem.OnDied += HealthSystem_OnDied;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
                _healthSystem.Damage(10);
        }

        private void HealthSystem_OnDied(object sender, EventArgs e)
        {
            Destroy(gameObject);
        }

        private void OnMouseEnter()
        {
            ShowBuildingDemolishBtn();
        }

        private void OnMouseExit()
        {
            HideBuildingDemolishBtn();
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
    }
}
