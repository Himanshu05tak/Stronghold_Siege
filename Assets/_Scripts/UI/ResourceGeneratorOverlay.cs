using TMPro;
using UnityEngine;
using _Scripts.Managers;

namespace _Scripts.UI
{
    public class ResourceGeneratorOverlay : MonoBehaviour
    {
        [SerializeField] private ResourceGenerator resourceGenerator;

        private Transform _barTransform;
        private int _index;
        private void Start()
        {
            var resourceGeneratorData = resourceGenerator.GetResourceGenerator();
            _barTransform = transform.Find("bar");
            transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
            transform.Find("text").GetComponent<TextMeshPro>().text = resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1");
        }

        private void Update()
        {
            _barTransform.localScale = new Vector3(resourceGenerator.GetTimerNormalized(),1,1);
        }
    }
}
