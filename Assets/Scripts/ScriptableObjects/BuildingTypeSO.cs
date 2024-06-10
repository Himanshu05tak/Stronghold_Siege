using UnityEngine;

namespace ScriptableObjects
{
     [CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
    public class BuildingTypeSo : ScriptableObject
    {
        public string nameString;
        public Transform prefab;
    }
}
