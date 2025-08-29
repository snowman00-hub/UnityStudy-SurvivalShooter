using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static readonly int hashMove = Animator.StringToHash("IsMove");

    public float speed = 5.0f;

    private Rigidbody rb;
    private PlayerInput input;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        float h = input.HorizontalMove;
        float v = input.VerticalMove;

        Vector3 moveDir = (camForward * v + camRight * h).normalized;
        rb.linearVelocity = moveDir * speed;

        bool isMove = rb.linearVelocity.magnitude > 0;
        animator.SetBool(hashMove, isMove);
    }
}
