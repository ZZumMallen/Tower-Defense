using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// publisher

namespace Partisan
{
    public class TestingUI : MonoBehaviour
    {
        private int _uiVersion = 0;
        
        private Button _spawnSingle, _spawnWave, _spawnTower, _spawnNothing;

        public event Action OnRequestEnemy;
        public event Action OnRequestWave;
        public event Action OnRequestTower;
        public event Action OnRequestNothing;

        private void Awake()
        {
            GetComponent<PanelRenderer>().RegisterUIReloadCallback(OnUIReload);
        }

        private void OnDestroy()
        {
            GetComponent<PanelRenderer>().UnregisterUIReloadCallback(OnUIReload);
        }

        private void OnUIReload(PanelRenderer panelRenderer, VisualElement root, int version)
        {
            if (_uiVersion == version) return;

            _uiVersion = version;

            _spawnSingle = root.Q<Button>("btn-spawn-enemy-single");
            _spawnWave = root.Q<Button>("btn-spawn-enemy-wave");
            _spawnTower = root.Q<Button>("btn-spawn-tower");
            _spawnNothing = root.Q<Button>("btn-spawn-nothing");            
                
            _spawnSingle.clicked += OnSpawnSingleClicked;
            _spawnWave.clicked += OnSpawnWaveClicked;
            _spawnTower.clicked += OnSpawnTowerClicked;
            _spawnNothing.clicked += OnSpawnNothingClicked;
        }

        private void OnSpawnSingleClicked() => OnRequestEnemy?.Invoke();
        private void OnSpawnWaveClicked() => OnRequestWave?.Invoke();
        private void OnSpawnTowerClicked() => OnRequestTower?.Invoke();
        private void OnSpawnNothingClicked() => OnRequestNothing?.Invoke();
    }
}