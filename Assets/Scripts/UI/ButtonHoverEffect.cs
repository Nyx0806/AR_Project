using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float hoverScale = 1.1f;
    [SerializeField] private float duration = 0.2f;

    private Vector3 defaultScale;

    private void Start()
    {
        defaultScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(defaultScale * hoverScale, duration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(defaultScale, duration);
    }
}