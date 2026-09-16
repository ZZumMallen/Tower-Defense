using Partisan.Game.BuildSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Partisan.Game.UI
{
    public class TestingUI : MonoBehaviour
    {
        private Button _button0;
        private Button _button1;

        private int _uiVersion = 0;

        void Awake()
        {
            GetComponent<PanelRenderer>().RegisterUIReloadCallback(OnUIReload);
        }

        void OnDestroy()
        {
            _button0.clicked -= OnButton0Clicked;
            _button1.clicked -= OnButton1Clicked;
            GetComponent<PanelRenderer>().UnregisterUIReloadCallback(OnUIReload);
        }

        private void OnUIReload(PanelRenderer panelRenderer, VisualElement root, int version)
        {
            if (_uiVersion == version) return;

            _uiVersion = version;

            _button0 = root.Q<Button>("spawnBuilding0");
            _button1 = root.Q<Button>("spawnBuilding1");

            _button0.clicked += OnButton0Clicked;
            _button1.clicked += OnButton1Clicked;
        }

        private void OnButton0Clicked()
        {
            BuildingSystem.Current.InitializeWithObject(BuildingSystem.Current.prefab0);
        }

        private void OnButton1Clicked()
        {
            BuildingSystem.Current.InitializeWithObject(BuildingSystem.Current.prefab1);
        }


    }
}