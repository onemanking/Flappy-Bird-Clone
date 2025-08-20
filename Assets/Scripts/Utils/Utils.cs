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
}
