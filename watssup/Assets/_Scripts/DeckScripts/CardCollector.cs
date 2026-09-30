using UnityEngine;

public class CardCollector : ScriptableObject
{

[SerializeField] public List<ScriptableCard> collectedCards { get; private set; }


// allows multiple cards in deck.
public void AddCard(ScriptableCard card)
{   
    collectedCards.Add(card);

}
}

