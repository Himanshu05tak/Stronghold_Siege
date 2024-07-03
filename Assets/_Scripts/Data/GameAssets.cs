using UnityEngine;

namespace _Scripts.Data
{
    public class GameAssets : MonoBehaviour
    {
        private static GameAssets _instance;

        public static GameAssets Instance
        {
            get
            {
                if (_instance == null)
                    _instance = Resources.Load<GameAssets>("GameAssets");
                return _instance;
            }
        }

        [SerializeField] private Transform pfEnemy;
        [SerializeField] private Transform pfArrowProjectile;
        [SerializeField] private Transform pfBuildingDestroyedParticle;
        [SerializeField] private Transform pfBuildingConstruction;
        [SerializeField] private Transform pfBuildingPlacedParticle;
        [SerializeField] private Transform pfEnemyDieParticle;

        public Transform GetEnemyPrefab => pfEnemy;
        public Transform GetProjectilePrefab => pfArrowProjectile;
        public Transform GetBuildingConstruction => pfBuildingConstruction;
        public Transform GetBuildingPlacedParticle => pfBuildingPlacedParticle;
        public Transform GetBuildingDestroyParticle => pfBuildingDestroyedParticle;
        public Transform GetEnemyDieParticle => pfEnemyDieParticle;
    }
}
