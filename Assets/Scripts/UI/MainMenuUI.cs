using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button m_startButton;
    [SerializeField] private Button m_highScoreButton;

    [Header("High Score")]
    [SerializeField] private Image m_highScorePanel;
    [SerializeField] private TextMeshProUGUI m_highScoreText;
    [SerializeField] private Button m_closeButton;

    private void Awake()
    {
        m_startButton.onClick.AddListener(OnStartButtonClicked);
        m_highScoreButton.onClick.AddListener(() => ToggleHighScorePanel(true));

        m_highScoreText.text = PlayerPrefs.GetInt(Constants.HighScoreKey, 0).ToString();
        m_closeButton.onClick.AddListener(() => ToggleHighScorePanel(false));

        m_highScorePanel.gameObject.SetActive(false);
    }

    private async void OnStartButtonClicked()
    {
        await SceneManager.LoadSceneAsync(Constants.GameplayScene);
    }

    private void ToggleHighScorePanel(bool value)
    {
        m_highScorePanel.gameObject.SetActive(value);
    }
}
