using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FreeFlyCamera : MonoBehaviour
{
    [Header("基础移动速度")]
    public float moveSpeed = 8f;

    //[Header]
    public float fastMoveMultiplier = 2.5f; // Shift加速倍数

    [Header("视角旋转速度")]
    public float lookSpeed = 2f;

    [Header("上下升降速度")]
    public float upDownSpeed = 5f;

    // 存储相机当前旋转值
    private float _pitch;
    private float _yaw;

    void Start()
    {
        // 初始化相机视角
        Vector3 rot = transform.eulerAngles;
        _pitch = rot.x;
        _yaw = rot.y;
    }

    void Update()
    {
        // 只有按住鼠标右键时，才响应视角旋转和移动（和Scene窗口一致）
        if (Input.GetMouseButton(1))
        {
            // 隐藏并锁定鼠标
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // 1. 鼠标旋转视角
            HandleMouseLook();

            // 2. WASD + QE 移动
            HandleMovement();
        }
        else
        {
            // 松开右键恢复鼠标
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    /// <summary>
    /// 鼠标控制视角旋转
    /// </summary>
    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        _yaw += mouseX;
        _pitch -= mouseY;

        // 限制上下视角角度，防止相机翻转
        _pitch = Mathf.Clamp(_pitch, -89f, 89f);

        // 应用旋转
        transform.rotation = Quaternion.Euler(_pitch, _yaw, 0f);
    }

    /// <summary>
    /// 键盘控制移动
    /// </summary>
    void HandleMovement()
    {
        // 获取输入
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D
        float vertical = Input.GetAxisRaw("Vertical");     // W/S
        float upDown = 0f;

        // Q = 下降, E = 上升
        if (Input.GetKey(KeyCode.Q)) upDown = -1f;
        if (Input.GetKey(KeyCode.E)) upDown = 1f;

        // 计算移动方向
        Vector3 dir = transform.right * horizontal + transform.forward * vertical + transform.up * upDown;
        dir.Normalize(); // 防止斜向移动加速

        // Shift 加速
        float speed = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * fastMoveMultiplier : moveSpeed;

        // 移动相机
        transform.Translate(dir * speed * Time.deltaTime, Space.World);
    }
}