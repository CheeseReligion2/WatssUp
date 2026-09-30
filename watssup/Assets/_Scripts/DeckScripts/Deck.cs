using UnityEngine;

public class Deck : MonoBehaviour
{
    public static Decklist Instance { get; private set; }

    [SerializeField] private CardCollector mainDeck;
    [SerializeField] private Card cardPrefab; // prefab, which will house different CardData

    [SerializeField] private Canvas canvas; // reference to the canvas, where the cards will be instantiated                                               

    private List<Card> deckPile;
    public List<Card> HandPile { get; private set; } = new();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            
        }


    }


    private void Start()
    {
        InstantiateDeck();


    }
    private void InstantiateDeck()
    {

        Card card = Instantiate(cardPrefab, canvas.transform);

        for (int i = 0; i < mainDeck.collectedCards.Count; i++)
        {
            Card card = Instantiate(cardPrefab, canvas.transform);
            card.SetUp(mainDeck.collectedCards[i]);
            mainDeck.Add(card);
            card.gameObject.SetActive(false); // later will activate the card when drawn to hand
        }
        ShuffleDeck();


    }

    private void ShuffleDeck(){
        for (int i = mainDeck.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Card temp = mainDeck[i];
            mainDeck[i] = mainDeck[j];
            mainDeck[j] = temp;
        }

    }
    public void DrawHand(int numberOfCards = 3)
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            if (mainDeck.Count <= 0){
                ShuffleDeck();
                
            }
            HandPile.Add(mainDeck[0]); // add the first card from the main deck to the hand
            mainDeck[0].gameObject.SetActive(true); // activate the card when drawn to hand
            mainDeck.RemoveAt(0); // remove the first card from the main deck after drawing it

        }
    }



}




