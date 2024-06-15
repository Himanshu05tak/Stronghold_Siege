using System.Collections.Generic;
using _Scripts.Data;
using _Scripts.Scriptables;
using _Scripts.Utilities;
using UnityEngine;

namespace _Scripts.Managers
{
    public class ResourceGenerator : MonoBehaviour
    {
        private List<ResourceGeneratorData> _resourceGeneratorData;
        private Dictionary<ResourceGeneratorData, float> _currentTimerOnEachData;
        private Dictionary<ResourceGeneratorData, float> _maxTimerOnEachData;

        private void Awake()
        {
            _resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
            _currentTimerOnEachData = new Dictionary<ResourceGeneratorData, float>();
            _maxTimerOnEachData = new Dictionary<ResourceGeneratorData, float>();

            foreach (var resourceGeneratorData in _resourceGeneratorData)
            {
                _currentTimerOnEachData[resourceGeneratorData] = 0f;
                _maxTimerOnEachData[resourceGeneratorData] = resourceGeneratorData.timerMax;
            }
        }
        private void Start()
        {
            //var results = new Collider2D[] { };
            //Physics2D.OverlapCircleNonAlloc(transform.position, 5f, results);
            for (var i = 0; i < _resourceGeneratorData.Count; i++)
            {
                var results = Physics2D.OverlapCircleAll(transform.position, _resourceGeneratorData[i].resourceDetectionRadius);

                var nearbyResourceAmount = 0;
                foreach (var result in results)
                {
                    var resourceNode = result.GetComponent<ResourceNode>();
                    if (resourceNode != null)
                    {
                        //It's a resource node!
                        if(resourceNode.resourceType == _resourceGeneratorData[i].resourceType)
                            nearbyResourceAmount++;
                    }
                }

                nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0,
                    _resourceGeneratorData[i].maxResourceAmount);

                if (nearbyResourceAmount == 0)
                {
                    //No resource nodes nearby 
                    //Disable resource generator
                    enabled = false;
                }
                else
                {
                    foreach (ResourceGeneratorData resourceGeneratorData in _resourceGeneratorData)
                    {
                        resourceGeneratorData.timerMax = (_resourceGeneratorData[i].timerMax / 2f) + _resourceGeneratorData[i].timerMax *
                                                         (1 - (float)nearbyResourceAmount / _resourceGeneratorData[i].maxResourceAmount);
                    Debug.Log($" nearbyResourceAmount: {nearbyResourceAmount} TimerMax {resourceGeneratorData.timerMax}");
                    }
                }
            }
        }

        private void Update()
        {
            foreach (ResourceGeneratorData resourceGeneratorData in _resourceGeneratorData) {
                _currentTimerOnEachData[resourceGeneratorData] -= Time.deltaTime;
                if (_currentTimerOnEachData[resourceGeneratorData] <= 0f) {
                    _currentTimerOnEachData[resourceGeneratorData] += _maxTimerOnEachData[resourceGeneratorData];
                    ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
                }
            }    
        }
    }
}
