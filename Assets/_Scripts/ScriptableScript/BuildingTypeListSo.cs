using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.ScriptableScript
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BuildingTypeList")]
    public class BuildingTypeListSo : ScriptableObject
    { 
        public List<BuildingTypeSo> list;
    }
}
