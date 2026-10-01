using UnityEngine;

public class CardHover : MonoBehaviour
{

    [SerializeField] private Collider cardCollider; // Reference to the card's collider component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnMouseEnter(Collider cardCollider)
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f); // Increase the scale of the card when hovered over
        Debug.Log("Mouse entered card: " + gameObject.name); // Log the name of the card when hovered over
    }

    private void OnMouseExit()
    {
        transform.localScale = new Vector3(1f, 1f, 1f); // Reset the scale of the card when not hovered over
        Debug.Log("Mouse exited card: " + gameObject.name); // Log the name of the card when not hovered over
    }

}
