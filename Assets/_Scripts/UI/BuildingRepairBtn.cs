using UnityEngine;
using UnityEngine.UI;
using _Scripts.Data.Components;
using _Scripts.Managers;
using _Scripts.ScriptableScript;

namespace _Scripts.UI
{
    public class BuildingRepairBtn : MonoBehaviour
    {
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private ResourceTypeSo goldResourceType;

        private void Awake()
        {
            transform.Find("button").GetComponent<Button>().onClick.AddListener(() =>
            {
                var missingHealth = healthSystem.GetHealthAmountMax() - healthSystem.GetHealthAmount();
                var repairCost = missingHealth / 2;

                var resourceAmountCost = new ResourceAmount[]
                {
                    new ResourceAmount() { resourceType = goldResourceType, amount = repairCost }
                };
                if (ResourceManager.Instance.CanAfford(resourceAmountCost))
                {
                    //Can afford the repairs
                    ResourceManager.Instance.SpendResources(resourceAmountCost);
                    healthSystem.HealFull();
                }
                else
                {
                    //We can't afford the repairs
                    TooltipUI.Instance.Show("Cannot afford repair cost!", new TooltipUI.TooltipTimer() { timer = 2f });
                }
            });
        }
    }
}