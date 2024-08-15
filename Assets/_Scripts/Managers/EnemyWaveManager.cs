using System;
using UnityEngine;
using _Scripts.Utilities;
using System.Collections.Generic;
using _Scripts.Data.Components;
using Random = UnityEngine.Random;

namespace _Scripts.Managers
{
    public class EnemyWaveManager : MonoBehaviour
    {
        public static EnemyWaveManager Instance { get; private set; }
        public event Action OnWaveNumberChanged;
        
        private enum State
        {
            None,
            WaitingToSpawnNextWave,
            SpawningWave
        }

        [SerializeField] private List<Transform> spawnTransformList;
        [SerializeField] private Transform nextWaveSpawnPositionTransform;

        private int _waveNumber;
        private float _nextWaveSpawnTimer;
        private float _nextEnemySpawnTimer;
        private float _remainingEnemySpawnAmount;

        private Vector3 _spawnPosition;
        private State _currentState;

        private const float SPAWN_TIMER = 10f;
        private const float MAX_NEXT_SPAWN_TIMER = .2f;
        private const float MAX_NEXT_SPAWN_RANGE = 5f;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _currentState = State.WaitingToSpawnNextWave;
            _spawnPosition = spawnTransformList[Random.Range(0,spawnTransformList.Count)].position;
            nextWaveSpawnPositionTransform.position = _spawnPosition;
            _nextWaveSpawnTimer = 3f;
            
            BuildingManager.Instance.GetHqBuilding().GetComponent<HealthSystem>().OnDied += (sender, args) =>
            {
                _currentState = State.None;
            };
        }
        private void Update()
        {
            switch (_currentState)
            {
                case State.WaitingToSpawnNextWave:
                    NextWaveSpawner();
                    break;
                case State.SpawningWave:
                    SpawnEnemy();
                    break;
                case State.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void NextWaveSpawner()
        {
            _nextWaveSpawnTimer -= Time.deltaTime;
            if (_nextWaveSpawnTimer < 0)
                SpawnWave();
        }
        private void SpawnEnemy()
        {
            if (!(_remainingEnemySpawnAmount > 0)) return;
            _nextEnemySpawnTimer -= Time.deltaTime;
            if (!(_nextEnemySpawnTimer < 0)) return;
            _nextEnemySpawnTimer = Random.Range(0, MAX_NEXT_SPAWN_TIMER);
            Enemy.Create(_spawnPosition + UtilsClass.GetRandomDir() * Random.Range(0, MAX_NEXT_SPAWN_RANGE));
            _remainingEnemySpawnAmount--;
            if (_remainingEnemySpawnAmount <= 0)
            {
                _currentState = State.WaitingToSpawnNextWave;
                _spawnPosition = spawnTransformList[Random.Range(0,spawnTransformList.Count)].position;
                nextWaveSpawnPositionTransform.position = _spawnPosition;
                _nextWaveSpawnTimer = SPAWN_TIMER;
            }
        }
        private void SpawnWave()
        {
            _remainingEnemySpawnAmount = 5 + 3 * _waveNumber;
            _currentState = State.SpawningWave;
            _waveNumber++;
            OnWaveNumberChanged?.Invoke();
        }

        public int GetWaveNumber()
        {
            return _waveNumber;
        }

        public float GetNextWaveSpawnTimer()
        {
            return _nextWaveSpawnTimer;
        }

        public Vector3 GetSpawnPosition()
        {
            return _spawnPosition;
        }
    }
}