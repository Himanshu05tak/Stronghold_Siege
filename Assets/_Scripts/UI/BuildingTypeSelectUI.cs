using System;
using UnityEngine;
using UnityEngine.UI;
using _Scripts.Managers;
using System.Collections.Generic;
using _Scripts.ScriptableScript;
using _Scripts.Systems;

namespace _Scripts.UI
{
    public class BuildingTypeSelectUI : MonoBehaviour
    {
        [SerializeField] private Sprite arrowSprite;
        [SerializeField] private List<BuildingTypeSo> ignoreBuildingTypes;

        private Transform _arrowBtn;
        private const float MAX_ARROW_ICON_SIZE = -50f;
        private const float X_OFFSET = 130f;
        private const float Y_OFFSET = 60f;
        private Dictionary<BuildingTypeSo, Transform> _btnTransformDictionary;

        private void Awake()
        {
            var btnTemplate = SetupArrowBtnTemplate(out var buildingTypeList, out var index);
            GeneratingBuildingTypes(buildingTypeList, btnTemplate, index);
        }
        private void Start()
        {
            BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
            UpdateActiveBuildingTypeButton();
        }
        private Transform SetupArrowBtnTemplate(out BuildingTypeListSo buildingTypeList, out int index)
        {
            var btnTemplate = GetBtnTemplate(out buildingTypeList);
            //CloningBuildingTypes(buildingTypeList, btnTemplate);

            index = 0;
            _arrowBtn = Instantiate(btnTemplate, transform);
            _arrowBtn.gameObject.SetActive(true);

            _arrowBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(X_OFFSET * index, Y_OFFSET);

            _arrowBtn.Find("image").GetComponent<Image>().sprite = arrowSprite;
            _arrowBtn.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, MAX_ARROW_ICON_SIZE);

            _arrowBtn.GetComponent<Button>().onClick.AddListener(() => { BuildingManager.Instance.SetBuildingType(null); });

            index++;
            return btnTemplate;
        }
        
        private void GeneratingBuildingTypes(BuildingTypeListSo buildingTypeList, Transform btnTemplate, int index)
        {
            foreach (var buildingType in buildingTypeList.list)
            {
                if (ignoreBuildingTypes.Contains(buildingType)) continue;
                var btnTransform = Instantiate(btnTemplate, transform);
                btnTransform.gameObject.SetActive(true);

                btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(X_OFFSET * index, Y_OFFSET);

                btnTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

                btnTransform.GetComponent<Button>().onClick.AddListener(() =>
                {
                    BuildingManager.Instance.SetBuildingType(buildingType);
                });
                var mouseEnterExitEvents = btnTransform.GetComponent<MouseEnterExitEvent>();
                mouseEnterExitEvents.OnMouseEnter += (sender,e) => { TooltipUI.Instance.Show(buildingType.nameString + "\n" + buildingType.GetConstructionResourcesCostString()); };
                mouseEnterExitEvents.OnMouseExit += (sender, e) => { TooltipUI.Instance.Hide(); };

                _btnTransformDictionary.Add(buildingType, btnTransform);
                index++;
            }
        }
        private void BuildingManager_OnActiveBuildingTypeChanged(object sender,
            BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
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

            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(X_OFFSET * index, Y_OFFSET);

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
            if (activeBuildingType == null)
                _arrowBtn.Find("selected").gameObject.SetActive(true);
            else
                _btnTransformDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
        }
    }
}