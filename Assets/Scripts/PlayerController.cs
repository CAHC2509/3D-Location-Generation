using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement and jump settings")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float runSpeed = 17.5f;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private float gravity = 20f;

    [Space, Header("Camera settings")]
    [SerializeField] private Camera firstPersonCamera;
    [SerializeField] private Camera thirdPersonCamera;
    [SerializeField] private float lookSpeed = 2f;
    [SerializeField] private float lookXLimit = 45f;
    [SerializeField] private bool firstPersonMode = false;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    // Singleton instance
    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("PlayerController instance already exists. Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Handles player movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical");
        float curSpeedY = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal");
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Handles player jumping
        if (Input.GetButton("Jump") && characterController.isGrounded)
            moveDirection.y = jumpPower;
        else
            moveDirection.y = movementDirectionY;

        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        // Apply movement
        characterController.Move(moveDirection * Time.deltaTime);

        if (firstPersonMode)
        {
            // Handles camera rotation
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            firstPersonCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    /// <summary>
    /// Alternates between two camera modes (first-person and third-person). It turns one camera on and the other off while resetting the object's rotation
    /// </summary>
    public void ToggleCameras()
    {
        firstPersonMode = !firstPersonMode;

        firstPersonCamera.gameObject.SetActive(firstPersonMode);
        thirdPersonCamera.gameObject.SetActive(!firstPersonMode);

        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
