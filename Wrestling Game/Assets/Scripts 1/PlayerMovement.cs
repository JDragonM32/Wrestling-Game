using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    CharacterController playerController;
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] Transform cameraTransform;
    InputActionMap actionMap;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction attackAction;
    float currentVelocity;

    public Vector2 moveInput;

    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSmoothTime = 0.1f;

    [SerializeField] float jumpHeight = 0.5f;
    bool isGrounded;
    float verticalVelocity = -2.0f;
    float gravity = -9.81f;

    void Awake()
    {
        actionMap = inputActions.FindActionMap("Player");
        moveAction = actionMap.FindAction("Move");
        jumpAction = actionMap.FindAction("Jump");
        attackAction = actionMap.FindAction("Attack");
    }
    void OnEnable()
    {
        actionMap.Enable();
    }
    void OnDisable()
    {
        actionMap.Disable();
    }
    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    void Update()
    { 
        isGrounded = playerController.isGrounded;
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        if (jumpAction.IsPressed() && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        verticalVelocity += gravity * Time.deltaTime;

        moveInput = moveAction.ReadValue<Vector2>();
        Vector3 moveDir = Vector3.zero;
        if (moveInput.magnitude > 0.1f)
        {
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, rotationSmoothTime);
            moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
        }
        Vector3 velocity = moveDir * speed + Vector3.up * verticalVelocity * gravity * -1;
        playerController.Move(velocity * Time.deltaTime);
    }
}
