using System;

// TODO: CHANGE TO STATIC CLASS
public class EventBus
{
    internal event Action OnGameStart;
    internal event Action OnGameOver;
    internal event Action OnRestart;

    internal event Action<GameState> GameStateChanged;

    internal event Action OnPlayerInput;
    internal event Action OnPlayerOutOfBound;
    internal event Action OnPlayerDied;

    internal event Action<int> OnScoreChanged;

    internal void RaiseGameStart() => OnGameStart?.Invoke();
    internal void RaiseGameOver() => OnGameOver?.Invoke();
    internal void RaiseRestart() => OnRestart?.Invoke();

    internal void RaisePlayerInput() => OnPlayerInput?.Invoke();
    internal void RaisePlayerOutOfBound() => OnPlayerOutOfBound?.Invoke();
    internal void RaisePlayerDied() => OnPlayerDied?.Invoke();

    internal void RaiseGameStateChanged(GameState newState) => GameStateChanged?.Invoke(newState);
    internal void RaiseScoreChanged(int score) => OnScoreChanged?.Invoke(score);
}