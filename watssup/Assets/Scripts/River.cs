using UnityEngine;
using TMPro;
using System.Collections.Generic;
//this script is a copy of BoardP1.cs, but it does not need to check for a queen on the river, or for other players, so it is a bit simpler, but it still has the same basic functionality of checking for cards and jacks on the river and calculating the total value of the cards on the river
public class River : MonoBehaviour
{
 
    public Card2 cardScript;
    public float riverValue;
    public bool jackOnBoard;
    public TextMeshProUGUI RiverValueText;
    
    private readonly HashSet<Collider> objectsInsideTrigger = new HashSet<Collider>();

    public bool QueenOnRiver => HasTagInsideTrigger("Queen");

    void OnTriggerEnter(Collider other)
    {
        objectsInsideTrigger.Add(other);
        jackOnBoard = HasTagInsideTrigger("Jack");

        if (other.gameObject.CompareTag("Card"))
        {
            Card2 enteredCard = other.GetComponentInParent<Card2>();
            if (enteredCard != null)
                cardScript = enteredCard;
        }

        RecalculateHandValue();
    }

    void OnTriggerExit(Collider other)
    {
        objectsInsideTrigger.Remove(other);
        jackOnBoard = HasTagInsideTrigger("Jack");

        RecalculateHandValue();
    }

    private void RecalculateHandValue()
    {
        bool queenOnBoard = HasTagInsideTrigger("Queen");
        HashSet<Card2> cardsInside = GetCardsInsideTrigger();
        riverValue = 0f;

        foreach (Card2 card in cardsInside)
        {
            float cardValue = card.value;

            if (jackOnBoard)
                cardValue *= -1f;

            if (queenOnBoard)
                cardValue *= 2f;

            riverValue += cardValue;
            Debug.Log($"Current card is {card.name}, effective value: {cardValue}");
            RiverValueText.text = "River Value: " + riverValue.ToString();
        }
    }

    private bool HasTagInsideTrigger(string tag)
    {
        foreach (Collider insideCollider in objectsInsideTrigger)
        {
            if (insideCollider != null && insideCollider.gameObject.CompareTag(tag))
                return true;
        }

        return false;
    }

    private HashSet<Card2> GetCardsInsideTrigger()
    {
        HashSet<Card2> cards = new HashSet<Card2>();

        foreach (Collider insideCollider in objectsInsideTrigger)
        {
            if (insideCollider == null || !insideCollider.gameObject.CompareTag("Card"))
                continue;

            Card2 card = insideCollider.GetComponentInParent<Card2>();
            if (card != null)
                cards.Add(card);
        }

        return cards;
    }

    void Update()
    {
        RiverValueText.text = "River Value: " + riverValue.ToString();
    }
}

   
    


