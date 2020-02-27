using UnityEngine;

namespace Application.Controllers
{
    public class AIPaddleController : MonoBehaviour
    {
#pragma warning disable CS0649
        [SerializeField] private Rigidbody2D _opponent;
        [SerializeField] private Rigidbody2D _ballRigidbody2D;
        [SerializeField] private float _maxDeltaPosition = 1.5f;
        [SerializeField] [Range(0.05f, 0.25f)] private float _absPaddleOffset = 0.15f;
#pragma warning restore CS0649

        private Rigidbody2D _rigidbody2D;

        public float MaxDeltaPosition { get => _maxDeltaPosition; set => _maxDeltaPosition = value; }

        protected virtual void OnEnable()
        {
            if (_rigidbody2D == null)
            {
                _rigidbody2D = GetComponent<Rigidbody2D>();
            }
        }

        protected virtual void Update()
        {
            // If ball is not active, paddle will move towards the center
            Vector2 targetPosition = new Vector2(_rigidbody2D.position.x, 0f);

            // if ball is active, and close we move towards it
            if (_ballRigidbody2D.gameObject.activeSelf)
            {
                float paddleOffset = _absPaddleOffset;
                if (_opponent.position.y < _ballRigidbody2D.position.y)
                {
                    paddleOffset = _absPaddleOffset * -1f;
                }

                targetPosition = new Vector2(_rigidbody2D.position.x, _ballRigidbody2D.position.y + paddleOffset);
            }

            Vector2 nextPosition = Vector2.MoveTowards(_rigidbody2D.position, targetPosition, MaxDeltaPosition * Time.deltaTime);
            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}