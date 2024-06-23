using UnityEngine;

namespace _Scripts.Data.Components
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float shootTimerMax = 0.5f;
        [SerializeField] private float targetMaxRadius = 20f;

        private Enemy _targetEnemy;
        private float _lookForTargetTimer;
        private const float LOOK_FOR_TARGET_TIMER_MAX = 0.2f;

        private float _shootArrowTimer;
        private Vector3 _projectileSpawnPos;

        private void Awake()
        {
            _projectileSpawnPos = transform.Find("projectileSpawnPos").position;
        }

        private void Update()
        {
            HandleTargetTimer();
            HandleShooting();
        }

        private void HandleShooting()
        {
            _shootArrowTimer -= Time.deltaTime;
            if (!(_shootArrowTimer <= 0)) return;
            _shootArrowTimer += shootTimerMax;
            if (_targetEnemy != null)
                ArrowProjectile.Create(_projectileSpawnPos, _targetEnemy);
        }

        private void HandleTargetTimer()
        {
            _lookForTargetTimer -= Time.deltaTime;
            if (!(_lookForTargetTimer <= 0)) return;
            _lookForTargetTimer += LOOK_FOR_TARGET_TIMER_MAX;
            LookForTargets();
        }

        private void LookForTargets()
        {
            var collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

            foreach (var col in collider2DArray)
            {
                var enemy = col.GetComponent<Enemy>();
                if (enemy == null) continue;
                if (_targetEnemy == null)
                {
                    //Is a building
                    _targetEnemy = enemy;
                }
                else
                {
                    var from = Vector3.Distance(transform.position, enemy.transform.position);
                    var to = Vector3.Distance(transform.position, _targetEnemy.transform.position);
                    if (from < to)
                    {
                        //Closer!
                        _targetEnemy = enemy;
                    }
                }
            }
        }
    }
}