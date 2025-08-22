using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    internal GameState CurrentGameState { get; private set; } = GameState.Idle;

    [Header("Player Prefab")]
    [SerializeField] private BasePlayerObject m_playerObjectPrefab;

    [Header("Game Configs")]
    [SerializeField] private GameConfig m_gameConfig;

    private int score = 0;

    void Awake()
    {
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
        GameUI.Instance.OnRestartButtonClicked += HandleRestartButton;
    }

    private void Start()
    {
        UpdateGameState(GameState.Idle);
    }

    private void InitGameConfig()
    {

    }

    private void InitEventBus()
    {
        EventBus.OnPlayerInput += HandlePlayInputAction;
        EventBus.OnPlayerOutOfBound += HandlePlayerOutOfBound;
        EventBus.OnPlayerDied += HandlePlayerDied;
        EventBus.OnScoreChanged += HandleScoreChanged;

    }

    private void HandlePlayInputAction()
    {
        switch (CurrentGameState)
        {
            case GameState.Idle:
                UpdateGameState(GameState.Starting);
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

    private void HandleScoreChanged(int value)
    {
        score += value;
        UpdateScoreText(score);
    }

    private void HandleIdle()
    {
        EventBus.RaiseRestart();

        score = 0;
        UpdateScoreText(score);
        var playerObject = PlayerController.Instance.CurrentControlObject;
        playerObject?.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    private void HandleGameStarting()
    {
        EventBus.RaiseGameStart();
    }

    private void HandleGameOver()
    {
        EventBus.RaiseGameOver();

        var newHighScore = UpdateHighScore();
        GameUI.Instance.UpdateGameOverText(score, newHighScore);
    }

    private int UpdateHighScore()
    {
        var highScoreKey = Constants.HighScoreKey;
        int currentHighScore = PlayerPrefs.GetInt(highScoreKey, 0);

        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt(highScoreKey, score);
            PlayerPrefs.Save();
            return score;
        }

        return currentHighScore;
    }

    private void HandleRestartButton()
    {
        UpdateGameState(GameState.Idle);
    }

    private void UpdateScoreText(int score)
    {
        GameUI.Instance.UpdateScoreText(score);
    }

    private void UpdateGameState(GameState newState)
    {
        CurrentGameState = newState;
        EventBus.RaiseGameStateChanged(newState);
        switch (newState)
        {
            case GameState.Idle:
                HandleIdle();
                break;
            case GameState.Starting:
                HandleGameStarting();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
        }
    }

    private void OnDestroy()
    {
        GameUI.Instance.OnRestartButtonClicked -= HandleRestartButton;

        EventBus.OnPlayerInput -= HandlePlayInputAction;
        EventBus.OnPlayerOutOfBound -= HandlePlayerOutOfBound;
        EventBus.OnPlayerDied -= HandlePlayerDied;
        EventBus.OnScoreChanged -= HandleScoreChanged;
    }
}
