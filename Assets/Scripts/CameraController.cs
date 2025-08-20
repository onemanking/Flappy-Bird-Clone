using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : Singleton<CameraController>
{
    [SerializeField] private Transform m_target;
    [Header("Camera Settings")]
    [SerializeField] private CameraConfig m_cameraConfig;

    void LateUpdate()
    {
        if (m_target != null)
        {
            Vector3 desiredPosition = new Vector3(m_target.position.x, transform.position.y, transform.position.z) + m_cameraConfig.Offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, m_cameraConfig.SmoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    internal void SetTarget(Transform newTarget)
    {
        m_target = newTarget;
    }
}
