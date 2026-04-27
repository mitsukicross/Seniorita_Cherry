using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CherrySimpleMovement : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float rotationSpeed = 10f;
    public float jumpHeight = 1.2f;
    public float gravity = -20f;

    public Animator animator;

    private CharacterController controller;
    private Vector3 verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    void Update()
    {
        bool isGrounded = controller.isGrounded;

        if (isGrounded && verticalVelocity.y < 0)
        {
            verticalVelocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal"); // A y D
        float vertical = Input.GetAxisRaw("Vertical");     // W y S

        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? runSpeed : walkSpeed;

        if (inputDirection.magnitude > 0.1f)
        {
            // Dirección hacia donde debe mirar Cherry
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);

            // Giro suave
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            // Movimiento hacia adelante
            controller.Move(inputDirection * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump");
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);

        float animationSpeed = 0f;

        if (inputDirection.magnitude > 0.1f)
        {
            animationSpeed = isRunning ? 1f : 0.5f;
        }

        animator.SetFloat("Speed", animationSpeed);
        animator.SetBool("Grounded", isGrounded);
    }
}