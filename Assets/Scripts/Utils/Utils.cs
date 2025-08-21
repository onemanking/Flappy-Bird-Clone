using UnityEngine;

internal static class Utils
{
    internal static bool IsReachedBoundaryY(float botY, float topY)
    {
        var cam = Camera.main;
        var camTr = cam.transform;

        var vertExtent = cam.orthographicSize;
        var minY = camTr.position.y - vertExtent;
        var maxY = camTr.position.y + vertExtent;

        return topY <= minY || botY >= maxY;
    }

    internal static bool IsReachedBoundaryX(float minX, float maxX)
    {
        var cam = Camera.main;
        var camTr = cam.transform;

        var horzExtent = cam.orthographicSize * cam.aspect;
        var screenMinX = camTr.position.x - horzExtent;
        var screenMaxX = camTr.position.x + horzExtent;

        return maxX < screenMinX || minX > screenMaxX;
    }
}
