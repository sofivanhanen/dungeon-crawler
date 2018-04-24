using BehaviourControllers;
using Interfaces;
using UnityEngine;

namespace GameObjectControllers
{
    public class GhostController : MonoBehaviour, IObjectWithHealth
    {
        private const int MaxRange = 5;
        private const float SpeedOfMovement = 1.0f;
        private const float SpeedOfTurn = 0.15f;
        private const float MinTimeBetweenAttacks = 1f;
        private float _attackTimer;
        private const int Damage = 30;

        private HealthAndDyingBehaviourController _healthAndDying;

        public GameObject Player;

        private void Start()
        {
            _healthAndDying = new HealthAndDyingBehaviourController(this, new Color(1f, 1f, 1f, 0.5f),
                new Color(1f, 0.8f, 0.8f, 0.5f), 40, 0.3f);
            Player = GameObject.FindWithTag("Player");
            _attackTimer = MinTimeBetweenAttacks;
        }

        private void Update()
        {
            _attackTimer += Time.deltaTime;
            _healthAndDying.Update(Time.deltaTime);
            // No check for death here as if we died, we should be destroyed by now

            // TODO: Movement controller for ghost
            var heading = Player.transform.position - transform.position;
            // Using sqrMagnitude to save processing power
            if (heading.sqrMagnitude < MaxRange * MaxRange)
            {
                // Player is within range! Let's get 'em!!
                var distance = heading.magnitude;
                var movement = heading / distance;
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement * -1), SpeedOfTurn);
                transform.Translate(movement * Time.deltaTime * SpeedOfMovement, Space.World);
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            // TODO: Attack controller for ghost
            if (collision.gameObject.CompareTag("Player") && _attackTimer >= MinTimeBetweenAttacks)
            {
                Player.GetComponent<PlayerController>().GetHit(Damage);
                _attackTimer = 0;
            }
        }

        public void GetHit(int damage)
        {
            _healthAndDying.GetHit(damage, true);
        }

        public void ChangeColor(Color newColor)
        {
            gameObject.GetComponent<Renderer>().material.color = newColor;
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}