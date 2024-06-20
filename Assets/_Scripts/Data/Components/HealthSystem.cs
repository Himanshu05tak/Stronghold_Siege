using System;
using UnityEngine;

namespace _Scripts.Data.Components
{
    public class HealthSystem : MonoBehaviour
    {
        public event EventHandler OnDamaged;
        public event EventHandler OnDied;
        
        private int _healthAmountMax;
        private int _healthAmount;

        private void Awake()
        {
            _healthAmount = _healthAmountMax;
        }

        public void Damage(int damageAmount)
        {
            _healthAmount -= damageAmount;
            _healthAmount = Mathf.Clamp(_healthAmount, 0, _healthAmountMax);
            
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
            return _healthAmount == _healthAmountMax;
        }

        public int GetHealthAmount()
        {
            return _healthAmount;
        }

        public float GetHealthAmountNormalized()
        {
            return (float)_healthAmount / _healthAmountMax;
        }

        public void SetHealthAmountMax(int healthAmountMax,bool updateHealthAmount)
        {
            _healthAmountMax = healthAmountMax;
            if (updateHealthAmount)
                _healthAmount = healthAmountMax;
        }
    }
}
