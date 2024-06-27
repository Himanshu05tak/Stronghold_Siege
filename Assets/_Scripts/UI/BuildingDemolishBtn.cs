using UnityEngine;
using UnityEngine.UI;
using _Scripts.Data.Components;
using _Scripts.Managers;

namespace _Scripts.UI
{
    public class DemolishBtn : MonoBehaviour
    {
        [SerializeField] private Building building;
        private void Awake()
        {
            transform.Find("button").GetComponent<Button>().onClick.AddListener(() =>
            {
                var buildingType = building.GetComponent<BuildingTypeHolder>().buildingType;
                foreach (var resourceAmount in buildingType.constructionResourceAmounts)
                {
                    ResourceManager.Instance.AddResource(resourceAmount.resourceType,
                        Mathf.FloorToInt(resourceAmount.amount * .6f));
                }
                Destroy(building.gameObject);
                
            });
        }
    }
}