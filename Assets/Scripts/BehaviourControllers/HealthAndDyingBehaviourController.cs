using Interfaces;
using UnityEngine;

namespace BehaviourControllers
{
    // Controls the health and dying of objects implementing IObjectWithHealth
    public class HealthAndDyingBehaviourController
    {
        public readonly IObjectWithHealth ObjectWithHealth;

        public Color NormalColor;
        public Color HurtingColor;

        public int MaxHealth;
        public int Health;
        public bool Dead;

        public readonly float DamageTakingTimerMax;
        private float _timeSinceLastHit;

        public HealthAndDyingBehaviourController(IObjectWithHealth objectWithHealth, Color normalColor,
            Color hurtingColor, int maxHealth, float damageTakingTimerMax)
        {
            ObjectWithHealth = objectWithHealth;
            NormalColor = normalColor;
            HurtingColor = hurtingColor;
            MaxHealth = maxHealth;
            Health = MaxHealth; // Assuming we start at full health
            Dead = false; // Assuming we start alive
            DamageTakingTimerMax = damageTakingTimerMax;
            _timeSinceLastHit = damageTakingTimerMax;
        }

        // Returns true if damage was taken, false otherwise
        public bool GetHit(int damage, bool useTimer)
        {
            if (Dead) return false;

            // TODO: This timer might end up being redundant
            if (useTimer)
            {
                if (_timeSinceLastHit < DamageTakingTimerMax)
                {
                    return false;
                }
            }

            Health -= damage;
            if (Health <= 0)
            {
                Dead = true;
                ObjectWithHealth.Die();
                return true;
            }

            ObjectWithHealth.ChangeColor(HurtingColor);
            _timeSinceLastHit = 0f;
            return true;
        }

        // The 'master' object calls this so this controller can be in charge of color changing and other time-sensitive things
        public void Update(float deltaTime)
        {
            if (Dead) return;
            _timeSinceLastHit += deltaTime;
            if (_timeSinceLastHit >= DamageTakingTimerMax && _timeSinceLastHit - deltaTime < DamageTakingTimerMax)
            {
                // Changing from hurting to normal
                ObjectWithHealth.ChangeColor(NormalColor);
            }
        }

        // TODO: Regenerating health
        // TODO: Increasing max health
    }
}