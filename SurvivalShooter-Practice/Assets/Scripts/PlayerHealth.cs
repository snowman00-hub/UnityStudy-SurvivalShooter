using System;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerHealth : LivingEntity
{
    private static readonly int hashDie = Animator.StringToHash("Die");

    public event Action OnHurt;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (IsDead)
            return;

        base.OnDamage(damage, hitPoint, hitNormal);
        OnHurt?.Invoke();
    }

    protected override void Die()
    {
        base.Die();

        animator.SetTrigger(hashDie);
    }

    public void RestartLevel()
    {

    }
}
