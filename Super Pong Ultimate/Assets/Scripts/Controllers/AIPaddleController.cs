using Application.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Application.Controllers
{
    public class AIPaddleController : MonoBehaviour
    {
        [SerializeField] private Transform _ballTransform;
        [SerializeField] private float _maxSpeed = 5;

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
            float nextPositionY = Mathf.MoveTowards(_rigidbody2D.position.y, _ballTransform.position.y, _maxSpeed * Time.deltaTime);
            Vector2 nextPosition = new Vector2(_rigidbody2D.position.x, nextPositionY);
            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}