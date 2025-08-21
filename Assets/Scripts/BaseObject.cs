using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public abstract class BaseObject : MonoBehaviour
{
    protected SpriteRenderer SpriteRenderer;
    protected Rigidbody2D Rb2D;
    protected Collider2D Collider2D;

    protected virtual void Awake()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        Rb2D ??= GetComponent<Rigidbody2D>();
        Collider2D ??= GetComponent<Collider2D>();
        SpriteRenderer ??= GetComponent<SpriteRenderer>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) { }
    protected virtual void OnTriggerEnter2D(Collider2D collider) { }
}