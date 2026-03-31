using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    [SerializeField] InputActionAsset InputActions;
    InputActionMap actionMap;
    InputAction lookAction;

    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] float cameraHeight = 1.5f;
    [SerializeField] Vector2 pitchMinMax = new Vector2(-30, 80);
    [SerializeField] float cameraSensitivity = 1.0f;

    float yaw, pitch;

    private void Awake()
    {
        actionMap = InputActions.FindActionMap("Player");
        lookAction = actionMap.FindAction("Look");
    }
    private void OnEnable()
    {
        InputActions.Enable();
    }
    private void OnDisable()
    {
        InputActions.Disable();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 mouseInput = lookAction.ReadValue<Vector2>();
        yaw += mouseInput.x * cameraSensitivity;
        pitch -= mouseInput.y * cameraSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
        Quaternion desiredRotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = desiredRotation * new Vector3(0, 0, -3);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 10.0f * Time.deltaTime);

        Vector3 desiredPosition = playerTransform.position + Vector3.up * cameraHeight + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 10.0f * Time.deltaTime);
    }
}
