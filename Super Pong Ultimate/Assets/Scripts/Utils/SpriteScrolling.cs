using UnityEngine;

namespace Application.Utils
{
    public class SpriteScrolling : MonoBehaviour
    {
        [SerializeField] private float _maxTimeToScaleWidth = 0;
        [SerializeField] private float _maxTimeToScaleHeight = 0;

        private SpriteRenderer _spriteRenderer;
        private Vector2 _minSize;
        private Vector2 _maxSize;

        protected virtual void OnEnable()
        {
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }

            _minSize = _spriteRenderer.size;
            _maxSize = _minSize * 3f;
        }

        protected virtual void Update()
        {
            Vector2 size = _spriteRenderer.size;
            float t;
            if (_maxTimeToScaleWidth > 0)
            {
                t = Mathf.InverseLerp(0f, _maxTimeToScaleWidth, Time.time % _maxTimeToScaleWidth);
                size.x = Mathf.Lerp(_minSize.x, _maxSize.x, t);
            }
            if (_maxTimeToScaleHeight > 0)
            {
                t = Mathf.InverseLerp(0f, _maxTimeToScaleHeight, Time.time % _maxTimeToScaleHeight);
                size.y = Mathf.Lerp(_minSize.y, _maxSize.y, t);
                Debug.Log(t);
            }
            _spriteRenderer.size = size;
        }
    }
}