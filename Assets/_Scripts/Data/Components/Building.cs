using System;
using _Scripts.ScriptableScript;
using UnityEngine;

namespace _Scripts.Data.Components
{
    public class Building : MonoBehaviour
    {
        private BuildingTypeSo _buildingType;
        private HealthSystem _healthSystem;

        private void Awake()
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
    }
}
