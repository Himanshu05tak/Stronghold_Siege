using System.Linq;
using _Scripts.Data;
using UnityEngine;

namespace _Scripts.ScriptableScript
{
     [CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
    public class BuildingTypeSo : ScriptableObject
    {
        public string nameString;
        public Transform prefab;
        //public List<ResourceGeneratorData> resourceGeneratorData;
        public bool hasResourceGeneratorData;
        public ResourceGeneratorData resourceGeneratorData;
        public Sprite sprite;
        public float minConstructionRadius;
        public ResourceAmount[] constructionResourceAmounts;
        public int healthAmountMax;
        public float constructionTimerMax;
        public string GetConstructionResourcesCostString()
        {
            var infoText = constructionResourceAmounts.Aggregate("", (current, resourceAmount)
                => current + ("<color=#" + resourceAmount.resourceType.colorHex + ">"
                              + resourceAmount.resourceType.nameShort + resourceAmount.amount) + "</color> ");
            return infoText;
        }
    }
}
