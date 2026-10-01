using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.InputSystem;


[System.Serializable]
public class DeckEntry
{
    public GameObject cardPrefab;
    public int copies = 1;
}

public class Deck : MonoBehaviour
{
    [SerializeField] private List<DeckEntry> deckList;

    private List<GameObject> drawPile = new List <GameObject>();


    private void Start()
    {
        DeckConstruct();
        ShuffleDeck();
    }

    private void Update()
    {
       
    }

    private void DeckConstruct()
    {
        drawPile.Clear();

        foreach (DeckEntry entry in deckList)
        {
            for (int i = 0; i < entry.copies; i++)
            {
                drawPile.Add(entry.cardPrefab); //adds the specific card wanted equal to the listed amount of times into the draw pile
            }
        }
        Debug.Log("Cards in deck: " + drawPile.Count);
    }

    private void ShuffleDeck()
    {
        for (int i = drawPile.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);

            GameObject temp = drawPile[i];
            drawPile[i] = drawPile[j];
            drawPile[j] = temp;
        }
    }

    public GameObject DrawCard()
    {
        int topCardIndex = drawPile.Count - 1; // the card wanted to draw becomes whatever the count is minus one (minus cus things start counting from 0)
        GameObject card = drawPile[topCardIndex]; // identifies and the thing and calls it a card
        drawPile.RemoveAt(topCardIndex); // removes the card from teh deck

        Debug.Log("Drew: " + card.name);
        Debug.Log("Cards remaining: " + drawPile.Count);

        return card; // gives the thing wahtever asked for the card :3


       
    }

    public void ReturnCardToDeck(GameObject cardPrefab)
    {
        drawPile.Add(cardPrefab);
        ShuffleDeck();

        Debug.Log("Returned: " + cardPrefab.name);
        Debug.Log("Cards in deck: " + drawPile.Count);
    }

}



