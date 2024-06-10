using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BuildingTypeList")]
    public class BuildingTypeListSo : ScriptableObject
    { 
        public List<BuildingTypeSo> list;
    }
}
