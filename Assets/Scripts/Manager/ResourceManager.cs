using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Manager
{
    public class ResourceManager : MonoBehaviour
    {
        private Dictionary<ResourceTypeSo, int> _resourceAmountDictionary;
        private void Awake()
        {
            _resourceAmountDictionary = new Dictionary<ResourceTypeSo, int>();

            var resourceTypeList = Resources.Load<ResourceTypeListSo>(nameof(ResourceTypeListSo));

            foreach (var resourceType in resourceTypeList.list)
            {
                _resourceAmountDictionary[resourceType] = 0;
            }

            TestLogResourceAmountDictionary();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                var resourceTypeList = Resources.Load<ResourceTypeListSo>(nameof(ResourceTypeListSo));
                AddResource(resourceTypeList.list[0], 2);
                TestLogResourceAmountDictionary();
            }
        }

        void TestLogResourceAmountDictionary()
        {
            foreach (var resourceType in _resourceAmountDictionary.Keys)
            {
                Debug.Log($"{resourceType.nameString} : {_resourceAmountDictionary[resourceType]}");
            }
        }

        public void AddResource(ResourceTypeSo resourceType, int amount)
        {
            _resourceAmountDictionary[resourceType] += amount;
        }
    }
}
