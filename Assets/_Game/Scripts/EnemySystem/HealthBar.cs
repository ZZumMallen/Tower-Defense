using UnityEngine;
using UnityEngine.UIElements;

namespace Partisan
{
    [RequireComponent(typeof(PanelRenderer))]
    public class HealthBar : MonoBehaviour
    {
        private const string HealthBarFillName = "HealthBarFill";
        private const string HealthLabelName = "HealthLabel";

        private PanelRenderer _panelRenderer;
        private Enemy _enemy;
        private VisualElement _healthBarFill;

        private Label _healthLabel;
        private int _uiVersion = -1;

        private void Awake()
        {
            _panelRenderer = GetComponent<PanelRenderer>();
            _panelRenderer.RegisterUIReloadCallback(OnUIReload);
        }

        private void OnDestroy()
        {
            _panelRenderer.UnregisterUIReloadCallback(OnUIReload);

            if (!_enemy) return;
            _enemy.HealthChanged -= OnHealthChanged;
        }

        private void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }

        public void Bind(Enemy enemy)
        {
            if (_enemy != null)
            {
                _enemy.HealthChanged -= OnHealthChanged;
            }

            _enemy = enemy;
            if (_enemy != null)
            {
                _enemy.HealthChanged += OnHealthChanged;
            }

            RefreshHealthDisplay();
        }

        private void OnUIReload(PanelRenderer panel, VisualElement root, int version)
        {
            if (_uiVersion == version) return;

            _uiVersion = version;
            _healthBarFill = root.Q<VisualElement>(HealthBarFillName);
            _healthLabel = root.Q<Label>(HealthLabelName);

            RefreshHealthDisplay();
        }

        private void RefreshHealthDisplay()
        {
            RefreshHealthDisplay(_enemy.CurrentHealth, _enemy.MaxHealth);
        }

        private void OnHealthChanged(float currentHealth, float maxHealth)
        {
            if (_enemy != null)
            {
                RefreshHealthDisplay(currentHealth, maxHealth);
            }
        }

        private void RefreshHealthDisplay(float currentHealth, float maxHealth)
        {
            var healthPercent = maxHealth > 0f
                ? Mathf.Clamp01(currentHealth / maxHealth) * 100f
                : 0f;

            if (_healthBarFill != null)
            {
                _healthBarFill.style.width = Length.Percent(healthPercent);
            }

            if (_healthLabel != null)
            {
                _healthLabel.text = $"{Mathf.CeilToInt(currentHealth)} / {Mathf.CeilToInt(maxHealth)}";
            }
        }
    }
}
