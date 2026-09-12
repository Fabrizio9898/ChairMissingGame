using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private float playerSpeed = 5.0f;
    public CharacterController controller;
    [Header("Input Actions")]
    public InputActionReference moveAction;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }

void Update()
    {
        Vector2 input=moveAction.action.ReadValue<Vector2>();
        // float x = Input.GetAxis("Horizontal");
        // float z = Input.GetAxis("Vertical");
        Vector3 moveDirection=new Vector3(input.x,0,input.y);
        controller.Move(
                 moveDirection * playerSpeed * Time.deltaTime
             );
    }
}