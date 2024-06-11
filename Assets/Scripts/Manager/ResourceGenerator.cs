using Data;
using UnityEngine;
using ScriptableObjects;
using System.Collections.Generic;

namespace Manager
{
    public class ResourceGenerator : MonoBehaviour
    {
        private BuildingTypeSo _buildingType;
        private Dictionary<ResourceGeneratorData, float> _currentTimerOnEachData;
        private Dictionary<ResourceGeneratorData, float> _maxTimerOnEachData;

        private void Awake()
        {
            _buildingType = GetComponent<BuildingTypeHolder>().buildingType;
            _currentTimerOnEachData = new Dictionary<ResourceGeneratorData, float>();
            _maxTimerOnEachData = new Dictionary<ResourceGeneratorData, float>();

            foreach (ResourceGeneratorData resourceGeneratorData in _buildingType.resourceGeneratorData)
            {
                _currentTimerOnEachData[resourceGeneratorData] = 0f;
                _maxTimerOnEachData[resourceGeneratorData] = resourceGeneratorData.timerMax;
            }
        }

        private void Update()
        {
            foreach (ResourceGeneratorData resourceGeneratorData in _buildingType.resourceGeneratorData) {
                _currentTimerOnEachData[resourceGeneratorData] -= Time.deltaTime;
                if (_currentTimerOnEachData[resourceGeneratorData] <= 0f) {
                    _currentTimerOnEachData[resourceGeneratorData] += _maxTimerOnEachData[resourceGeneratorData];
                    ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
                }
            }    
            // _timer -= Time.deltaTime;
            // if (!(_timer <= 0f)) return;
            // _timer += _timerMax;
            // ResourceManager.Instance.AddResource(_buildingType.resourceGeneratorData.resourceType,1);
        }
    }
}
