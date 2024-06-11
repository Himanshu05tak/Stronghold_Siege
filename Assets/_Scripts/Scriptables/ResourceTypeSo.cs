using UnityEngine;

namespace _Scripts.Scriptables
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ResourceType")]
    public class ResourceTypeSo : ScriptableObject
    {
        public string nameString;
        public Sprite sprite;
    }
}
