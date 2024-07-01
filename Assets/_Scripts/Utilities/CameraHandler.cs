using System;
using Cinemachine;
using UnityEngine;

namespace _Scripts.Utilities
{
    public class CameraHandler : MonoBehaviour
    {
        public static CameraHandler Instance { get; private set; }
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private float moveSpeed = 30;
        [SerializeField] private float zoomSpeed = 2;
        [SerializeField] private float minZoom = 5;
        [SerializeField] private float maxZoom = 20;

        private float _orthographicSize;
        private float _targetOrthographicSize;
        private bool _edgeScrolling;

        private const string HORIZONTAL = "Horizontal";
        private const string VERTICAL = "Vertical";
        private const string EDGE_SCROLLING = "edgeScrolling";

        private void Awake()
        {
            Instance = this;

            _edgeScrolling= PlayerPrefs.GetInt(EDGE_SCROLLING, 1) == 1;
        }

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
            _targetOrthographicSize += -Input.mouseScrollDelta.y * zoomSpeed;
            _targetOrthographicSize = Mathf.Clamp(_targetOrthographicSize, minZoom, maxZoom);
            _orthographicSize = Mathf.Lerp(_orthographicSize, _targetOrthographicSize, Time.deltaTime * zoomSpeed);
            if (virtualCamera)
                virtualCamera.m_Lens.OrthographicSize = _orthographicSize;
        }

        private void HandleInput()
        {
            var x = Input.GetAxisRaw(HORIZONTAL);
            var y = Input.GetAxisRaw(VERTICAL);

            if (_edgeScrolling)
            {
                const int edgeScrollingSize = 30;
                if (Input.mousePosition.x > Screen.width - edgeScrollingSize)
                {
                    x = 1f;
                }
                if(Input.mousePosition.x < edgeScrollingSize)
                {
                    x = -1f;
                }
                if (Input.mousePosition.y > Screen.height - edgeScrollingSize)
                {
                    y = 1f;
                }
                if(Input.mousePosition.y < edgeScrollingSize)
                {
                    y = -1f;
                }
            }
            
            var moveDir = new Vector3(x, y, 0).normalized;
            transform.position += moveDir * (moveSpeed * Time.deltaTime);
        }

        public bool GetEdgeScrolling() => _edgeScrolling;
        public void SetEdgeScrolling(bool edgeScrolling)
        {
            _edgeScrolling = edgeScrolling;
            PlayerPrefs.SetInt(EDGE_SCROLLING,_edgeScrolling ? 1 : 0);
        }
    }
}
