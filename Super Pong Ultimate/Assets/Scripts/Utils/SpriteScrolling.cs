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
            _maxSize = _minSize * 2f;
        }

        protected virtual void Update()
        {
            Vector2 size = _spriteRenderer.size;
            float t;
            if (_maxTimeToScaleWidth > 0)
            {
                t = (Time.time % _maxTimeToScaleWidth) / _maxTimeToScaleWidth;
                size.x = Mathf.Lerp(_minSize.x, _maxSize.x, t);
            }
            if (_maxTimeToScaleHeight > 0)
            {
                t = (Time.time % _maxTimeToScaleHeight) / _maxTimeToScaleHeight;
                size.y = Mathf.Lerp(_minSize.y, _maxSize.y, t);
            }
            _spriteRenderer.size = size;
        }
    }
}