using UnityEngine;
using BehaviourControllers;

namespace GameObjectControllers
{
    // Our main camera controller
    // Tested in CameraBehaviourTest
    public class CameraController : MonoBehaviour
    {
        public GameObject Player;
        private CameraMovementBehaviourController _movement;

        private void OnEnable()
        {
            _movement = new CameraMovementBehaviourController();
            _movement.Interpolation = 5.0f;
            _movement.Offset = transform.position - Player.transform.position;
        }

        private void LateUpdate()
        {
            // Moving in LateUpdate so the object we're following has already moved
            transform.position =
                _movement.CalculatePosition(transform.position, Player.transform.position, Time.deltaTime);
        }
    }
}