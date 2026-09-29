using UnityEngine;


public class BoardP1 : MonoBehaviour
{
    public Card2 cardScript;
    public float handValue;

    void OnTriggerEnter(Collider other)
    {
        Card2 enteredCard = other.GetComponentInParent<Card2>();
        

        cardScript = enteredCard;
        handValue += cardScript.value;
        Debug.Log($"Current card is now {cardScript.name}");
    }
}
    

   
