using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera freeLook;
    [SerializeField] private InputActionReference lookAction;
    [SerializeField] private float sensitivity = 0.12f;

    private CinemachineOrbitalFollow orbit;

    private void Awake()
    {
        orbit = freeLook.GetComponent<CinemachineOrbitalFollow>();
    }

    private void OnEnable()
    {
        lookAction.action.Enable();
    }

    private void OnDisable()
    {
        lookAction.action.Disable();
    }

    private void Update()
    {
        Vector2 mouse = lookAction.action.ReadValue<Vector2>();

        orbit.HorizontalAxis.Value += mouse.x * sensitivity;
        orbit.VerticalAxis.Value -= mouse.y * sensitivity;
    }
}