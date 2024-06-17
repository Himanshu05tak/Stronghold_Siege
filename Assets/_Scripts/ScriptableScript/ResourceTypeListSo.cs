using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.ScriptableScript
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ResourceTypeList")]
    public class ResourceTypeListSo : ScriptableObject
    {
        public List<ResourceTypeSo> list;
    }
}
