using System;
using UnityEngine;

namespace _Scripts.Data.Components
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private HealthSystem healthSystem;

        private Transform _barTransform;
        private Vector3 _localScale;
        private void Awake()
        {
            _barTransform = transform.Find("bar");
            _localScale = _barTransform.localScale;
        }

        private void Start()
        {
            healthSystem.OnDamaged += HealthSystem_OnDamaged;
            UpdateBar();
            UpdateHealthBarVisible();
        }

        private void HealthSystem_OnDamaged(object sender, EventArgs e)
        {
            UpdateBar();
            UpdateHealthBarVisible();
        }

        private void UpdateBar()
        {
            _barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized() *_localScale.x, 1, 1);
        }

        private void UpdateHealthBarVisible()
        {
            gameObject.SetActive(!healthSystem.IsFullHealth());
        }
    }
}
