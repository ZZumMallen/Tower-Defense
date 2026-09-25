using UnityEngine;
using UnityEngine.UIElements;

namespace Partisan
{
    public class TestingUI : MonoBehaviour
    {
        private Button _button0, _button1, _spawnEnemy;
        private int _uiVersion = 0;

        
        private void Awake()
        {
            GetComponent<PanelRenderer>().RegisterUIReloadCallback(OnUIReload);
        }

        private void OnUIReload(PanelRenderer panelRenderer, VisualElement root, int version)
        {
            if (_uiVersion == version) return;

            _uiVersion = version;

            _button0 = root.Q<Button>("spawnBuilding0");
            _button1 = root.Q<Button>("spawnBuilding1");
            _spawnEnemy = root.Q<Button>("btn-spawn-enemy");
            

            _button0.clicked += OnButton0Clicked;
            _button1.clicked += OnButton1Clicked;
            _spawnEnemy.clicked += OnSpawnEnemyClicked;
        }

        private static void OnSpawnEnemyClicked()
        {
            //
        }

        private static void OnButton0Clicked()
        {
            //
        }

        private static void OnButton1Clicked()
        {
            //
        }

        private void OnDestroy()
        {
            GetComponent<PanelRenderer>().UnregisterUIReloadCallback(OnUIReload);
        }


    }
}