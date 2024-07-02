using System;
using UnityEngine;

namespace _Scripts.Data.Components
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private HealthSystem healthSystem;

        private Transform _barTransform;
        private Vector3 _localScale;
        private Transform _separatorContainer;
        private void Awake()
        {
            _barTransform = transform.Find("bar");
            _localScale = _barTransform.localScale;
        }

        private void Start()
        {
            _separatorContainer = transform.Find("separatorContainer");
            ConstructHealthBarSeparators();
            
            healthSystem.OnDamaged += HealthSystem_OnDamaged;
            healthSystem.OnHealed += HealthSystem_OnHealed;
            healthSystem.OnHealthAmountMaxChanged += HealthSystem_OnHealthAmountMaxChanged;
            UpdateBar();
            UpdateHealthBarVisible();
        }

        private void HealthSystem_OnHealed(object sender, EventArgs e)
        {
            UpdateBar();
            UpdateHealthBarVisible();
        }

        private void HealthSystem_OnDamaged(object sender, EventArgs e)
        {
            UpdateBar();
            UpdateHealthBarVisible();
        }

        private void HealthSystem_OnHealthAmountMaxChanged(object sender, EventArgs e)
        {
            ConstructHealthBarSeparators();
        }

        private void ConstructHealthBarSeparators()
        {
            var separatorTemplate = _separatorContainer.Find("separatorTemplate");
            separatorTemplate.gameObject.SetActive(false);

            foreach (Transform separatorTransform in _separatorContainer)
            {
                if(separatorTransform== separatorTemplate) continue;
                Destroy(separatorTransform.gameObject);
            }
            const float barSize = 3f;
            const int healthAmountPerSeparator = 10;
            var barOneHealthAmountSize = barSize / healthSystem.GetHealthAmountMax();
            var healthSeparatorCount = Mathf.FloorToInt(healthSystem.GetHealthAmountMax() / healthAmountPerSeparator);

            for (var i = 1; i < healthSeparatorCount; i++)
            {
                var separatorTransform = Instantiate(separatorTemplate, _separatorContainer);
                separatorTransform.gameObject.SetActive(true);
                separatorTransform.localPosition =
                    new Vector3(barOneHealthAmountSize * i * healthAmountPerSeparator, 0, 0);
            }

        }
        private void UpdateBar()
        {
            _barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized() *_localScale.x, 1, 1);
        }

        private void UpdateHealthBarVisible()
        {
            gameObject.SetActive(!healthSystem.IsFullHealth());
            gameObject.SetActive(true);
        }
    }
}
