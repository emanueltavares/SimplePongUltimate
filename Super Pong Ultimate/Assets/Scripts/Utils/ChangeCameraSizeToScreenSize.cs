using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Update Camera's orthographic size to screen size
    /// </summary>
    public class ChangeCameraSizeToScreenSize : MonoBehaviour
    {
#pragma warning disable CS0649
        [SerializeField] private Camera _camera;
#pragma warning restore CS0649
        [SerializeField] private Vector2 _targetScreenSize = new Vector2(1920f, 1080f);

        private float _originalOrthoGraphicSize;

        protected virtual void Awake()
        {
            _originalOrthoGraphicSize = _camera.orthographicSize;
        }

        protected virtual void Update()
        {
            float targetAspectRatio = _targetScreenSize.x / _targetScreenSize.y;
            _camera.orthographicSize = Mathf.Max(targetAspectRatio / _camera.aspect, 1) * _originalOrthoGraphicSize;
        }
    }
}
