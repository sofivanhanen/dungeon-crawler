using BehaviourControllers;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace GameObjectControllers
{
    public class PlayerController : MonoBehaviour, IObjectWithHealth
    {
        private const float SpeedOfMovement = 5.0f;
        private const float SpeedOfTurn = 0.15f;

        private HealthAndDyingBehaviourController _healthAndDying;

        public GameObject Sword;
        public Text Health;

        public bool Dead;

        private void Start()
        {
            _healthAndDying =
                new HealthAndDyingBehaviourController(this, new Color(1f, 1f, 1f), new Color(1f, 0.7f, 0.7f), 100, 1f);
            Dead = false;
        }

        private void Update()
        {
            if (Input.GetKey("escape")) Application.Quit();
            _healthAndDying.Update(Time.deltaTime);
            if (_healthAndDying.Dead) return;

            // Moving
            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");
            if (!(x.Equals(0f) && z.Equals(0f)))
            {
                var movement = new Vector3(x, 0, z);
                // TODO: Why does rotation work wrong? Player always faces opposite direction. '*-1* fixes it.
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement * -1), SpeedOfTurn);
                transform.Translate(movement * Time.deltaTime * SpeedOfMovement, Space.World);
            }

            // Shooting
            if (Input.GetMouseButtonDown(0))
            {
                var swordController = Sword.GetComponent<SwordController>();
                swordController.Attack();
            }

            Health.text = "Health: " + _healthAndDying.Health;
        }

        public void GetHit(int damage)
        {
            _healthAndDying.GetHit(damage, false);
        }

        public void ChangeColor(Color newColor)
        {
            gameObject.GetComponent<Renderer>().material.color = newColor;
        }

        public void Die()
        {
            Dead = true;
            ChangeColor(new Color(1f, 0.3f, 0.3f));
            Health.text = "Health: 0 :(";
        }
    }
}