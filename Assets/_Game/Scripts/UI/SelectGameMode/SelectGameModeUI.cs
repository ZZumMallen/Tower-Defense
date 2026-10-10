using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Partisan
{
    [RequireComponent(typeof(PanelRenderer))]
    public class SelectGameModeUI : MonoBehaviour
    {
        private PanelRenderer _panelRenderer;
        
        private Button _btnEndless;
        private Button _btnCampaign;
        private Button _btnUnknown;

        private event Action OnRequestEndless;
        private event Action OnRequestCampaign;
        private event Action OnRequestUnknown;

        private int _uiVersion;
        private void Awake()
        {
            _panelRenderer = GetComponent<PanelRenderer>();
            _panelRenderer.RegisterUIReloadCallback(OnUIReload);
            GameManager.OnGameStateChanged += OnGameManagerStateChanged;
        }

        private void OnGameManagerStateChanged(GameState state)
        {
            if (state == GameState.SelectGameMode)
            {
                _panelRenderer.enabled = true;
            }
        }

        private void OnDestroy()
        {
            _panelRenderer.UnregisterUIReloadCallback(OnUIReload);
            GameManager.OnGameStateChanged -= OnGameManagerStateChanged;
        }

        private void OnUIReload(PanelRenderer panelRenderer, VisualElement root, int version)
        {
            // ReSharper disable once RedundantCheckBeforeAssignment
            if (_uiVersion == version)
                return;

            _uiVersion = version;

            _btnEndless = root.Q<Button>("btnEndless");
            _btnCampaign = root.Q<Button>("btnCampaign");
            _btnUnknown = root.Q<Button>("btnUnknown");

            _btnCampaign.clicked += () => { OnRequestCampaign?.Invoke(); };
            _btnUnknown.clicked += () => { OnRequestUnknown?.Invoke(); };
            _btnEndless.clicked += () => { OnRequestEndless?.Invoke(); };
        }
    }
}