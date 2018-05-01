using Interfaces;
using UnityEngine;

namespace BehaviourControllers
{
    public class EnemyMovementBehaviourController
    {
        // Calculates the position for an enemy
        // Enemies move towards the player if the player is in range. If not, they stay still.

        private readonly IEnemyFollowing _enemy;
        private readonly int _maxRange;
        private readonly float _speedOfMovement;
        private readonly float _speedOfTurn;

        public EnemyMovementBehaviourController(IEnemyFollowing enemy, int maxRange, float speedOfMovement,
            float speedOfTurn)
        {
            _enemy = enemy;
            _maxRange = maxRange;
            _speedOfMovement = speedOfMovement;
            _speedOfTurn = speedOfTurn;
        }

        public void Update(Vector3 currentPosition, Quaternion currentRotation, Vector3 objectToFollowPosition,
            float deltaTime)
        {
            var heading = objectToFollowPosition - currentPosition;
            // Using sqrMagnitude to save processing power
            if (heading.sqrMagnitude < _maxRange * _maxRange)
            {
                // Object is within range! Let's get 'em!!
                var distance = heading.magnitude;
                var movement = heading / distance;
                _enemy.Move(movement * deltaTime * _speedOfMovement);
                _enemy.Rotate(Quaternion.Slerp(currentRotation, Quaternion.LookRotation(movement * -1), _speedOfTurn));
            }
        }
    }
}