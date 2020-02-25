using UnityEngine;

namespace Application.Utils
{

    public class SpriteScrollHeight : MonoBehaviour
    {
        [SerializeField] private float _maxTime;

        private SpriteRenderer _spriteRenderer;
        private float _minHeight;
        private float _maxHeight;

        protected virtual void OnEnable()
        {
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }

            Vector2 size = _spriteRenderer.size;
            _minHeight = size.y;
            _maxHeight = _minHeight * 2;
        }

        protected virtual void Update()
        {
            float t = (Time.time % _maxTime) / _maxTime;
            float height = Mathf.Lerp(_minHeight, _maxHeight, t);
            Vector2 size = _spriteRenderer.size;
            size.y = height;
            _spriteRenderer.size = size;
        }
    }
}