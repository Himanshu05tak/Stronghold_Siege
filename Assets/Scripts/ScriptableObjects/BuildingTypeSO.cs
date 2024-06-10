using UnityEngine;

namespace ScriptableObjects
{
     [CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
    public class BuildingTypeSO : ScriptableObject
    {
        public string nameString;
        public Transform prefab;
    }
}
