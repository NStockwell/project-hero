using UnityEngine;

namespace InputSystem
{
    public class ActionSystem : MonoBehaviour
    {
        private InputManager _inputManager;
        private Camera _cameraMain;
        private void Awake()
        {
            _inputManager = InputManager.Instance;
            _cameraMain = Camera.main;
        }

        private void OnEnable()
        {
            _inputManager.OnStartTouch += Move;
        }

        private void OnDisable()
        {
            InputManager.Instance.OnEndTouch += Move;
        }

        public void Move(Vector2 screenPosition, float time)
        {
            Vector3 screenCoords = new Vector3(screenPosition.x, screenPosition.y, _cameraMain.nearClipPlane);
            Vector3 worldCoords = _cameraMain.ScreenToWorldPoint(screenCoords);
            worldCoords.z = 0;
            transform.position = worldCoords;
        }
    }
}
