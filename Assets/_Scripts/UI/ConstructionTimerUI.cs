using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class ConstructionTimerUI : MonoBehaviour
    {
        [SerializeField] private BuildingConstruction buildingConstruction;
        private Image _constructionProgressImg;
        private void Awake()
        {
            _constructionProgressImg = transform.Find("mask").Find("image").GetComponent<Image>();
        }

        private void Update()
        {
            _constructionProgressImg.fillAmount = buildingConstruction.GetConstructionTimerNormalized();
        }
    }
}
