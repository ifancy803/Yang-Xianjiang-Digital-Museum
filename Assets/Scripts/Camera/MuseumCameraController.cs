using UnityEngine;

[DisallowMultipleComponent]
public class MuseumCameraController : MonoBehaviour
{
    [Header("移动设置")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float fastMoveSpeed = 10f;

    [Header("旋转设置")]
    [SerializeField] private float lookSpeed = 2f;
    [SerializeField] private Vector2 verticalAngleLimit = new Vector2(-80f, 80f);

    [Header("跳跃设置")]
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -9.81f;

    [Header("相机")]
    [SerializeField] private Camera playerCamera; // 拖拽子物体 Camera 到这里

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float verticalVelocity = 0f;
    private float currentX = 0f;  // 水平旋转角度
    private float currentY = 0f;  // 垂直旋转角度

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("MuseumCameraController: 需要 CharacterController 组件");
        }

        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();

        if (playerCamera == null)
            Debug.LogError("MuseumCameraController: 未找到相机，请手动指定");

        // 锁定鼠标
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMouseLook();
        HandleMovement();
        HandleJumpAndGravity();
    }

    private void HandleMouseLook()
    {
        if (Cursor.lockState != CursorLockMode.Locked) return;

        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        currentX += mouseX;
        currentY -= mouseY;
        currentY = Mathf.Clamp(currentY, verticalAngleLimit.x, verticalAngleLimit.y);

        // 玩家自身绕 Y 轴旋转（水平转向）
        transform.rotation = Quaternion.Euler(0f, currentX, 0f);
        // 相机绕 X 轴旋转（俯仰）
        playerCamera.transform.localRotation = Quaternion.Euler(currentY, 0f, 0f);
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        forward.y = 0f;
        forward.Normalize();
        right.y = 0f;
        right.Normalize();

        Vector3 desiredMove = (forward * vertical + right * horizontal).normalized;
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? fastMoveSpeed : moveSpeed;
        Vector3 motion = desiredMove * currentSpeed * Time.deltaTime;
        motion.y = verticalVelocity * Time.deltaTime;

        characterController.Move(motion);
    }

    private void HandleJumpAndGravity()
    {
        bool isGrounded = characterController.isGrounded;
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (!isGrounded)
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }
}