using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class DragManager : MonoBehaviour
{
    [SerializeField] private PuzzleDrag draggedObject;
    private Canvas canvas;
    private Level _level;

    [Inject]
    public void Constructor(Level level) => _level = level;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public void StartDrag(PuzzleDrag target)
    {
        draggedObject = target;
    }

    private void Update()
    {
        if (draggedObject == null)
            return;

        var touchscreen = Touchscreen.current;
        if (touchscreen != null)
        {
            if (touchscreen.primaryTouch.press.isPressed)
                DragTo(touchscreen.primaryTouch.position.ReadValue());
            else if (touchscreen.primaryTouch.press.wasReleasedThisFrame)
                StopDrag();
            return;
        }

        var mouse = Mouse.current;
        if (mouse != null)
        {
            if (mouse.leftButton.isPressed)
                DragTo(mouse.position.ReadValue());
            else if (mouse.leftButton.wasReleasedThisFrame)
                StopDrag();
        }
    }

    private void DragTo(Vector2 screenPos)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            screenPos,
            canvas.worldCamera,
            out Vector2 pos
        );
        draggedObject.GetComponent<RectTransform>().localPosition = pos;
    }

    public void StopDrag()
    {
        _level.TryPut(draggedObject);
        draggedObject = null;
    }
}