using UnityEngine;
using GameObjectControllers;

public class SwordController : MonoBehaviour
{
    private const int Damage = 20;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        _animator.SetTrigger("Sword_attack");
    }

    private void OnTriggerEnter(Collider colliderOfSecondObject)
    {
        // Possible because of multiple contact points, OnTriggerEnter gets called repeatedly. Handle this in GetHit.
        if (colliderOfSecondObject.gameObject.CompareTag("Enemy") && _animator.GetCurrentAnimatorStateInfo(0).IsName("Sword_swing"))
            colliderOfSecondObject.gameObject.GetComponent<GhostController>().GetHit(Damage);
    }
}