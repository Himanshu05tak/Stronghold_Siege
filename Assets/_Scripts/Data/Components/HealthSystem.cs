using System;
using UnityEngine;

namespace _Scripts.Data.Components
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private int healthAmountMax;
        public event EventHandler OnDamaged;
        public event EventHandler OnDied;
        
        private int _healthAmount;

        private void Awake()
        {
            _healthAmount = healthAmountMax;
        }

        public void Damage(int damageAmount)
        {
            _healthAmount -= damageAmount;
            _healthAmount = Mathf.Clamp(_healthAmount, 0, healthAmountMax);
            
            OnDamaged?.Invoke(this,EventArgs.Empty);
            if(IsDead())
                OnDied?.Invoke(this,EventArgs.Empty);
        }

        public bool IsDead()
        {
            return _healthAmount == 0;
        }

        public bool IsFullHealth()
        {
            return _healthAmount == healthAmountMax;
        }

        public int GetHealthAmount()
        {
            return _healthAmount;
        }

        public float GetHealthAmountNormalized()
        {
            return (float)_healthAmount / healthAmountMax;
        }

        public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount)
        {
            this.healthAmountMax = healthAmountMax;
            if (updateHealthAmount)
                _healthAmount = healthAmountMax;
        }
    }
}
