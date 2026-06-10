using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class FieldDragHandler : MonoBehaviour,
    IPointerDownHandler, IDragHandler
{
    [SerializeField] private RectTransform field;
    [SerializeField] private float maxDistance = 300f;

    private Vector2 _startPosition;
    private Vector2 _pointerOffset;

    void Awake()
    {
        _startPosition = field.localPosition;
    }

    public void OnPointerDown(PointerEventData e)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            field.parent as RectTransform,
            e.position,
            e.pressEventCamera,
            out Vector2 localPoint);

        _pointerOffset = (Vector2)field.localPosition - localPoint;
    }

    public void OnDrag(PointerEventData e)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            field.parent as RectTransform,
            e.position,
            e.pressEventCamera,
            out Vector2 localPoint);

        Vector2 targetPos = localPoint + _pointerOffset;

        Vector2 delta = targetPos - _startPosition;
        if (delta.magnitude > maxDistance)
            targetPos = _startPosition + delta.normalized * maxDistance;

        field.localPosition = targetPos;
    }
}