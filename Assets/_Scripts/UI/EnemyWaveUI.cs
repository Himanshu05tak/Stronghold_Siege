using TMPro;
using UnityEngine;
using _Scripts.Managers;
using _Scripts.Utilities;

namespace _Scripts.UI
{
    public class EnemyWaveUI : MonoBehaviour
    {
        [SerializeField] private EnemyWaveManager enemyWaveManager;
        
        private TextMeshProUGUI _waveNumberText;
        private TextMeshProUGUI _waveMessageText;
        private RectTransform _enemyWaveSpawnPositionIndicator;
        private RectTransform _enemyClosestPositionIndicator;
        private Camera _mainCamera;

        private const float OFFSET = 300f;
        //private const float  = 1.5f;
        private void Awake()
        {
            _waveNumberText = transform.Find("waveNumberText").GetComponent<TextMeshProUGUI>();
            _waveMessageText = transform.Find("waveMessageText").GetComponent<TextMeshProUGUI>();
            _enemyWaveSpawnPositionIndicator = transform.Find("enemyWaveSpawnPositionIndicator").GetComponentInChildren<RectTransform>();
            _enemyClosestPositionIndicator = transform.Find("enemyClosestSpawnPosition").GetComponentInChildren<RectTransform>();
        }

        private void Start()
        {
            _mainCamera = Camera.main;
            enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
            SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
        }

        private void EnemyWaveManager_OnWaveNumberChanged()
        {
            SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
        }

        private void Update()
        {
            HandleNextWaveMessage();
            HandleEnemyWaveSpawnPositionIndicator();
            HandleClosestPositionIndicator();
        }

        private void HandleNextWaveMessage()
        {
            var nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
            if (nextWaveSpawnTimer <= 0f)
                SetMessageText("");
            else
                SetMessageText("Next Wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
       

        private void HandleEnemyWaveSpawnPositionIndicator()
        {
            var dirToNextSpawnPosition = (enemyWaveManager.GetSpawnPosition() - _mainCamera.transform.position).normalized;
            _enemyWaveSpawnPositionIndicator.anchoredPosition = dirToNextSpawnPosition * OFFSET;
            _enemyWaveSpawnPositionIndicator.eulerAngles =
                new Vector3(0, 0, UtilsClass.GetAngelFromVector(dirToNextSpawnPosition));
            
            var distanceToNextSpawnPosition =
                Vector3.Distance(enemyWaveManager.GetSpawnPosition(), _mainCamera.transform.position);
            _enemyWaveSpawnPositionIndicator.gameObject.SetActive(distanceToNextSpawnPosition >
                                                                  _mainCamera.orthographicSize * 1.5f);
        }
        private void HandleClosestPositionIndicator()
        {
            var collider2DArray = Physics2D.OverlapCircleAll(_mainCamera.transform.position, 9999f);
            Enemy _targetEnemy = null;
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

            if (_targetEnemy != null)
            {
                var dirToClosetEnemy = (_targetEnemy.transform.position - _mainCamera.transform.position).normalized;
                _enemyClosestPositionIndicator.anchoredPosition = dirToClosetEnemy * 250f;
                _enemyClosestPositionIndicator.eulerAngles =
                    new Vector3(0, 0, UtilsClass.GetAngelFromVector(dirToClosetEnemy));
                var distanceToClosetEnemy =
                    Vector3.Distance(enemyWaveManager.GetSpawnPosition(), _mainCamera.transform.position);
                _enemyClosestPositionIndicator.gameObject.SetActive(distanceToClosetEnemy >
                                                                    _mainCamera.orthographicSize * 1.5f);
            }
            else
            {
                //No enemies Alive
                _enemyClosestPositionIndicator.gameObject.SetActive(false);
            }
        }

        

        private void SetMessageText(string message)
        {
            _waveMessageText.SetText(message);
        }
        
        private void SetWaveNumberText(string message)
        {
            _waveNumberText.SetText(message);
        }
    }
}
