using System;
using System.Collections.Generic;
using _Scripts.Managers;
using _Scripts.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class ResourcesUI : MonoBehaviour
    {
        private const float OFFSET_X = -160f;
        private const float OFFSET_Y = -50f;
        
        private ResourceTypeListSo _resourceTypeList;
        private Dictionary<ResourceTypeSo, Transform> _resourceTypeTransformDictionary;
        private void Awake()
        {
            _resourceTypeList = Resources.Load<ResourceTypeListSo>(nameof(ResourceTypeListSo));
            _resourceTypeTransformDictionary = new Dictionary<ResourceTypeSo, Transform>();
            
            var resourceTemplate = transform.Find("resourceTemplate");
            resourceTemplate.gameObject.SetActive(false);
            
            var index = 0;
            foreach (var resourceType in _resourceTypeList.list)
            {
                var resourceTransform = Instantiate(resourceTemplate, transform);
                resourceTransform.gameObject.SetActive(true);
                
                resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(OFFSET_X * index, OFFSET_Y);

                var resourceImage = resourceTransform.Find("image").GetComponent<Image>();
                resourceImage.sprite = resourceType.sprite;

                _resourceTypeTransformDictionary.Add(resourceType, resourceTransform);
               
                index++;
            }
        }

        private void Start()
        {
            UpdateResourceAmount();
            ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        }

        private void ResourceManager_OnResourceAmountChanged(object sender, EventArgs e)
        {
            UpdateResourceAmount();
        }

        private void UpdateResourceAmount()
        {
            foreach (var resourceType in _resourceTypeList.list)
            {
                var resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);

                var resourceTransform = _resourceTypeTransformDictionary[resourceType];
                var resourceText = resourceTransform.Find("text").GetComponent<TextMeshProUGUI>();
                resourceText.SetText(resourceAmount.ToString()); 
            }
        }
    }
}
