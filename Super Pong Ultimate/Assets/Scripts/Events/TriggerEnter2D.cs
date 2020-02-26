using Application.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Application.Events
{
    public class TriggerEnter2D : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask = new LayerMask();
        [SerializeField] private UnityEvent _event = new UnityEvent();

        protected virtual void OnTriggerEnter2D(Collider2D collider2D)
        {
            // If layer mask contains the layer
            if (_layerMask == (_layerMask | (1 << collider2D.gameObject.layer)))
            {
                _event?.Invoke();
            }
        }
    }
}
