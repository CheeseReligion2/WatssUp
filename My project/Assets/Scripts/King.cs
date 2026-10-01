using UnityEngine;
using UnityEngine.InputSystem;

public class King : MonoBehaviour
{
    public bool isOverACard;
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Card") || other.gameObject.CompareTag("Jack") || other.gameObject.CompareTag("Queen"))
        {
            isOverACard = true;
        }

        }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Card") || other.gameObject.CompareTag("Jack") || other.gameObject.CompareTag("Queen"))
        {
            isOverACard = false;
        }
    }

         void Update()
        {
            Mouse mouse = Mouse.current; 
            if (mouse.leftButton.wasPressedThisFrame && isOverACard)
            {
                 
            }
            
            }
        }

