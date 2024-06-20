using System;
using System.Collections.Generic;
using _Scripts.ScriptableScript;
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
        }
        public void AddResource(ResourceTypeSo resourceType, int amount)
        {
            _resourceAmountDictionary[resourceType] += amount;
            OnResourceAmountChanged?.Invoke(this,EventArgs.Empty);
        }

        public int GetResourceAmount(ResourceTypeSo resourceType)
        {
            return _resourceAmountDictionary[resourceType];
        }

        public bool CanAfford(ResourceAmount[] resourceAmounts)
        {
            foreach (var resourceAmount in resourceAmounts)
            {
                if (GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount)
                {
                    //Can afford 
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        
        public void SpendResources(ResourceAmount[] resourceAmounts)
        {
            foreach (var resourceAmount in resourceAmounts)
            {
                if (GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount)
                    _resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
            }
        }
    }
}
