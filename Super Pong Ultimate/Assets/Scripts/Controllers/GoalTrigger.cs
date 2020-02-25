using Application.Controllers;
using Application.Utils;
using UnityEngine;
using UnityEngine.Events;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _goalEvent = new UnityEvent();

    protected virtual void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.layer == Constants.BallLayer)
        {
            // Goal Event
            _goalEvent?.Invoke();
        }
    }
}
