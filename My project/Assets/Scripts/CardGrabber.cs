using UnityEngine;
using UnityEngine.InputSystem;

public class CardGrabber : MonoBehaviour
{
    public bool Selected;
    private Plane dragPlane;
    private Vector3 dragOffset;

    void Update()
    {
        Mouse mouse = Mouse.current;
        Camera mainCamera = Camera.main;
        if (mouse == null || mainCamera == null)
            return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            Ray ray = mainCamera.ScreenPointToRay(mouse.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit) &&
                hit.collider.GetComponentInParent<CardGrabber>() == this)
            {
                Selected = true;
                dragPlane = new Plane(-mainCamera.transform.forward, transform.position);
                if (dragPlane.Raycast(ray, out float distance))
                    dragOffset = transform.position - ray.GetPoint(distance);
            }
        }

        if (Selected && mouse.leftButton.isPressed)
        {
            Ray ray = mainCamera.ScreenPointToRay(mouse.position.ReadValue());
            if (dragPlane.Raycast(ray, out float distance))
                transform.position = ray.GetPoint(distance) + dragOffset;
        }

        if (mouse.leftButton.wasReleasedThisFrame)
            Selected = false;
    }
}
