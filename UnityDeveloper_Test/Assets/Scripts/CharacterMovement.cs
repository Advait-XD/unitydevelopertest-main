using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private Animator animator;

    public Transform orientation; // Used for correct movement direction

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = (orientation.right * moveX + orientation.forward * moveZ).normalized;

        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity += -Physics.gravity.normalized * Mathf.Sqrt(jumpForce * 2f * 9.81f);
            animator.SetBool("isJumping", true);
        }

        playerVelocity += Physics.gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        animator.SetBool("isRunning", move.magnitude > 0);

        if (isGrounded && playerVelocity.y <= 0)
            animator.SetBool("isJumping", false);
    }
}
