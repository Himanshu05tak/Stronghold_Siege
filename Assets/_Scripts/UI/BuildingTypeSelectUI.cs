using System;
using UnityEngine;
using UnityEngine.UI;
using _Scripts.Managers;
using _Scripts.Scriptables;
using System.Collections.Generic;

namespace _Scripts.UI
{
    public class BuildingTypeSelectUI : MonoBehaviour
    {
        [SerializeField] private Sprite arrowSprite;
        private Transform _arrowBtn;
        private const float MAX_ARROW_ICON_SIZE = -50f;
        private const float X_OFFSET = 130f;
        private Dictionary<BuildingTypeSo, Transform> _btnTransformDictionary;
        private void Awake()
        {
            var btnTemplate = GetBtnTemplate(out var buildingTypeList);
            //CloningBuildingTypes(buildingTypeList, btnTemplate);
            
            var index = 0;
           _arrowBtn = Instantiate(btnTemplate, transform);
           _arrowBtn.gameObject.SetActive(true);

           _arrowBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(X_OFFSET * index, 0);

           _arrowBtn.Find("image").GetComponent<Image>().sprite = arrowSprite;
           _arrowBtn.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(0,MAX_ARROW_ICON_SIZE);

           _arrowBtn.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetBuildingType(null);
            });
        
           index++;
            foreach (var buildingType in buildingTypeList.list)
            {
                var btnTransform = Instantiate(btnTemplate, transform);
                btnTransform.gameObject.SetActive(true);

                btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(X_OFFSET * index, 0);

                btnTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

                btnTransform.GetComponent<Button>().onClick.AddListener(() =>
                {
                    BuildingManager.Instance.SetBuildingType(buildingType);
                });
                _btnTransformDictionary.Add(buildingType, btnTransform);
                index++;
            }
        }

        private void Start()
        {
            BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
            UpdateActiveBuildingTypeButton();
        }

        private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
        {
            UpdateActiveBuildingTypeButton();
        }

        private void CloningBuildingTypes(BuildingTypeListSo buildingTypeList, Transform btnTemplate)
        {
            var index = 0;
            foreach (var buildingType in buildingTypeList.list)
            {
                var btnTransform = InstantiatingBtnTemplate(btnTemplate, index, buildingType);
                _btnTransformDictionary.Add(buildingType, btnTransform);
                index++;
            }
        }

        private Transform InstantiatingBtnTemplate(Transform btnTemplate, int index, BuildingTypeSo buildingType)
        {
            var btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);

            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(X_OFFSET * index, 0);

            btnTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

            btnTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetBuildingType(buildingType);
            });
            return btnTransform;
        }

        private Transform GetBtnTemplate(out BuildingTypeListSo buildingTypeList)
        {
            var btnTemplate = transform.Find("btnTemplate");
            btnTemplate.gameObject.SetActive(false);

            buildingTypeList = Resources.Load<BuildingTypeListSo>(nameof(BuildingTypeListSo));
            _btnTransformDictionary = new Dictionary<BuildingTypeSo, Transform>();
            return btnTemplate;
        }

        private void UpdateActiveBuildingTypeButton()
        {
            _arrowBtn.Find("selected").gameObject.SetActive(false);
            foreach (var buildingType in _btnTransformDictionary.Keys)
            {
                var btnTransform = _btnTransformDictionary[buildingType];
                btnTransform.Find("selected").gameObject.SetActive(false);
            }

            var activeBuildingType = BuildingManager.Instance.GetActiveBuildingType;
            if(activeBuildingType==null)
                _arrowBtn.Find("selected").gameObject.SetActive(true);
            else
                _btnTransformDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
        }
    }
}
