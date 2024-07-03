using UnityEngine;
using _Scripts.Utilities;

namespace _Scripts.Data.Components
{
    public class ArrowProjectile : MonoBehaviour
    {
        public static ArrowProjectile Create(Vector3 position, Enemy enemy)
        {
            var arrowTransform = Instantiate(GameAssets.Instance.GetProjectilePrefab, position, Quaternion.identity);
            var arrowProjectile = arrowTransform.GetComponent<ArrowProjectile>();
            arrowProjectile.SetTarget(enemy);
            return arrowProjectile;
        }     
        
        [SerializeField] private float moveSpeed = 5f;
        private Enemy _targetEnemy;
        private Vector3 _lastMoveDir;
        private float _timeToDie = 2f;

        private void Update()
        {
            Vector3 moveDirection;
            if (_targetEnemy != null)
            {
                moveDirection = (_targetEnemy.transform.position - transform.position).normalized;
                _lastMoveDir = moveDirection;
            }
            else
                moveDirection = _lastMoveDir;
            
            _lastMoveDir = moveDirection;
            transform.position += moveDirection * (moveSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngelFromVector(moveDirection));

            _timeToDie -= Time.deltaTime;
            if (_timeToDie <= 0f)
            {
                Destroy(gameObject);
            }
        }

        public void SetTarget(Enemy targetEnemy)
        {
            _targetEnemy = targetEnemy;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                //Hit an Enemy!
                const int damageAmount = 10;
                enemy.GetComponent<HealthSystem>().Damage(damageAmount);
                Destroy(gameObject);
            }
        }
    }
}
