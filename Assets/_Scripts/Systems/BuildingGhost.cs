using _Scripts.Managers;
using _Scripts.Utilities;
using UnityEngine;

namespace _Scripts.Systems
{
    public class BuildingGhost : MonoBehaviour
    {
        private GameObject _sprite;

        private void Awake()
        {
            _sprite = transform.Find("sprite").gameObject;
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
                Show(e.ActiveBuildingType.sprite);
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
