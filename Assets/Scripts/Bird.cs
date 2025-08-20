using UnityEngine;

public class Bird : BasePlayerObject
{
    protected override void CheckBoundary()
    {
        var bounds = Collider2D.bounds;
        if (Utils.IsReachedBoundaryY(bounds.min.y, bounds.max.y))
        {
            OnOutOfBound();
        }
    }

    internal override void InputAction()
    {
        Jump();
    }

    private void Jump()
    {
        Rb2D.linearVelocityY = Config.JumpForce;
    }
}
