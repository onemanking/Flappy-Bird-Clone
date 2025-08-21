using UnityEngine;
using Random = UnityEngine.Random;

public class PipeContainer : MonoBehaviour
{
    [SerializeField] private PipeContainerConfig m_config;
    [SerializeField] private Pipe[] m_pipes;
    [SerializeField] private Collider2D m_scoringZone;

    private Pipe TopPipe => m_pipes.Length > 0 ? m_pipes[0] : null;
    private Pipe BottomPipe => m_pipes.Length > 1 ? m_pipes[1] : null;

    private void Start()
    {
        PositionPipes();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PositionPipes();
        }
#endif

        if (Utils.IsReachedBoundaryX(TopPipe.GetBounds().max.x, true))
        {
            // TODO: IMPLEMENT POOLING SYSTEM
            Destroy(gameObject);
        }
    }

    private void PositionPipes()
    {
        if (m_pipes.Length < 2) return;

        var cam = Camera.main;
        var screenTop = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        var screenBottom = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        m_scoringZone.transform.localScale = new Vector2(m_scoringZone.transform.localScale.x, screenTop - screenBottom);

        var topRandomHeight = Random.Range(m_config.MinTopPipeHeight, m_config.MaxTopPipeHeight);
        var gapSize = Random.Range(m_config.MinGap, m_config.MaxGap);
        var botPosY = screenBottom + m_config.BottomOffset;

        if (TopPipe != null)
        {
            var topCenterY = screenTop - (topRandomHeight / 2);
            TopPipe.transform.position = new Vector2(TopPipe.transform.position.x, topCenterY);
            TopPipe.ExtendToSize(topRandomHeight);
        }

        if (TopPipe != null && BottomPipe != null)
        {
            var topBounds = TopPipe.GetBounds();
            var topPipeEdgeY = topBounds.min.y - gapSize;

            var totalDistance = Mathf.Max(1, topPipeEdgeY - botPosY);

            var centerY = botPosY + (totalDistance / 2);
            BottomPipe.transform.position = new Vector2(BottomPipe.transform.position.x, centerY);

            BottomPipe.ExtendToSize(totalDistance);
        }
        else
        {
            TopPipe.gameObject.SetActive(false);
            BottomPipe.gameObject.SetActive(false);
        }
    }
}
