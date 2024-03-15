using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    CinemachineConfiner2D confiner2D;
    public CinemachineImpulseSource impulseSource;
    public Void_Event_SO cameraShakeEvent;
    private void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }
    private void OnEnable()
    {
        cameraShakeEvent.OnEventRaised += OnCameraShakeEvent;
    }
    private void OnDisable()
    {
        cameraShakeEvent.OnEventRaised -= OnCameraShakeEvent;
    }
    private void OnCameraShakeEvent()
    {
        impulseSource.GenerateImpulse();
    }

    private void Start()
    {
        GetCameraBounds();
    }
    void GetCameraBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if (obj != null)
        {
            confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
            confiner2D.InvalidateCache();
        }
    }
}
