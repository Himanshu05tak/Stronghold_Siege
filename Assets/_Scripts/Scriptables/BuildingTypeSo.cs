using System.Collections.Generic;
using _Scripts.Data;
using UnityEngine;

namespace _Scripts.Scriptables
{
     [CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
    public class BuildingTypeSo : ScriptableObject
    {
        public string nameString;
        public Transform prefab;
        public List<ResourceGeneratorData> resourceGeneratorData;
        public Sprite sprite;
        public float minConstructionRadius;
    }
}
