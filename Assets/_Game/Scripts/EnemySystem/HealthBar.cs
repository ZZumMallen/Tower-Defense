using System;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Android.AndroidBuild;

namespace Partisan
{
    [RequireComponent(typeof(PanelRenderer))]
    public class HealthBar : MonoBehaviour
    {
        private Camera m_camera;
        private Quaternion m_newRotation;

        private PanelRenderer _panelRenderer;

        [SerializeField] private EnemyHealthSO healthData;
        [SerializeField] private float fullWidth = 100f;



        [SerializeField] private VisualElementReference<VisualElement> _healthBarFill;
        [SerializeField] private AuthoringIdPath _healthBarID;
        private VisualElement _healthBar;
        
        
        int uiVersion = 0;


        private void Awake()
        {
            _panelRenderer?.RegisterUIReloadCallback(OnUIReload);
        }

        private void OnDestroy()
        {
            _panelRenderer?.UnregisterUIReloadCallback(OnUIReload);
        }

        private void OnUIReload(PanelRenderer panel, VisualElement root, int version)
        {
            if (uiVersion == version)
                return;

            
        }

        private void OnEnable()
        {
            healthData.Reset();
        }

        private void Start()
        {
            m_camera = Camera.main;
        }

        private void LateUpdate()
        {
            var dir = m_camera.transform.position - transform.forward;
            m_newRotation = Quaternion.LookRotation(dir);
            transform.rotation = m_newRotation;
        }

        public void Bind(object dataSource)
        {
            
        }
    }
}
