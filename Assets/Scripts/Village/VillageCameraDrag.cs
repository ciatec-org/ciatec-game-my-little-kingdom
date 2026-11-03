using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public sealed class VillageCameraDrag : MonoBehaviour
{
    public Camera mapCamera;
    public Rect bounds = new Rect(-21.875f, -13.541667f, 43.75f, 27.083334f);
    public bool Locked { get; set; }
    public bool DraggedThisGesture { get; private set; }

    bool dragging;
    Vector2 previous, start;
    void Update()
    {
        Vector2 point;
        bool pressed, held;
        var touch = Touchscreen.current;
        if (touch != null && touch.primaryTouch.press.isPressed)
        {
            point = touch.primaryTouch.position.ReadValue();
            pressed = touch.primaryTouch.press.wasPressedThisFrame;
            held = true;
        }
        else if (Mouse.current != null)
        {
            point = Mouse.current.position.ReadValue();
            pressed = Mouse.current.leftButton.wasPressedThisFrame;
            held = Mouse.current.leftButton.isPressed;
        }
        else
            return;
        if (pressed)
        {
            DraggedThisGesture = false;
            dragging = !Locked && !OverlayAt(point);
            previous = start = point;
        }

        if (!held || Locked)
        {
            dragging = false;
            return;
        }

        if (!dragging)
            return;
        if ((point - start).sqrMagnitude > 64)
            DraggedThisGesture = true;
        if (DraggedThisGesture)
        {
            Vector3 delta = mapCamera.ScreenToWorldPoint(new Vector3(previous.x, previous.y, -mapCamera.transform.position.z)) - mapCamera.ScreenToWorldPoint(new Vector3(point.x, point.y, -mapCamera.transform.position.z));
            Focus((Vector2)transform.position + (Vector2)delta);
        }

        previous = point;
    }

    bool OverlayAt(Vector2 point)
    {
        if (EventSystem.current == null)
            return false;
        var hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current) { position = point }, hits);
        foreach (var hit in hits)
        {
            var canvas = hit.gameObject.GetComponentInParent<Canvas>();
            if (canvas != null && canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                return true;
        }

        return false;
    }

    public void Focus(Vector2 position)
    {
        float h = mapCamera.orthographicSize, w = h * mapCamera.aspect;
        position.x = bounds.width <= w * 2 ? bounds.center.x : Mathf.Clamp(position.x, bounds.xMin + w, bounds.xMax - w);
        position.y = bounds.height <= h * 2 ? bounds.center.y : Mathf.Clamp(position.y, bounds.yMin + h, bounds.yMax - h);
        transform.position = new Vector3(position.x, position.y, -10);
    }

    void OnDisable()
    {
        dragging = false;
    }
}
