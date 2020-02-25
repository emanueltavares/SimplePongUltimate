using Application.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Application.Controllers
{
    public class PaddleController : MonoBehaviour
    {
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
            float verticalInput = Input.GetAxisRaw("Vertical");
            float currentSpeed = verticalInput * _maxSpeed * Time.deltaTime;
            Vector2 up = transform.TransformDirection(Vector2.up);
            _rigidbody2D.velocity = up * currentSpeed;
        }
    }
}