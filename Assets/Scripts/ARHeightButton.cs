using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Gắn script này lên mỗi UI Button lên/xuống.
/// Dùng EventTrigger hoặc implement IPointer để nhận giữ nút liên tục.
///
/// Cách dùng:
///   1. Tạo 2 UI Button (UpButton, DownButton) trong Canvas
///   2. Gắn ARHeightButton lên mỗi button
///   3. Set isUpButton = true cho nút lên, false cho nút xuống
///   4. Gắn ARModelController vào field controller
///
/// Nếu bạn dùng TrueInputSystem action "HeightUp"/"HeightDown",
/// bỏ qua script này và dùng TISMobileSkillButton thay thế.
/// </summary>
public class ARHeightButton : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] private bool isUpButton = true;
    [SerializeField] private ARModelController controller;

    public bool IsHeld { get; private set; }

    private void Start()
    {
        if (controller == null)
            controller = FindFirstObjectByType<ARModelController>();
    }

    public void OnPointerDown(PointerEventData eventData) => IsHeld = true;
    public void OnPointerUp(PointerEventData eventData)   => IsHeld = false;
    public void OnPointerExit(PointerEventData eventData) => IsHeld = false;
}
