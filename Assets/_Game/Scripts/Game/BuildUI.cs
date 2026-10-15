using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Partisan
{
	[RequireComponent(typeof(PanelRenderer))]
	public class BuildUI : MonoBehaviour
	{
		public static BuildUI Instance {get; private set;}
		
		private int _uiVersion;
		private PanelRenderer _panelRenderer;
		private Button _btnTower1, _btnTower2, _btnTower3;
		public event Action OnRequestTower1, OnRequestTower2, OnRequestTower3;

		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(gameObject);
				return;
			}
			Instance = this;
			
			_panelRenderer = GetComponent<PanelRenderer>();
			_panelRenderer.RegisterUIReloadCallback(OnUIReload);
		}

		private void OnDestroy()
		{
			_panelRenderer.RegisterUIReloadCallback(OnUIReload);
		}

		private void OnUIReload(PanelRenderer panelRenderer, VisualElement root, int version)
		{
			if (_uiVersion == version) return;
			_uiVersion = version;

			_btnTower1 = root.Q<Button>("btn-spawn-tower__1");
			_btnTower2 = root.Q<Button>("btn-spawn-tower__2");
			_btnTower3 = root.Q<Button>("btn-spawn-tower__3");

			_btnTower1.clicked += () => OnRequestTower1?.Invoke();
			_btnTower2.clicked += () => OnRequestTower2?.Invoke();
			_btnTower3.clicked += () => OnRequestTower3?.Invoke();

		}
	}
}
