using UnityEngine;
using System.Collections.Generic;
using TMPro;
//this script is a copy of BoardP1.cs, it is the exact same script, i am lazy so wont rewrite the comments, but it is for player 3, so all the p1 references are now p3 references
public class BoardP3 : MonoBehaviour
{
    public Card2 cardScript;
    public River riverScript;
    public float p3HandValue;

    public bool jackOnBoard;

    public TextMeshProUGUI P3HandValueText;
    private readonly HashSet<Collider> objectsInsideTrigger = new HashSet<Collider>();
    private bool lastRiverQueenState;


    void OnTriggerEnter(Collider other)
    {
        objectsInsideTrigger.Add(other);
        jackOnBoard = HasTagInsideTrigger("Jack");

        if (other.gameObject.CompareTag("Card"))
        {
            Card2 enteredCard = other.GetComponentInParent<Card2>();
            if (enteredCard != null)
                cardScript = enteredCard;
                cardScript.isOnBoard = true;
                
        }

        RecalculateHandValue();
    }

    void OnTriggerExit(Collider other)
    {
        objectsInsideTrigger.Remove(other);
        jackOnBoard = HasTagInsideTrigger("Jack");
        cardScript.isOnBoard = false;
        cardScript = null;

        RecalculateHandValue();
    }

    private void RecalculateHandValue()
    {
        bool queenOnRiver = riverScript != null && riverScript.QueenOnRiver;
        HashSet<Card2> cardsInside = GetCardsInsideTrigger();
        p3HandValue = 0f;

        foreach (Card2 card in cardsInside)
        {
            float cardValue = card.value;

            if (jackOnBoard)
                cardValue *= -1f;

            if (queenOnRiver)
                cardValue *= 2f;

            p3HandValue += cardValue;
            Debug.Log($"Current card is {card.name}, effective value: {cardValue}");
            P3HandValueText.text = "P3 Hand Value: " + p3HandValue.ToString();
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
        bool queenOnRiver = riverScript != null && riverScript.QueenOnRiver;
        if (queenOnRiver != lastRiverQueenState)
        {
            lastRiverQueenState = queenOnRiver;
            RecalculateHandValue();
        }

        if (riverScript != null && P3HandValueText != null)
        {
            float totalValue = p3HandValue + riverScript.riverValue;
            if (HasTagInsideTrigger("Queen"))
                totalValue *= 2f;

            P3HandValueText.text = "P3 Hand Value: " + totalValue;
        }
    }
}



