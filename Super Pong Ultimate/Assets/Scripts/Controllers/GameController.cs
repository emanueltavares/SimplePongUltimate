using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Controllers
{
    public class GameController : MonoBehaviour
    {
        // Serialized Variables
        [Header("UI")]
        [SerializeField] private Text _scoreLeftText;
        [SerializeField] private Text _scoreRightText;
        [SerializeField] private Text _status;
        [SerializeField] private float _timePerCharStatus = 0.15f;

        [Header("Game")]
#pragma warning disable CS0649
        [SerializeField] private BallController _ball;
#pragma warning restore CS0649
        [SerializeField] private Vector2 _serveDirection = Vector2.right;
        [SerializeField] private float _startSpeed = 5f;
        [SerializeField] private int _maxScore = 5;

        // Properties
        public int ScoreLeft { get; set; }
        public int ScoreRight { get; set; }

        protected virtual void Start()
        {
            StartCoroutine(StartGame());
        }

        public IEnumerator StartGame()
        {
            if (_serveDirection == Vector2.right)
            {
                yield return StartCoroutine(SetStatusText("RIGHT SERVES", 3f));
            }
            else
            {
                yield return StartCoroutine(SetStatusText("LEFT SERVES", 3f));
            }

            yield return StartCoroutine(SetStatusText("READY", 1f));
            yield return StartCoroutine(SetStatusText("SET", 1f));
            yield return StartCoroutine(SetStatusText("GO", 1f));

            // Clean up
            _scoreLeftText.text = string.Empty;
            _scoreRightText.text = string.Empty;
            _status.text = string.Empty;

            // Initialize ball
            _ball.gameObject.SetActive(true);
            _ball.Direction = _serveDirection;
            _ball.Speed = _startSpeed;
        }

        private IEnumerator SetStatusText(string text, float duration = 0f)
        {
            for (int i = 0; i < text.Length; i++)
            {
                _status.text = text.Substring(0, i + 1);
                yield return new WaitForSeconds(_timePerCharStatus);
            }

            _status.text = text;
            float remainingTime = duration - (_timePerCharStatus * text.Length);
            yield return new WaitForSeconds(remainingTime);
        }

        public void OnLeftGoal()
        {
            StartCoroutine(ResetGame(true));
        }

        public void OnRightGoal()
        {
            // Reset Ball
            StartCoroutine(ResetGame(false));
        }

        private IEnumerator ResetGame(bool addRight)
        {
            // Reset Ball
            _ball.gameObject.SetActive(false);

            if (addRight)
            {
                // Set Serve to Left
                _serveDirection = Vector2.left;

                // Add Right Score
                ScoreRight += 1;
                _scoreLeftText.text = ScoreLeft.ToString();
                _scoreRightText.text = ScoreRight.ToString();

                yield return StartCoroutine(SetStatusText("RIGHT SCORES", 3f));

                if (ScoreRight < _maxScore)
                {
                    yield return StartCoroutine(StartGame());
                }
                else
                {
                    // end game
                    yield return StartCoroutine(SetStatusText("RIGHT WINS", 3f));
                }
            }
            else
            {
                // Add Left Score
                ScoreLeft += 1;
                _scoreLeftText.text = ScoreLeft.ToString();
                _scoreRightText.text = ScoreRight.ToString();

                // Set Serve to Right
                _serveDirection = Vector2.right;

                yield return StartCoroutine(SetStatusText("LEFT SCORES", 3f));

                if (ScoreLeft < _maxScore)
                {
                    yield return StartCoroutine(StartGame());
                }
                else
                {
                    // end game
                    yield return StartCoroutine(SetStatusText("LEFT WINS", 3f));
                }
            }
        }
    }
}