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

    protected override void Update()
    {
        base.Update();

        UpdateRotationFromVelocity();
    }

    private void UpdateRotationFromVelocity()
    {
        var birdConfig = Config as BirdConfig;

        if (Rb2D.linearVelocityY <= birdConfig.StartFallingVelocity)
        {
            var targetAngle = Mathf.Lerp(0, -90, Mathf.Abs(birdConfig.StartFallingVelocity - Rb2D.linearVelocityY) * birdConfig.MaxFallSpeedScale);

            transform.rotation = Quaternion.Euler(0, 0, targetAngle);
        }
    }

    internal override void InputAction()
    {
        if (!IsActive) return;

        Jump();
    }

    private void Jump()
    {
        var birdConfig = Config as BirdConfig;
        Rb2D.linearVelocityY = birdConfig.JumpForce;
        transform.rotation = Quaternion.Euler(0, 0, birdConfig.TiltAngle);
    }
}
