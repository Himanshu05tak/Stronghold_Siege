using UnityEngine;

namespace _Scripts.Utilities
{
    public class SpritePositionSortingOrder : MonoBehaviour
    {
        [SerializeField] private bool runOnce;
        [SerializeField] private float positionOffsetY; //For each bushes 
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            const float precisionMultiplier = 5f;
            _spriteRenderer.sortingOrder = (int)(-(transform.position.y + positionOffsetY) * precisionMultiplier);
            
            if(runOnce)
                Destroy(this);
        }
    }
}
