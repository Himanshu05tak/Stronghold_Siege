using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Scriptables
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ResourceTypeList")]
    public class ResourceTypeListSo : ScriptableObject
    {
        public List<ResourceTypeSo> list;
    }
}
