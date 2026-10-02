using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using DG.Tweening;

public class CardGrabber : MonoBehaviour
{
    public bool selected;

    public bool placed;
    private Plane dragPlane;
    private Vector3 dragOffset;

    public GameObject grabbedCard;

    public Card2 cardScript;

    public HandManagerScript handManagerScript;


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
                grabbedCard = hit.collider.gameObject;
                cardScript = grabbedCard.GetComponent<Card2>();

                selected = true;
                dragPlane = new Plane(-mainCamera.transform.forward, transform.position);
                if (dragPlane.Raycast(ray, out float distance))
                    dragOffset = transform.position - ray.GetPoint(distance);

                grabbedCard.transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 0.5f); // Rotate the card to a flat orientation when grabbed
            }


        }

        if (selected && mouse.leftButton.isPressed)
        {
            Ray ray = mainCamera.ScreenPointToRay(mouse.position.ReadValue());
            if (dragPlane.Raycast(ray, out float distance))
                transform.position = ray.GetPoint(distance) + dragOffset;
        }

        if (mouse.leftButton.wasReleasedThisFrame && selected)
        {
                        selected = false;
                        
                        
                        
                        
                        
                        if (cardScript.isOnBoard == true)
                        {
                            
                        
                            handManagerScript.handCards.Remove(this.gameObject);
                            handManagerScript.UpdateCardPositions(); // Update the card positions in the hand when the card is released
                            
                            


                        }
    
                        else
                        {
                          handManagerScript.UpdateCardPositions(); // Update the card positions in the hand when the card is released
                        
                        }

          
        }
            
    }
}


