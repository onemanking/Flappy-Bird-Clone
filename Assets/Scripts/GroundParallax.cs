using UnityEngine;

public class GroundParallax : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] m_SpriteRenderers;

    private void Update()
    {
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
}
