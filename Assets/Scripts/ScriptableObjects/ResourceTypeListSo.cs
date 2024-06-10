using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ResourceTypeList")]
    public class ResourceTypeListSo : ScriptableObject
    {
        public List<ResourceTypeSo> list;
    }
}
