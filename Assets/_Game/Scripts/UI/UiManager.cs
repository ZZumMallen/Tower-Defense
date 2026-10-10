using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Partisan
{
	public class UiManager : MonoBehaviour
	{
		[SerializeField] private PanelRenderer selectGameModePanel;
		[SerializeField] private PanelRenderer buildModePanel;

		private void Awake()
		{
			GameManager.OnGameStateChanged += OnGameManagerStateChanged;
		}

		private void OnDestroy()
		{
			GameManager.OnGameStateChanged -= OnGameManagerStateChanged;
		}

		private void OnGameManagerStateChanged(GameState state)
		{
			switch (state)
			{
				case GameState.Build:
					buildModePanel.gameObject.SetActive(true);
					break;
				case GameState.SelectGameMode:
					selectGameModePanel.gameObject.SetActive(false);
					break;
				case GameState.Defend:
				case GameState.Victory:
				case GameState.Lose:
				default:
					throw new ArgumentOutOfRangeException(nameof(state), state, null);
			}
		}
	}
}
