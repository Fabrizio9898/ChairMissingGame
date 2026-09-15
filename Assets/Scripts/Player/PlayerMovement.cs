using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float walkSpeed = 1.5f;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float rotationSpeed = 360f;

    [Header("Gravedad")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundedGravity = -2f;

    [Header("Input")]
    public InputActionReference moveAction;

    private CharacterController controller;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        moveAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
    }

    void Update()
    {
        Camera viewCamera = Camera.main;

        Vector2 input = moveAction.action.ReadValue<Vector2>();

        Vector3 forward = viewCamera.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = viewCamera.transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 direction =
            right * input.x +
            forward * input.y;

        bool running =
            Keyboard.current.leftShiftKey.isPressed;

        float speed = running ? runSpeed : walkSpeed;

        if (controller.isGrounded)
        {
            verticalVelocity = groundedGravity;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 movement = direction * speed;
        movement.y = verticalVelocity;

        controller.Move(movement * Time.deltaTime);

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}