using _Scripts.Data;
using TMPro;
using UnityEngine;
using _Scripts.Data.Components;

namespace _Scripts.UI
{
    public class ResourcesIdentifierGeneratorHq : MonoBehaviour
    {
        [SerializeField] private BuildingTypeHolder typeHolder;
        [SerializeField] private TextMeshPro typeHolderText;

        private ResourceGeneratorData _resourceGeneratorData;
        private void Awake()
        {
            typeHolder = typeHolder.GetComponent<BuildingTypeHolder>();
            typeHolderText = typeHolderText.GetComponent<TextMeshPro>();

            _resourceGeneratorData = typeHolder.buildingType.resourceGeneratorData;
        }

        private void Start()
        {
            UpdateBuildingInfo();
        }

        private void UpdateBuildingInfo()
        {
            var colorHex = ("<color=#" + _resourceGeneratorData.resourceType.colorHex + ">"
                            + _resourceGeneratorData.resourceType.nameString + "</color> ");
            typeHolderText.SetText(colorHex);
        }
    }
}
