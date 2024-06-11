using System.Collections.Generic;
using Data;
using UnityEngine;

namespace ScriptableObjects
{
     [CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
    public class BuildingTypeSo : ScriptableObject
    {
        public string nameString;
        public Transform prefab;
        public List<ResourceGeneratorData> resourceGeneratorData;
    }
}
