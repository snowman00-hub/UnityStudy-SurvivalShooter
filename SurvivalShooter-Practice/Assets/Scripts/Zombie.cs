using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class Zombie : LivingEntity
{
    private static readonly int hashHasTarget = Animator.StringToHash("HasTarget");
    private static readonly int hashDie = Animator.StringToHash("Die");

    public enum Status
    {        
        Idle,
        Trace,
        Attack,
        Die
    }

    private Status currentStatus;

    public Status CurrentStatus
    {
        get { return currentStatus; }
        set
        {
            var prevStatus = currentStatus;
            currentStatus = value;

            switch (currentStatus)
            {
                case Status.Idle:
                    animator.SetBool(hashHasTarget, false);
                    agent.isStopped = true;
                    break;
                case Status.Trace:
                    animator.SetBool(hashHasTarget, true);
                    agent.isStopped = false;
                    break;
                case Status.Attack:
                    animator.SetBool(hashHasTarget, false);
                    agent.isStopped = true;
                    break;
                case Status.Die:
                    animator.SetTrigger(hashDie);
                    audioSource.PlayOneShot(deathClip);
                    agent.isStopped = true;
                    capsuleCollider.enabled = false;
                    break;
            }
        }
    }

    public float traceDistance = 10f;
    public float attackInterval = 0.5f;
    public float lastAttackTime;
    private float attackDistance = 0.7f;

    public ZombieData zombieData;

    public ParticleSystem hitEffect;
    public AudioClip hurtClip;
    public AudioClip deathClip;

    public LayerMask targetLayer;

    private float damage;
    private float score;

    private Transform target;
    private Animator animator;
    private AudioSource audioSource;
    private NavMeshAgent agent;

    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        SetUp();
    }

    public void SetUp()
    {
        maxHP = zombieData.maxHp;
        agent.speed = zombieData.speed;
        damage = zombieData.damage;
        score = zombieData.score;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        capsuleCollider.enabled = true;
        rb.isKinematic = true;
        currentStatus = Status.Idle;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
        hitEffect.transform.position = hitPoint;
        hitEffect.transform.forward = hitNormal;
        hitEffect.Play();
        audioSource.PlayOneShot(hurtClip);
    }

    protected override void Die()
    {
        base.Die();
        CurrentStatus = Status.Die;
    }

    public void StartSinking()
    {
        StartCoroutine(CoSinking());
    }

    private IEnumerator CoSinking()
    {
        yield return new WaitForSeconds(1.1f);

        rb.isKinematic = false;
        agent.enabled = false;

        Destroy(gameObject, 1f);
        while (true)
        {
            transform.position += Vector3.down * 1f * Time.deltaTime;
            yield return null;
        }
    }

    private void Update()
    {
        switch (currentStatus)
        {
            case Status.Idle:
                UpdateIdle();
                break;
            case Status.Trace:
                UpdateTrace();
                break;
            case Status.Attack:
                UpdateAttack();
                break;
            case Status.Die:
                UpdateDie();
                break;
        }
    }

    private void UpdateIdle()
    {
        if (target != null &&
            Vector3.Distance(transform.position, target.position) < traceDistance)
        {
            CurrentStatus = Status.Trace;
        }

        target = FindTarget(traceDistance);
    }

    private void UpdateTrace()
    {
        if (target != null &&
            Vector3.Distance(transform.position, target.position) < attackDistance)
        {
            CurrentStatus = Status.Attack;
            return;
        }

        if (target == null || 
            Vector3.Distance(transform.position, target.position) > traceDistance)
        {
            CurrentStatus = Status.Idle;
            return;
        }

        agent.SetDestination(target.position);
    }

    private void UpdateAttack()
    {
        if (target == null ||
            Vector3.Distance(transform.position, target.position) > attackDistance)
        {
            CurrentStatus = Status.Trace;
            return;
        }

        var lookAt = target.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);

        if (lastAttackTime + attackInterval < Time.time)
        {
            lastAttackTime = Time.time;
            var damagable = target.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.OnDamage(damage, transform.position, -transform.forward);
            }
        }
    }

    private void UpdateDie()
    {

    }

    private Transform FindTarget(float radius)
    {
        var colliders = Physics.OverlapSphere(transform.position, radius, targetLayer.value);
        if (colliders.Length == 0)
        {
            return null;
        }

        var target = colliders.OrderBy(
           x => Vector3.Distance(x.transform.position, transform.position)).First();
        return target.transform;
    }
}
