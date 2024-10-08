using System;
using _Scripts.Data;
using UnityEngine;
using _Scripts.Managers;
using _Scripts.Data.Components;
using _Scripts.Effects;
using _Scripts.Utilities;
using Unity.Mathematics;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        private const float LOOK_FOR_TARGET_TIMER_MAX = 0.2f;
        private Transform _targetTransform;
        private Rigidbody2D _rigidbody2D;
        private float _lookForTargetTimer;
        private HealthSystem _healthSystem;

        public static Enemy Create(Vector3 pos)
        {
            var enemyTransform = Instantiate(GameAssets.Instance.GetEnemyPrefab, pos, Quaternion.identity);
            var enemy = enemyTransform.GetComponent<Enemy>();
            return enemy;
        }

        private void Start()
        {
            if (BuildingManager.Instance.GetHqBuilding() != null)
                _targetTransform = BuildingManager.Instance.GetHqBuilding().transform;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _lookForTargetTimer = Random.Range(0f, LOOK_FOR_TARGET_TIMER_MAX);
            _healthSystem = GetComponent<HealthSystem>();
            _healthSystem.OnDied += HealthSystem_OnDied;
            _healthSystem.OnDamaged += HealthSystem_OnDamaged;
        }

        private void HealthSystem_OnDamaged(object sender, EventArgs e)
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyHit);
            CameraShake.Instance.ShakeCamera(5f,.1f);
        }

        private void HealthSystem_OnDied(object sender, EventArgs e)
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyDie);
            CameraShake.Instance.ShakeCamera(7f,.15f);
            ChromaticAberrationEffect.Instance.SetWeight(.5f);
            Instantiate(GameAssets.Instance.GetEnemyDieParticle,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }

        private void Update()
        {
            HandleMovement();
            HandleTargetTimer();
        }

        private void HandleMovement()
        {
            if (_targetTransform != null)
            {
                var moveDirection = (_targetTransform.position - transform.position).normalized;
                _rigidbody2D.velocity = moveDirection * moveSpeed;
            }
            else
                _rigidbody2D.velocity = Vector2.zero;
        }

        private void HandleTargetTimer()
        {
            _lookForTargetTimer -= Time.deltaTime;
            if (!(_lookForTargetTimer <= 0)) return;
            _lookForTargetTimer += LOOK_FOR_TARGET_TIMER_MAX;
            LookForTargets();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var building = other.gameObject.GetComponent<Building>();
            if (building != null)
            {
                var healthSystem = building.GetComponent<HealthSystem>();
                healthSystem.Damage(10);
            }
            this._healthSystem.Damage(999);
        }

        //Figure out which one is closer
        private void LookForTargets()
        {
            const float targetMaxRadius = 10f;
            var collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

            foreach (var col in collider2DArray)
            {
                var building = col.GetComponent<Building>();
                if (building == null) continue;
                if (_targetTransform == null)
                {
                    //Is a building
                    _targetTransform = building.transform;
                }
                else
                {
                    var from = Vector3.Distance(transform.position, building.transform.position);
                    var to = Vector3.Distance(transform.position, _targetTransform.transform.position);
                    if (from < to)
                    {
                        //Closer!
                        _targetTransform = building.transform;
                    }
                }
            }

            if (_targetTransform == null)
            {
                //Found no targets within range!
                if (BuildingManager.Instance.GetHqBuilding() != null)
                    _targetTransform = BuildingManager.Instance.GetHqBuilding().transform;
            }
        }
    }
}