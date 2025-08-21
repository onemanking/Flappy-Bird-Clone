using UnityEngine;

public class Pipe : BaseObject
{
    internal void ExtendToSize(float value)
    {
        var newSize = SpriteRenderer.size;
        newSize.y = value;
        SpriteRenderer.size = newSize;

        if (Collider2D is BoxCollider2D boxCollider)
        {
            boxCollider.size = newSize;
        }
    }

    internal Bounds GetBounds() => SpriteRenderer.bounds;
}
