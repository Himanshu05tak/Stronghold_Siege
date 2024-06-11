using System;
using System.Collections.Generic;
using _Scripts.Scriptables;
using UnityEngine;

namespace _Scripts.Managers
{
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager Instance { get; private set; }
        private Dictionary<ResourceTypeSo, int> _resourceAmountDictionary;

        public event EventHandler OnResourceAmountChanged;
        private void Awake()
        {
            Instance = this;
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
            OnResourceAmountChanged?.Invoke(this,EventArgs.Empty);
            TestLogResourceAmountDictionary();
        }

        public int GetResourceAmount(ResourceTypeSo resourceType)
        {
            return _resourceAmountDictionary[resourceType];
        }
    }
}
