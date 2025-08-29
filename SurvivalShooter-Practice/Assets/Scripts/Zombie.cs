using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class Zombie : MonoBehaviour
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
                    agent.isStopped = true;
                    capsuleCollider.enabled = false;
                    break;
            }
        }
    }

    public float traceDistance = 10f;
    public float attackDistance = 0.3f;
    public LayerMask targetLayer;

    private Transform target;
    private Animator animator;
    private NavMeshAgent agent;
    private CapsuleCollider capsuleCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        capsuleCollider = GetComponent<CapsuleCollider>();
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
        CurrentStatus = Status.Idle; // 테스트코드

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
