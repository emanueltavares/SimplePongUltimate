using Application.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Application.Controllers
{
    public class AIPaddleController : MonoBehaviour
    {
#pragma warning disable CS0649
        [SerializeField] private Rigidbody2D _ballRigidbody2D;
        [SerializeField] private float _maxDeltaPosition = 1.5f;
#pragma warning restore CS0649

        private Rigidbody2D _rigidbody2D;

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
                targetPosition = new Vector2(_rigidbody2D.position.x, _ballRigidbody2D.position.y);
            }

            Vector2 nextPosition = Vector2.MoveTowards(_rigidbody2D.position, targetPosition, _maxDeltaPosition * Time.deltaTime);
            _rigidbody2D.MovePosition(nextPosition);

        }
    }
}