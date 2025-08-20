using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    internal GameState CurrentGameState { get; private set; } = GameState.Waiting;

    [Header("Player Prefab")]
    [SerializeField] private BasePlayerObject m_playerObjectPrefab;

    [Header("Game Configs")]
    [SerializeField] private GameConfig m_gameConfig;

    private EventBus eventBus;

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    private void Initialize()
    {
        InitGameConfig();
        InitEventBus();

        var playerObject = Instantiate(m_playerObjectPrefab, Vector3.zero, Quaternion.identity);
        playerObject.SetupEventBus(eventBus);

        CameraController.Instance.SetTarget(playerObject.transform);
        PlayerController.Instance.SetupControlObject(playerObject);
        PlayerController.Instance.SetupEventBus(eventBus);

        UpdateGameState(GameState.Waiting);
    }

    private void InitGameConfig()
    {
        Physics2D.gravity = new Vector2(0, m_gameConfig.GravityScale);
    }

    private void InitEventBus()
    {
        eventBus = new EventBus();
        eventBus.OnPlayerInput += HandlePlayInputAction;
        eventBus.OnPlayerOutOfBound += HandlePlayerOutOfBound;
        eventBus.OnPlayerDied += HandlePlayerDied;
    }

    private void HandlePlayInputAction()
    {
        switch (CurrentGameState)
        {
            case GameState.Waiting:
                UpdateGameState(GameState.Playing);
                Debug.Log("Game Started");
                break;
        }
    }

    private void HandlePlayerOutOfBound()
    {
        UpdateGameState(GameState.GameOver);
    }

    private void HandlePlayerDied()
    {
        UpdateGameState(GameState.GameOver);
    }

    private void UpdateGameState(GameState newState)
    {
        CurrentGameState = newState;
        eventBus.RaiseGameStateChanged(newState);
    }
}
