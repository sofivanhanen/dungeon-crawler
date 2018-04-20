using UnityEngine;

namespace BehaviourControllers
{
    public class CameraMovementBehaviourController
    {
        // Calculates the position for an object that's following another object. Used for cameras.

        public Vector3 Offset;
        public float Interpolation;

        public Vector3 CalculateDirection(Vector3 currentPosition, Vector3 objectToFollowPosition, float deltaTime)
        {
            var newPosition = new Vector3();
            newPosition.x = Mathf.Lerp(currentPosition.x, objectToFollowPosition.x + Offset.x,
                Interpolation * deltaTime);
            newPosition.y = currentPosition.y; // We don't want the camera to change altitude
            newPosition.z = Mathf.Lerp(currentPosition.z, objectToFollowPosition.z + Offset.z,
                Interpolation * deltaTime);
            return newPosition;
        }
    }
}