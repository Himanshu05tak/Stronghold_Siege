using Cinemachine;
using UnityEngine;

namespace _Scripts.Utilities
{
    public class CameraHandler : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private float moveSpeed = 30;
        [SerializeField] private float zoomSpeed = 2;
        [SerializeField] private float minZoom = 5;
        [SerializeField] private float maxZoom = 20;

        private float _orthographicSize;
        private float _targetOrthographicSize;
        private void Start()
        {
            _orthographicSize = virtualCamera.m_Lens.OrthographicSize;
            _targetOrthographicSize = _orthographicSize;
        }

        private void Update()
        {
            HandleInput();
            HandleZoom();
        }

        private void HandleZoom()
        {
            _targetOrthographicSize += Input.mouseScrollDelta.y * zoomSpeed;
            _targetOrthographicSize = Mathf.Clamp(_targetOrthographicSize, minZoom, maxZoom);
            _orthographicSize = Mathf.Lerp(_orthographicSize, _targetOrthographicSize, Time.deltaTime * zoomSpeed);
            if (virtualCamera)
                virtualCamera.m_Lens.OrthographicSize = _orthographicSize;
        }

        private void HandleInput()
        {
            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetAxisRaw("Vertical");

            var moveDir = new Vector3(x, y, 0).normalized;
            transform.position += moveDir * (moveSpeed * Time.deltaTime);
        }
    }
}
