using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class TooltipUI : MonoBehaviour
    {
        public static TooltipUI Instance { get; private set; }
        [SerializeField] private RectTransform canvasRectTransform;
        
        private TextMeshProUGUI _textMeshPro;
        private RectTransform _rectTransform;
        private RectTransform _backgroundRectTransform;
        private TooltipTimer _tooltipTimer;
        private void Awake()
        {
            Instance = this;
            _rectTransform = GetComponent<RectTransform>();
            _textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
            _backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
         
            Hide();
        }

        private void Update()
        {
            TooltipPositionConstraint();

            if (_tooltipTimer == null) return;
            _tooltipTimer.timer -= Time.deltaTime;
            if (_tooltipTimer.timer <= 0)
            {
                Hide();
            }
        }

        private void TooltipPositionConstraint()
        {
            var anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.y;
            anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, 0,
                canvasRectTransform.rect.width - _backgroundRectTransform.rect.width);
            anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, 0,
                canvasRectTransform.rect.height - _backgroundRectTransform.rect.height);
            _rectTransform.anchoredPosition = anchoredPosition;
        }

        private void SetText(string tooltipText)
        {
            _textMeshPro.SetText(tooltipText);
            _textMeshPro.ForceMeshUpdate();

            var paddingText = new Vector2(8, 8);
            var textSize = _textMeshPro.GetRenderedValues(false);
            _backgroundRectTransform.sizeDelta = textSize + paddingText;
        }

        public void Show(string tooltipText, TooltipTimer tooltipTimer = null)
        {
            _tooltipTimer = tooltipTimer;
            gameObject.SetActive(true);
            SetText(tooltipText);
            TooltipPositionConstraint();
        }
        public void Hide()
        { 
            gameObject.SetActive(false);
        }

        public class TooltipTimer
        {
            public float timer;
        }
    }
}
