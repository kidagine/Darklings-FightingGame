using System.Collections;
using Cinemachine;
using UnityEngine;
public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;
    private CinemachineTransposer _cinemachineTransposer;
    private Coroutine _zoomCoroutine;
    private float _shakeTimer;
    private float _zoomTimer;
    private int _defaultZoom;

    void Awake()
    {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cinemachineBasicMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cinemachineTransposer = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        _defaultZoom = (int)_cinemachineTransposer.m_FollowOffset.z;
        CheckInstance();
    }

    private void CheckInstance()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void Shake(CameraShakerNetwork cameraShaker)
    {
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = cameraShaker.intensity;
        _shakeTimer = cameraShaker.timer;
    }

    public void Zoom(int zoomValue, float time)
    {
        if (_zoomCoroutine != null)
            StopCoroutine(_zoomCoroutine);
        _zoomCoroutine = StartCoroutine(ZoomCoroutine(zoomValue, time));
    }

    public void ZoomDefault(float time)
    {
        if (_zoomCoroutine != null)
            StopCoroutine(_zoomCoroutine);
        _zoomCoroutine = StartCoroutine(ZoomCoroutine(_defaultZoom, time));
    }

    IEnumerator ZoomCoroutine(float zoomValue, float time)
    {
        float elapsedTime = 0;
        float waitTime = time;
        Vector3 startZoom = new Vector3(0, 0, _cinemachineTransposer.m_FollowOffset.z);
        Vector3 endZoom = new Vector3(0, 0, zoomValue);
        while (elapsedTime < waitTime)
        {
            _cinemachineTransposer.m_FollowOffset = Vector3.Lerp(startZoom, endZoom, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _cinemachineTransposer.m_FollowOffset = endZoom;
    }

    private void Update()
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.unscaledDeltaTime;
            if (_shakeTimer <= 0)
            {
                _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
            }
        }
        if (_zoomTimer > 0)
        {
            _zoomTimer -= Time.unscaledDeltaTime;
            if (_zoomTimer <= 0)
                _cinemachineTransposer.m_FollowOffset = new Vector3(0, 0, _defaultZoom);
        }
    }
}
