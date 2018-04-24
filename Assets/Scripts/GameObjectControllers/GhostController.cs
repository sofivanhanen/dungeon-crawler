using UnityEngine;
using GameObjectControllers;

public class GhostController : MonoBehaviour
{
    private const int MaxRange = 5;
    private const float SpeedOfMovement = 1.0f;
    private const float SpeedOfTurn = 0.15f;
    private const float MinTimeBetweenAttacks = 1f;
    private const int Damage = 30;
    private const int MaxHealth = 40;
    private const float MinTimeBetweenGettingHit = 0.3f;
    
    private float _attackTimer;
    private int _health;
    private float _hitTimer;
    private readonly Color _hurtingColor = new Color(1f, 0.8f, 0.8f, 0.5f);
    private readonly Color _normalColor = new Color(1f, 1f, 1f, 0.5f);

    public GameObject Player;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        _attackTimer = MinTimeBetweenAttacks;
        _health = MaxHealth;
        _hitTimer = MinTimeBetweenAttacks;
    }

    private void Update()
    {
        _attackTimer += Time.deltaTime;
        if (_hitTimer < MinTimeBetweenGettingHit)
        {
            _hitTimer += Time.deltaTime;
            if (_hitTimer >= MinTimeBetweenGettingHit) gameObject.GetComponent<Renderer>().material.color = _normalColor;
        }

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
        if (collision.gameObject.tag == "Player" && _attackTimer >= MinTimeBetweenAttacks)
        {
            Player.GetComponent<PlayerController>().GetHit(Damage);
            _attackTimer = 0;
        }
    }

    public void GetHit(int damage)
    {
        if (_hitTimer < MinTimeBetweenGettingHit) return;
        _health -= damage;
        if (_health <= 0) Destroy(gameObject);
        _hitTimer = 0f;
        gameObject.GetComponent<Renderer>().material.color = _hurtingColor;
    }
}