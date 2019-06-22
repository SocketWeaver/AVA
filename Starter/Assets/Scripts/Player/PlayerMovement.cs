using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 6.0F;
    public float Gravity = -15f;
    public float VerticalVelocity;
    public float JumpForce = 7.0f;
    Animator animator;
    Player player;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<Player>();

        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        cameraFollow.Target = transform;
    }

    void Update()
    {
        if (player.Dead)
        {
            return;
        }

        float speedX = Input.GetAxis("Horizontal");
        float speedY = Input.GetAxis("Vertical");

        Vector2 inputVector = new Vector2(speedY, speedX);

        if (inputVector.sqrMagnitude > 1)
        {
            inputVector.Normalize();
        }

        if (inputVector.magnitude > 0.01f)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        if (characterController.isGrounded)
        {
            VerticalVelocity = Gravity;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            VerticalVelocity += Gravity * Time.deltaTime;
        }

        animator.SetBool("Air", !characterController.isGrounded);

        Vector3 movement = transform.forward * inputVector.x * MoveSpeed + transform.right * inputVector.y * MoveSpeed + transform.up * VerticalVelocity;

        characterController.Move(movement * Time.deltaTime);
    }

    public void Jump()
    {
        VerticalVelocity = JumpForce;
    }
}
