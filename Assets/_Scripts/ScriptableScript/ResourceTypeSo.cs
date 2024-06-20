using UnityEngine;

namespace _Scripts.ScriptableScript
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ResourceType")]
    public class ResourceTypeSo : ScriptableObject
    {
        public string nameString;
        public string nameShort;
        public Sprite sprite;
        public string colorHex;
    }
}
