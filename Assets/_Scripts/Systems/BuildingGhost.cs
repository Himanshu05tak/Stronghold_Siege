using UnityEngine;
using _Scripts.Managers;
using _Scripts.UI;
using _Scripts.Utilities;

namespace _Scripts.Systems
{
    public class BuildingGhost : MonoBehaviour
    {
        private GameObject _sprite;
        private ResourceNearbyOverlay _resourceGeneratorOverlay;
        private void Awake()
        {
            _sprite = transform.Find("sprite").gameObject;
            _resourceGeneratorOverlay = transform.Find("PF_ResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();
            Hide();
        }

        private void Start()
        {
            BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
        }

        private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
        {
            if (e.ActiveBuildingType == null)
                Hide();
            else
            {
                Show(e.ActiveBuildingType.sprite);
                _resourceGeneratorOverlay.Show(e.ActiveBuildingType.resourceGeneratorData);
            }
        }

        private void Update()
        {
            transform.position = Utility.GetMouseWorldPosition();
        }

        private void Show(Sprite ghostSprite)
        {
            _sprite.gameObject.SetActive(true);
            _sprite.GetComponent<SpriteRenderer>().sprite = ghostSprite;
        }
        
        private void Hide()
        {
            _sprite.gameObject.SetActive(false);
        }
    }
}
