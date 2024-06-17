using UnityEngine;
using _Scripts.Data;
using _Scripts.Utilities;

namespace _Scripts.Managers
{
    public class ResourceGenerator : MonoBehaviour
    {
        private ResourceGeneratorData _resourceGeneratorData;
        //private Dictionary<ResourceGeneratorData, float> _currentTimerOnEachData;
        //private Dictionary<ResourceGeneratorData, float> _maxTimerOnEachData;
        private float _currentTimerOnEachData;
        private float _maxTimerOnEachData;
        
        
        private void Awake()
        {
            _resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
            //_currentTimerOnEachData = new Dictionary<ResourceGeneratorData, float>();
            //_maxTimerOnEachData = new Dictionary<ResourceGeneratorData, float>();

            // foreach (var resourceGeneratorData in _resourceGeneratorData)
            // {
            //     _currentTimerOnEachData[resourceGeneratorData] = 0f;
            //     _maxTimerOnEachData[resourceGeneratorData] = resourceGeneratorData.timerMax;
            // }
            _currentTimerOnEachData = 0;
            _maxTimerOnEachData = _resourceGeneratorData.timerMax;
        }
        private void Start()
        {
            var nearbyResourceAmount = GetNearbyResourceAmount(_resourceGeneratorData, transform.position);

            if (nearbyResourceAmount == 0)
            {
                //No resource nodes nearby Disable resource generator
                enabled = false;
            }
            else
            {
                _maxTimerOnEachData = _resourceGeneratorData.timerMax / 2f + _resourceGeneratorData.timerMax *
                    (1 - (float)nearbyResourceAmount / _resourceGeneratorData.maxResourceAmount);
            }
        }

        private void Update()
        {
            _currentTimerOnEachData -= Time.deltaTime;
            if (_currentTimerOnEachData <= 0)
            {
                _currentTimerOnEachData += _maxTimerOnEachData;
                ResourceManager.Instance.AddResource(_resourceGeneratorData.resourceType, 1);
            }
        }

        public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
        {
            var results = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);

            var nearbyResourceAmount = 0;
            foreach (var result in results)
            {
                var resourceNode = result.GetComponent<ResourceNode>();
                if (resourceNode == null) continue;
                //It's a resource node!
                if(resourceNode.resourceType == resourceGeneratorData.resourceType)
                    nearbyResourceAmount++;
            }

            return nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0,
                resourceGeneratorData.maxResourceAmount);
        }

        /*private void GeneratingResources()
        {
            foreach (ResourceGeneratorData resourceGeneratorData in _resourceGeneratorData)
            {
                _currentTimerOnEachData[resourceGeneratorData] -= Time.deltaTime;
                if (!(_currentTimerOnEachData[resourceGeneratorData] <= 0f)) continue;
                _currentTimerOnEachData[resourceGeneratorData] += _maxTimerOnEachData[resourceGeneratorData];
                ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
                
            }
        }*/

        public ResourceGeneratorData GetResourceGenerator()
        {
            return _resourceGeneratorData;
        }

        public float GetTimerNormalized()
        {
            return _currentTimerOnEachData / _maxTimerOnEachData;
        }

        public float GetAmountGeneratedPerSecond()
        {
            return 1 / _maxTimerOnEachData;
        }
    }
}
