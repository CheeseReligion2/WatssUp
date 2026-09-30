using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class River : MonoBehaviour
{
 
    public Card2 cardScript;
    public float riverValue;
    public bool jackOnBoard;
    public TextMeshProUGUI RiverValueText;
    
    private readonly HashSet<Collider> objectsInsideTrigger = new HashSet<Collider>();
    private readonly HashSet<Card2> jackAffectedCards = new HashSet<Card2>();
    void OnTriggerEnter(Collider other)
    {
        objectsInsideTrigger.Add(other);
        jackOnBoard = HasTagInsideTrigger("Jack");

        if (other.gameObject.CompareTag("Card"))
        {
            Card2 enteredCard = other.GetComponentInParent<Card2>();
            {
                cardScript = enteredCard;
                if (jackOnBoard)
                    jackAffectedCards.Add(enteredCard);
            }
        }

        RecalculateHandValue();
    }

    void OnTriggerExit(Collider other)
    {
        objectsInsideTrigger.Remove(other);
        jackOnBoard = HasTagInsideTrigger("Jack");

        HashSet<Card2> cardsStillInside = GetCardsInsideTrigger();
        jackAffectedCards.RemoveWhere(card => !cardsStillInside.Contains(card));

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

            if (jackAffectedCards.Contains(card))
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
            Card2 card = insideCollider.GetComponentInParent<Card2>();
                cards.Add(card);
        }

        return cards;
    }

    void Update()
    {
        RiverValueText.text = "River Value: " + riverValue.ToString();
    }
}

   
    


