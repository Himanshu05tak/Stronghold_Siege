using Cinemachine;
using UnityEngine;

namespace _Scripts.Effects
{
    public class CameraShake : MonoBehaviour
    {
        public static CameraShake Instance { get; private set; }
        
        private CinemachineVirtualCamera _virtualCamera;
        private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

        private float _timer;
        private float _timerMax;
        private float _intensity;
        private void Awake()
        {
            Instance = this;
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _cinemachineBasicMultiChannelPerlin =  _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private void Update()
        {
            if (!(_timer < _timerMax)) return;
            _timer += Time.deltaTime;
            var amplitude = Mathf.Lerp(_intensity, 0, _timer/_timerMax);
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitude;
        }

        public void ShakeCamera(float intensity, float timerMax)
        {
            _timerMax = timerMax;
            _timer = 0;
            _intensity = intensity;
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        }
    }
}
