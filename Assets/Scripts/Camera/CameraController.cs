using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineCamera freeLook;
    public InputActionReference lookAction;

    public float sensitivity = 0.12f;
    public float minPitch = -20f;
    public float maxPitch = 70f;

    private CinemachineOrbitalFollow orbit;

    private float yaw;
    private float pitch;

    void Start()
    {
        orbit = freeLook.GetComponent<CinemachineOrbitalFollow>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnEnable()
    {
        lookAction.action.Enable();
    }

    void OnDisable()
    {
        lookAction.action.Disable();
    }

    void Update()
    {
        Vector2 mouse = lookAction.action.ReadValue<Vector2>();

        yaw += mouse.x * sensitivity;
        pitch -= mouse.y * sensitivity;

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        orbit.HorizontalAxis.Value = yaw;
        orbit.VerticalAxis.Value = pitch;
    }
}