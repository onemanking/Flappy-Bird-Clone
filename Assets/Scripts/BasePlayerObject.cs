using System;
using UnityEngine;

public abstract class BasePlayerObject : BaseObject
{
    [Header("Base Properties")]
    [SerializeField] protected BasePlayerObjectConfig Config;

    protected bool IsActive;

    #region Abstract Methods
    protected abstract void CheckBoundary();
    internal abstract void InputAction();
    #endregion

    #region Override Methods
    protected override void Awake()
    {
        base.Awake();

        EventBus.GameStateChanged += OnGameStateChanged;

#if UNITY_EDITOR
        Config.OnEditorValidate += Initialize;
#endif
    }

    protected override void Initialize()
    {
        base.Initialize();

        if (Rb2D != null)
        {
            Rb2D.gravityScale = Config.GravityScale;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            EventBus.RaisePlayerDied();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ScoringZone"))
        {
            EventBus.RaiseScoreChanged(1);
        }
    }

    #endregion

    #region Virtual Methods
    protected virtual void Update()
    {
        if (IsActive)
        {
            Move();
            CheckBoundary();
        }
    }

    protected virtual void Move()
    {
        transform.Translate(Config.Speed * Time.deltaTime * Vector3.right);
    }

    protected virtual void OnOutOfBound()
    {
        EventBus.RaisePlayerOutOfBound();
    }

    protected virtual void OnDestroy()
    {
        EventBus.GameStateChanged -= OnGameStateChanged;

#if UNITY_EDITOR
        Config.OnEditorValidate -= Initialize;
#endif
    }

    internal virtual void SetActive(bool active)
    {
        IsActive = active;

        if (Rb2D != null)
        {
            Rb2D.simulated = active;
            Rb2D.linearVelocity = Vector2.zero;
            Rb2D.angularVelocity = 0f;
        }

        if (Collider2D != null)
        {
            Collider2D.enabled = active;
        }
    }
    #endregion

    #region Private Methods
    private void OnGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Idle:
                SetActive(false);
                break;
            case GameState.Starting:
                SetActive(true);
                break;
            case GameState.GameOver:
                SetActive(false);
                break;
        }
    }
    #endregion
}