using UnityEngine;
using System;

namespace Partisan
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameState State;

        public static event Action<GameState> OnGameStateChanged;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            UpdateGameState(GameState.SelectGameMode);
        }

        public void UpdateGameState(GameState newState)
        {
            State = newState;
            
            switch (newState)
            {
                case GameState.SelectGameMode:
                    HandleSelectGameMode();
                    break;
                case GameState.Build:
                    HandleBuild();
                    break;
                case GameState.Defend:
                    HandleDefend();
                    break;
                case GameState.Victory:
                    HandleVictory();
                    break;
                case GameState.Lose:
                    HandleLose();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }


            OnGameStateChanged?.Invoke(newState);
        }

        private void HandleSelectGameMode()
        {
            
        }

        private void HandleBuild()
        {
            throw new NotImplementedException();
        }
        
        private void HandleDefend()
        {
            throw new NotImplementedException();
        }
        
        private void HandleVictory()
        {
            throw new NotImplementedException();
        }
        
        private void HandleLose()
        {
            throw new NotImplementedException();
        }


    }

    public enum GameState
    {
        SelectGameMode,
        Build,
        Defend,
        Victory,
        Lose
    }
    
}