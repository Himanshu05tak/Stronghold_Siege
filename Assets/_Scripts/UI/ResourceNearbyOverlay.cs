using TMPro;
using UnityEngine;
using _Scripts.Data;
using _Scripts.Managers;

namespace _Scripts.UI
{
    public class ResourceNearbyOverlay : MonoBehaviour
    {
        private ResourceGeneratorData _resourceGeneratorData;

        private void Awake()
        {
            Hide();
        }

        private void Update()
        {
            var nearbyResourceAmount =
                ResourceGenerator.GetNearbyResourceAmount(_resourceGeneratorData, transform.position);
            var percent = Mathf.RoundToInt((float)nearbyResourceAmount / _resourceGeneratorData.maxResourceAmount*100f);
            transform.Find("text").GetComponent<TextMeshPro>().SetText(percent + "%");
        }

        public void Show(ResourceGeneratorData resourceGeneratorData)
        {
            _resourceGeneratorData = resourceGeneratorData;
            gameObject.SetActive(true);

            transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }

    }
}
