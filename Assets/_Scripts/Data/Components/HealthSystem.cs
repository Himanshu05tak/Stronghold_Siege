using System;
using UnityEngine;

namespace _Scripts.Data.Components
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private int healthAmountMax;
        public event EventHandler OnDamaged;
        public event EventHandler OnDied;
        public event EventHandler OnHealed;
        public event EventHandler OnHealthAmountMaxChanged;

        private int _healthAmount;

        private void Awake()
        {
            _healthAmount = healthAmountMax;
        }

        public void Damage(int damageAmount)
        {
            _healthAmount -= damageAmount;
            _healthAmount = Mathf.Clamp(_healthAmount, 0, healthAmountMax);

            OnDamaged?.Invoke(this, EventArgs.Empty);
            if (IsDead())
                OnDied?.Invoke(this, EventArgs.Empty);
        }

        public void Heal(int healAmount)
        {
            _healthAmount += healAmount;
            _healthAmount = Mathf.Clamp(_healthAmount, 0, healthAmountMax);
            OnHealed?.Invoke(this, EventArgs.Empty);
        }

        public void HealFull()
        {
            _healthAmount = healthAmountMax;
            OnHealed?.Invoke(this, EventArgs.Empty);
        }

        private bool IsDead()
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
        public int GetHealthAmountMax()
        {
            return healthAmountMax;
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
            OnHealthAmountMaxChanged?.Invoke(this,EventArgs.Empty);
        }
    }
}