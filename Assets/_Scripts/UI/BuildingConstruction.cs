using UnityEngine;
using _Scripts.Data.Components;
using _Scripts.ScriptableScript;

namespace _Scripts.UI
{
    public class BuildingConstruction : MonoBehaviour
    {
        public static BuildingConstruction Create(Vector3 position, BuildingTypeSo buildingType)
        {
            var pfBuildingConstruction = Resources.Load<Transform>("PF_BuildingConstruction");
            var buildingConstructionTransform = Instantiate(pfBuildingConstruction, position, Quaternion.identity);

            var buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();
            buildingConstruction.SetBuildingType( buildingType);
            return buildingConstruction;
        }
        
        private float _constructionTimer;
        private float _constructionTimerMax;
        private BuildingTypeSo _buildingType;
        private BoxCollider2D _boxCollider2D;
        private SpriteRenderer _spriteRenderer;
        private BuildingTypeHolder _buildingTypeHolder;
        private Material _constructionMaterial;
        private static readonly int Progress = Shader.PropertyToID("_Progress");

        private void Awake()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _spriteRenderer =  transform.Find("sprite").GetComponent<SpriteRenderer>();
            _buildingTypeHolder = GetComponent<BuildingTypeHolder>();
            _constructionMaterial = _spriteRenderer.material;
        }
        private void Update()
        {
            _constructionTimer -= Time.deltaTime;
            _constructionMaterial.SetFloat(Progress, GetConstructionTimerNormalized());
            if (!(_constructionTimer <= 0)) return;
            Debug.Log("Ding!");
            Instantiate(_buildingType.prefab,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
        private void SetBuildingType(BuildingTypeSo buildingType)
        {
            _buildingType = buildingType;
            
            _spriteRenderer.sprite = buildingType.sprite;
            _buildingTypeHolder.buildingType = buildingType;
            _boxCollider2D.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset; 
            _boxCollider2D.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;
            
            _constructionTimerMax = buildingType.constructionTimerMax;
            _constructionTimer = _constructionTimerMax;
        }

        public float GetConstructionTimerNormalized()
        {
            return 1 -_constructionTimer/_constructionTimerMax;
        }
    }
}
