using UnityEngine;

namespace Application.Controllers
{
    public class GameController : MonoBehaviour
    {
        // Serialized Variables

        [Header("Ball")]
#pragma warning disable CS0649
        [SerializeField] private BallController _ball;
#pragma warning restore CS0649
        [SerializeField] private Vector2 _serveDirection = Vector2.right;
        [SerializeField] private float _startSpeed = 5f;

        // Properties
        public int ScoreLeft { get; set; }
        public int ScoreRight { get; set; }

        protected virtual void OnEnable()
        {
            StartGame();
        }

        public void StartGame()
        {
            _ball.gameObject.SetActive(true);
            _ball.Direction = _serveDirection;
            _ball.Speed = _startSpeed;
        }

        public void OnLeftGoal()
        {
            // Reset Ball
            _ball.gameObject.SetActive(false);

            // Add Right Score
            ScoreRight += 1;

            // Set Serve to Left
            _serveDirection = Vector2.left;
        }

        public void OnRightGoal()
        {
            // Reset Ball
            _ball.gameObject.SetActive(false);

            // Add Left Score
            ScoreLeft += 1;

            // Set Serve to Right
            _serveDirection = Vector2.right;
        }
    }
}