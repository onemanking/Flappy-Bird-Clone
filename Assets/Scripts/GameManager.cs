using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    internal GameState CurrentGameState { get; private set; } = GameState.Waiting;

    [Header("Player Prefab")]
    [SerializeField] private BasePlayerObject m_playerObjectPrefab;

    [Header("Game Configs")]
    [SerializeField] private GameConfig m_gameConfig;

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
        playerObject.SetActive(false);

        CameraController.Instance.SetTarget(playerObject.transform);
        PlayerController.Instance.SetupControlObject(playerObject);

        UpdateGameState(GameState.Waiting);
    }

    private void InitGameConfig()
    {

    }

    private void InitEventBus()
    {
        EventBus.OnPlayerInput += HandlePlayInputAction;
        EventBus.OnPlayerOutOfBound += HandlePlayerOutOfBound;
        EventBus.OnPlayerDied += HandlePlayerDied;
    }

    private void HandlePlayInputAction()
    {
        switch (CurrentGameState)
        {
            case GameState.Waiting:
                UpdateGameState(GameState.Playing);
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
        EventBus.RaiseGameStateChanged(newState);
        switch (newState)
        {
            case GameState.Waiting:
                EventBus.RaiseRestart();
                break;
            case GameState.Playing:
                EventBus.RaiseGameStart();
                break;
            case GameState.GameOver:
                EventBus.RaiseGameOver();
                break;
        }
    }
}
