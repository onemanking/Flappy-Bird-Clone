using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : Singleton<GameUI>
{
    [Header("Idle")]
    [SerializeField] private Image m_idlePanel;

    [Header("In-game")]
    [SerializeField] private Image m_inGamePanel;
    [SerializeField] private TextMeshProUGUI m_scoreText;

    [Header("Game Over")]
    [SerializeField] private Image m_gameOverPanel;
    [SerializeField] private TextMeshProUGUI m_finalScoreText;
    [SerializeField] private TextMeshProUGUI m_highScoreText;
    [SerializeField] private Button m_restartButton;

    internal event Action OnRestartButtonClicked;

    void Awake()
    {
        m_restartButton.onClick.AddListener(() =>
        {
            OnRestartButtonClicked?.Invoke();
        });

        EventBus.GameStateChanged += HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Idle:
                m_idlePanel.gameObject.SetActive(true);
                m_inGamePanel.gameObject.SetActive(false);
                m_gameOverPanel.gameObject.SetActive(false);
                break;
            case GameState.Starting:
                m_idlePanel.gameObject.SetActive(false);
                m_inGamePanel.gameObject.SetActive(true);
                m_gameOverPanel.gameObject.SetActive(false);
                break;
            case GameState.GameOver:
                m_idlePanel.gameObject.SetActive(false);
                m_inGamePanel.gameObject.SetActive(false);
                m_gameOverPanel.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    internal void UpdateScoreText(int score)
    {
        m_scoreText.text = score.ToString();
    }

    internal void UpdateGameOverText(int score, int highScore)
    {
        m_finalScoreText.text = score.ToString();
        m_highScoreText.text = highScore.ToString();
    }

    private void OnDestroy()
    {
        OnRestartButtonClicked = null;
        EventBus.GameStateChanged -= HandleGameStateChanged;
    }
}