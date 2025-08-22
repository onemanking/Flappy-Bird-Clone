using System;

public static class EventBus
{
    internal static event Action OnRestart;
    internal static event Action OnGameStart;
    internal static event Action OnGameOver;

    internal static event Action<GameState> GameStateChanged;

    internal static event Action OnPlayerInput;
    internal static event Action OnPlayerOutOfBound;
    internal static event Action OnPlayerDied;

    internal static event Action<int> OnScoreChanged;

    internal static void RaiseRestart() => OnRestart?.Invoke();
    internal static void RaiseGameStart() => OnGameStart?.Invoke();
    internal static void RaiseGameOver() => OnGameOver?.Invoke();

    internal static void RaisePlayerInput() => OnPlayerInput?.Invoke();
    internal static void RaisePlayerOutOfBound() => OnPlayerOutOfBound?.Invoke();
    internal static void RaisePlayerDied() => OnPlayerDied?.Invoke();

    internal static void RaiseGameStateChanged(GameState newState) => GameStateChanged?.Invoke(newState);
    internal static void RaiseScoreChanged(int score) => OnScoreChanged?.Invoke(score);
}