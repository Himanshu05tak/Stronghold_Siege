using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Scriptables
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BuildingTypeList")]
    public class BuildingTypeListSo : ScriptableObject
    { 
        public List<BuildingTypeSo> list;
    }
}
