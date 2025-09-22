using UnityEngine;
using Cinemachine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager instance;

    [SerializeField] private float globalShakeForce = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CameraShake(CinemachineImpulseSource impulseSource)
    {
        if (impulseSource != null)
            impulseSource.GenerateImpulseWithForce(globalShakeForce);
    }

    public void ZoomOut(CinemachineVirtualCamera vcam, float fov)
    {
        if (vcam != null)
            vcam.m_Lens.FieldOfView = fov; // works for perspective projection
    }
    public void ResetZoom(CinemachineVirtualCamera vcam, float defaultFov)
    {
        if (vcam != null)
            vcam.m_Lens.FieldOfView = defaultFov;
    }
}