using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class BoardP1 : MonoBehaviour
{
    public Card2 cardScript;
    public River riverScript;
    public float p1HandValue;

    public bool jackOnBoard;

    public TextMeshProUGUI P1HandValueText;
    private readonly HashSet<Collider> objectsInsideTrigger = new HashSet<Collider>();
    private bool lastRiverQueenState;


    void OnTriggerEnter(Collider other)
    {
        // Add the collider to the HashSet when it enters the trigger
        objectsInsideTrigger.Add(other);
        jackOnBoard = HasTagInsideTrigger("Jack");

        if (other.gameObject.CompareTag("Card"))
        {
            // Get the Card2 component from the parent of the collider
            Card2 enteredCard = other.GetComponentInParent<Card2>();
            if (enteredCard != null)
                cardScript = enteredCard;
                cardScript.isOnBoard = true; // Set the isOnBoard property to true when the card enters the trigger
        }

        RecalculateHandValue();
    }

    void OnTriggerExit(Collider other)
    {
     Card2 exitedCard = other.GetComponentInParent<Card2>();   
        objectsInsideTrigger.Remove(other);
        jackOnBoard = HasTagInsideTrigger("Jack");
        exitedCard.isOnBoard = false; // Set the isOnBoard property to false when the card exits the trigger
        cardScript = null;

        RecalculateHandValue();
    }

    private void RecalculateHandValue()
    {
        // check if queen is on the river
        bool queenOnRiver = riverScript.QueenOnRiver;
        HashSet<Card2> cardsInside = GetCardsInsideTrigger();
        p1HandValue = 0f; // if im honest, im not sure what this line does but if i remove it funky stuff happens so im leaving it in

        foreach (Card2 card in cardsInside)
        {
            float cardValue = card.value; // Get the value of the card

            if (jackOnBoard)
                cardValue *= -1f; // flip the cards value if it is effected by a jack 

            if (queenOnRiver)
                cardValue *= 2f; // double the cards value if it is effected by a queen on the river

            p1HandValue += cardValue; // Add the card's effective value to the total hand value
            Debug.Log($"Current card is {card.name}, effective value: {cardValue}");
            P1HandValueText.text = "P1 Hand Value: " + p1HandValue.ToString(); // Update the UI text with the current hand value 
        }
    }

    private bool HasTagInsideTrigger(string tag)
    {
        foreach (Collider insideCollider in objectsInsideTrigger) // Run through each collider in the HashSet and check if it has the specified tag
        {
            if (insideCollider != null && insideCollider.gameObject.CompareTag(tag))
                return true;
        }

        return false;
    }

    private HashSet<Card2> GetCardsInsideTrigger()
    {
        HashSet<Card2> cards = new HashSet<Card2>(); // same as above but instead of checking for a tag, it checks for the Card2 component and adds it to a new HashSet of Card2 objects

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
        bool queenOnRiver = riverScript != null && riverScript.QueenOnRiver; // Check if the queen is on the river, ideally this wouldn't be done every frame but it is what it is
        if (queenOnRiver != lastRiverQueenState)
        {
            lastRiverQueenState = queenOnRiver;
            RecalculateHandValue();
        }

       
        {
            float totalValue = p1HandValue + riverScript.riverValue;
            if (HasTagInsideTrigger("Queen"))
                totalValue *= 2f;

            P1HandValueText.text = "P1 Hand Value: " + totalValue;
        }
    }
}



