using Application.Utils;
using UnityEngine;

namespace Application.Controllers
{
    public class BallController : MonoBehaviour
    {
        // Serialized Variables
        [SerializeField] private float _maxDistFromCenter = 0.25f;

        // Variables
        private Rigidbody2D _rigidbody2D;
        private Vector2 _direction = Vector2.zero;

        // Properties
        public float Speed { get; set; }
        public Vector2 Direction { get => _direction; set => _direction = value; }

        protected virtual void OnEnable()
        {
            if (_rigidbody2D == null)
            {
                _rigidbody2D = GetComponent<Rigidbody2D>();
            }
        }

        protected virtual void OnDisable()
        {
            Speed = 0f;
            _direction = Vector2.zero;
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.position = Vector2.zero;
            transform.position = Vector2.zero;
        }

        protected virtual void Update()
        {
            Vector2 velocity = _direction * Speed;
            _rigidbody2D.velocity = velocity * Time.deltaTime;
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision2D)
        {
            // Obtain the normal by adding all normal from all contact points 
            Vector2 contactNormal = Vector2.zero;
            for (int i = 0; i < collision2D.contactCount; i++)
            {
                ContactPoint2D contactPoint = collision2D.GetContact(i);
                contactNormal += contactPoint.normal;
            }
            contactNormal /= collision2D.contactCount;

            if (contactNormal == Vector2.down || contactNormal == Vector2.up) // If contact normal is either down or up, we will flip Y
            {
                _direction.y = -_direction.y; // flip Y by negative Y
            }
            else if (contactNormal == Vector2.left || contactNormal == Vector2.right) // If contact normal is either left or right, we will flip X
            {
                _direction.x = -_direction.x; // flip X by negative X
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            // Change direction when colliding with player
            if (collider.gameObject.layer == Constants.PaddleTriggerLayer)
            {
                _direction.x = -_direction.x; // flip X by negative X

                // let's calculate Y direction
                float distFromCenter = Mathf.Abs(collider.transform.position.y - _rigidbody2D.position.y);
                float maxDirectionY = Mathf.Sign(_direction.y) * 0.5f;
                _direction.y = Mathf.Lerp(0f, maxDirectionY, distFromCenter / _maxDistFromCenter);

                // Scale magnitudes up, so we won't hinder the speed of our ball
                float magnitude = _direction.magnitude;
                _direction.x *= 1f / magnitude;
                _direction.y *= 1f / magnitude;
            }
        }
    }
}