using System;
using UnityEngine;

public abstract class BasePlayerObject : BaseObject, IEventBus
{
    [Header("Base Properties")]
    [SerializeField] protected BasePlayerObjectConfig Config;

    protected bool IsActive;

    public EventBus EventBus { get; private set; }

    #region Abstract Methods
    protected abstract void CheckBoundary();
    internal abstract void InputAction();
    #endregion


    #region Override Methods

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Pipe>(out var _))
        {
            EventBus.RaisePlayerDied();
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

    protected virtual void SetActive(bool active)
    {
        IsActive = active;

        if (Rb2D != null)
        {
            Rb2D.simulated = active;
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
            case GameState.Waiting:
                SetActive(false);
                break;
            case GameState.Playing:
                SetActive(true);
                break;
            case GameState.GameOver:
                SetActive(false);
                break;
        }
    }

    #endregion

    public void SetupEventBus(EventBus eventBus)
    {
        EventBus = eventBus;
        EventBus.GameStateChanged += OnGameStateChanged;
    }
}