using UnityEngine;
using TrueInputSystem;

/// <summary>
/// Điều khiển model AR bằng TrueInputSystem:
///   - Joystick X/Y  → di chuyển trái/phải/tiến/lùi + tự xoay theo hướng đi
///   - Nút Up/Down   → tăng/giảm độ cao
/// </summary>
public class ARModelController : MonoBehaviour
{
    [Header("Tham chiếu")]
    [SerializeField] private ARTrackedImageSpawner spawner;

    [Header("Di chuyển (Joystick)")]
    [SerializeField] private float moveSpeed = 1f;
    [Tooltip("Tốc độ xoay model để quay theo hướng đi (độ/giây). Cao = xoay ngay, thấp = xoay mượt.")]
    [SerializeField] private float rotateSpeed = 10f;

    [Header("Độ cao (nút Up/Down)")]
    [SerializeField] private float heightSpeed = 0.5f;
    [SerializeField] private float minHeight = 0f;
    [SerializeField] private float maxHeight = 2f;

    [Header("Tên Action trong TIS Setup")]
    [SerializeField] private string upActionName   = "Up";
    [SerializeField] private string downActionName = "Down";

    private TISInputBridge _bridge;
    private GameObject     _model;

    private void Start()
    {
        _bridge = TISInputBridge.Instance;

        if (_bridge == null)
            Debug.LogError("[ARModelController] Không tìm thấy TISInputBridge trong scene!");

        if (spawner == null)
            spawner = FindFirstObjectByType<ARTrackedImageSpawner>();
    }

    private void Update()
    {
        _model = spawner != null ? spawner.SpawnedModel : null;

        if (_model == null || !_model.activeInHierarchy) return;
        if (_bridge == null) return;

        HandleMove();
        HandleHeight();
    }

    private void HandleMove()
    {
        Vector2 input = _bridge.MoveDirection;

        if (input.sqrMagnitude < 0.01f) return;

        // Di chuyển
        Vector3 delta = new Vector3(input.x, 0f, input.y) * moveSpeed * Time.deltaTime;
        _model.transform.position += delta;

        // Xoay model theo hướng đang đi (giống code mẫu Atan2 nhưng mượt hơn)
        float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
        _model.transform.rotation = Quaternion.Lerp(
            _model.transform.rotation,
            targetRotation,
            rotateSpeed * Time.deltaTime
        );
    }

    private void HandleHeight()
    {
        float direction = 0f;

        if (_bridge.IsSkillHeld(upActionName))   direction += 1f;
        if (_bridge.IsSkillHeld(downActionName)) direction -= 1f;

        if (Mathf.Approximately(direction, 0f)) return;

        Vector3 pos = _model.transform.position;
        pos.y = Mathf.Clamp(
            pos.y + direction * heightSpeed * Time.deltaTime,
            minHeight,
            maxHeight
        );
        _model.transform.position = pos;
    }
}