using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float SpeedOfMovement = 5.0f;
    private const float SpeedOfTurn = 0.15f;
    private const float MaxTimeShowingHurtingColor = 1f;

    public bool Dead;
    public int MaxHealth;
    public int Health;
    private float _timeSinceHit;
    private readonly Color _hurtingColor = new Color(1f, 0.7f, 0.7f);
    private readonly Color _normalColor = new Color(1f, 1f, 1f);

    public GameObject Sword;

    private void Start()
    {
        MaxHealth = 100;
        Health = MaxHealth;
        Dead = false;
        _timeSinceHit = 0f;
    }

    private void Update()
    {
        if (Input.GetKey("escape")) Application.Quit();

        if (Dead) return;

        if (_timeSinceHit < MaxTimeShowingHurtingColor)
        {
            _timeSinceHit += Time.deltaTime;
            if (_timeSinceHit >= MaxTimeShowingHurtingColor)
                gameObject.GetComponent<Renderer>().material.color = _normalColor;
        }

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
    }

    public void GetHit(int damage)
    {
        if (Dead) return;
        Health -= damage;
        if (Health <= 0) Dead = true;
        gameObject.GetComponent<Renderer>().material.color = _hurtingColor;
        _timeSinceHit = 0f;
    }
}