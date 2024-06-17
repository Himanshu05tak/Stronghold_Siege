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
        public ResourceGeneratorData resourceGeneratorData;
        public Sprite sprite;
        public float minConstructionRadius;
        public ResourceAmount[] constructionResourceAmounts;
    }
}
