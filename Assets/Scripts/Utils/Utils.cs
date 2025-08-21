using UnityEngine;

internal static class Utils
{
    internal static bool IsReachedBoundaryY(float botY, float topY)
    {
        var cam = Camera.main;
        var screenTop = cam.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0f)).y;
        var screenBottom = cam.ViewportToWorldPoint(new Vector3(0.5f, 0f, 0f)).y;

        return topY < screenBottom || botY >= screenTop;
    }

    internal static bool IsReachedBoundaryX(float minX, float maxX)
    {
        var cam = Camera.main;
        var screenLeft = cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0f)).x;
        var screenRight = cam.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0f)).x;

        return maxX < screenLeft || minX > screenRight;
    }
}
