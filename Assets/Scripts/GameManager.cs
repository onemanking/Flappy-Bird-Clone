using System;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    internal GameState CurrentGameState { get; private set; } = GameState.Idle;

    [Header("Player Prefab")]
    [SerializeField] private BasePlayerObject m_playerObjectPrefab;

    [Header("Game Configs")]
    [SerializeField] private GameConfig m_gameConfig;

    //TODO: MOVE IT TO OWN SCRIPT
    [Header("Game UI")]
    [SerializeField] private TextMeshProUGUI m_scoreText;

    private int score = 0;

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

        void HandlePlayInputAction()
        {
            switch (CurrentGameState)
            {
                case GameState.Idle:
                    UpdateGameState(GameState.Starting);
                    break;
                case GameState.GameOver:
                    UpdateGameState(GameState.Idle);
                    break;
            }
        }

        void HandlePlayerOutOfBound()
        {
            UpdateGameState(GameState.GameOver);
        }

        void HandlePlayerDied()
        {
            UpdateGameState(GameState.GameOver);
        }

        void HandleScoreChanged(int value)
        {
            score += value;
            UpdateScoreText(score);
        }
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
    }

    //TODO: MOVE TO OWN UI SCRIPT
    private void UpdateScoreText(int score)
    {
        m_scoreText.text = score.ToString();
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
}
