using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Controllers
{
    public class GameController : MonoBehaviour
    {
#pragma warning disable CS0649
        // Serialized Variables
        [Header("UI")]
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Text _scoreLeftText;
        [SerializeField] private Text _scoreRightText;
        [SerializeField] private Text _status;
        [SerializeField] private float _timePerCharStatus = 0.15f;

        [Header("Game")]
        [SerializeField] private PaddleController _leftPaddle;
        [SerializeField] private AIPaddleController _rightPaddle;
        [SerializeField] private BallController _ball;
        [SerializeField] private int _maxScore = 5;
        [SerializeField] private Vector2 _serveDirection = Vector2.right;

        [Header("Difficulty")]
        [SerializeField] private float _startBallSpeed = 5f;
        [SerializeField] private float _maxBallSpeed = 5f;
        [SerializeField] private int _hitsToMaxSpeed = 20;
        [SerializeField] private AnimationCurve _ballSpeedProgression = new AnimationCurve();
        [SerializeField] private float _startDeltaPosition = 1.5f;
        [SerializeField] private float _maxDeltaPosition = 2f;
        [SerializeField] private AnimationCurve _difficultyProgression = new AnimationCurve();
#pragma warning restore CS0649

        // Variables
        private int _hits = 0;

        // Properties
        public int ScoreLeft { get; set; }
        public int ScoreRight { get; set; }

        protected virtual void OnEnable()
        {
            StartCoroutine(ShowStartScreen());
        }

        private IEnumerator ShowStartScreen()
        {
            // Clean up
            _scoreLeftText.text = string.Empty;
            _scoreRightText.text = string.Empty;

            // Hide paddles
            _leftPaddle.gameObject.SetActive(false);
            _rightPaddle.gameObject.SetActive(false);

            // Show Game Name
            yield return StartCoroutine(SetStatusText("EXTREME PONG", 3f));

            // Show Start Game Button
            _startGameButton.gameObject.SetActive(true);
        }

        public void StartGame()
        {
            // Reset difficulty
            _rightPaddle.MaxDeltaPosition = _startDeltaPosition;
            
            // Reset Score
            ScoreLeft = 0;
            ScoreRight = 0;

            _leftPaddle.gameObject.SetActive(true);
            _rightPaddle.gameObject.SetActive(true);
            _startGameButton.gameObject.SetActive(false);
            StartCoroutine(StartMatch());
        }

        private IEnumerator StartMatch()
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
            _hits = 0;
            _ball.gameObject.SetActive(true);
            _ball.Direction = _serveDirection;
            _ball.Speed = _startBallSpeed;
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
                // Play sound
                //_audioSource.PlayOneShot(_rightScoresSound);

                // Set Serve to Left
                _serveDirection = Vector2.left;

                // Add Right Score
                ScoreRight += 1;
                _scoreLeftText.text = ScoreLeft.ToString();
                _scoreRightText.text = ScoreRight.ToString();

                yield return StartCoroutine(SetStatusText("RIGHT SCORES", 3f));

                if (ScoreRight < _maxScore)
                {
                    yield return StartCoroutine(StartMatch());
                }
                else
                {
                    // end game
                    yield return StartCoroutine(SetStatusText("RIGHT WINS", 3f));
                    yield return StartCoroutine(ShowStartScreen());
                }
            }
            else
            {
                // Play sound
                //_audioSource.PlayOneShot(_leftScoresSound);

                // Add Left Score
                ScoreLeft += 1;
                _scoreLeftText.text = ScoreLeft.ToString();
                _scoreRightText.text = ScoreRight.ToString();

                // Set Serve to Right
                _serveDirection = Vector2.right;

                yield return StartCoroutine(SetStatusText("LEFT SCORES", 3f));

                if (ScoreLeft < _maxScore)
                {
                    IncreaseAILevel();
                    yield return StartCoroutine(StartMatch());
                }
                else
                {
                    // end game
                    yield return StartCoroutine(SetStatusText("LEFT WINS", 3f));
                    yield return StartCoroutine(ShowStartScreen());
                }
            }
        }

        public void IncreaseBallSpeed()
        {
            _hits += 1;

            float normalizedHits = Mathf.InverseLerp(0, _hitsToMaxSpeed, _hits);
            float difficulty = _ballSpeedProgression.Evaluate(normalizedHits);
            _ball.Speed = Mathf.Lerp(_startBallSpeed, _maxBallSpeed, difficulty);

            Debug.LogFormat("Increase Ball Speed to {0}", _ball.Speed);
        }

        private void IncreaseAILevel()
        {
            float normalizedLeftScore = Mathf.InverseLerp(0, _maxScore - 1, ScoreLeft);
            float difficulty = _difficultyProgression.Evaluate(normalizedLeftScore);
            _rightPaddle.MaxDeltaPosition = Mathf.Lerp(_startDeltaPosition, _maxDeltaPosition, difficulty);

            Debug.LogFormat("Increase AI Level to {0}", _rightPaddle.MaxDeltaPosition);
        }
    }
}