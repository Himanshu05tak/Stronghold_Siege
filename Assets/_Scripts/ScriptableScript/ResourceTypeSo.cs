using UnityEngine;

namespace _Scripts.ScriptableScript
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ResourceType")]
    public class ResourceTypeSo : ScriptableObject
    {
        public string nameString;
        public Sprite sprite;
    }
}
