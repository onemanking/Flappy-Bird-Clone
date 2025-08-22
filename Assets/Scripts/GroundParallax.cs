using UnityEngine;

public class GroundParallax : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] m_SpriteRenderers;

    private Vector2[] m_initialPositions;

    private bool isActive;
    private void Awake()
    {
        m_initialPositions = new Vector2[m_SpriteRenderers.Length];
        for (var i = 0; i < m_SpriteRenderers.Length; i++)
        {
            m_initialPositions[i] = m_SpriteRenderers[i].transform.position;
        }

        EventBus.GameStateChanged += HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Idle:
                HandleRestart();
                break;
            case GameState.Starting:
                isActive = true;
                break;
            case GameState.GameOver:
                isActive = false;
                break;
        }
    }

    private void HandleRestart()
    {
        isActive = false;
        for (var i = 0; i < m_SpriteRenderers.Length; i++)
        {
            m_SpriteRenderers[i].transform.position = m_initialPositions[i];
        }
    }

    private void Update()
    {
        if (!isActive) return;

        for (var i = 0; i < m_SpriteRenderers.Length; i++)
        {
            var sr = m_SpriteRenderers[i];
            var availableSr = i == m_SpriteRenderers.Length - 1 ? m_SpriteRenderers[0] : m_SpriteRenderers[i + 1];
            if (Utils.IsReachedBoundaryX(sr.bounds.min.x, sr.bounds.max.x))
            {
                var targetPos = new Vector3(availableSr.bounds.max.x, sr.transform.position.y);
                sr.transform.position = targetPos;
            }
        }
    }

    private void OnDestroy()
    {
        EventBus.GameStateChanged -= HandleGameStateChanged;
    }
}
