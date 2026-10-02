using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.InputSystem;

public class HandManagerScript : MonoBehaviour
{
    [SerializeField] private int maxHandSize;

    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private SplineContainer riverSplineContainer;

    [SerializeField] private Deck deck;

    [SerializeField] private Transform targetParent;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spaceB = 0.3f;

    [SerializeField] public List<GameObject> handCards = new List<GameObject>();
    [SerializeField] private List<GameObject> riverCards = new List<GameObject>();

    private Dictionary<GameObject, GameObject> cardPrefabs =
    new Dictionary<GameObject, GameObject>();


    private void Start()
    {
        
    
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ReturnLastDrawnCard();
        }
    }

    public void Draw()
    {
        if (handCards.Count >= maxHandSize) return;

        GameObject cardPrefab = deck.DrawCard(); // makes the specific card drawn the top card of the deck, defined in the deck script

        GameObject newCard = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity, targetParent);
        handCards.Add(newCard);
        cardPrefabs.Add(newCard, cardPrefab); // remebers each card in hand so they can be put back into the deck 

        UpdateCardPositions();
        newCard.GetComponent<CardGrabber>().handManagerScript = this; // sets the hand manager script to the card grabber script so it can access the return card function
    }

    public void DrawToRiver()
    {

        GameObject cardPrefab = deck.DrawCard(); // makes the specific card drawn the top card of the deck, defined in the deck script

        GameObject newCard = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity);
        riverCards.Add(newCard);
        cardPrefabs.Add(newCard, cardPrefab); // remebers each card in hand so they can be put back into the deck 
       // Vector3 riverPosition = new Vector3(8f, 0.55f, -3.75f); // Replace with the actual position of the river
       // newCard.transform.DOMove(riverPosition, 0.5f).SetEase(Ease.InOutSine); // Animate the card to the river position
       // newCard.transform.DORotateQuaternion(Quaternion.Euler(0f, 0f, 0f), 0.5f).SetEase(Ease.InOutSine); // Animate the card rotation
        UpdateRiverCardPositions();
        
    }
     public void UpdateRiverCardPositions()
    {
        if (riverCards.Count == 0)
            return;

        float cardSpacing = spaceB / maxHandSize;

        float firstCardPosition =
            0.5f - (riverCards.Count - 1) * cardSpacing / 2f;

        Spline spline = riverSplineContainer.Spline;

        for (int i = 0; i < riverCards.Count; i++)
        {
            float t = firstCardPosition + i * cardSpacing;

            Vector3 localPosition = spline.EvaluatePosition(t);
            Vector3 localTangent = spline.EvaluateTangent(t);
            Vector3 localUp = spline.EvaluateUpVector(t);

            Vector3 worldPosition = riverSplineContainer.transform.TransformPoint(localPosition);

            Vector3 worldTangent = riverSplineContainer.transform.TransformDirection(localTangent);

            Vector3 worldUp = riverSplineContainer.transform.TransformDirection(localUp);

            Quaternion rotation = Quaternion.LookRotation(worldUp, -worldTangent);
            rotation *= Quaternion.Euler(0f, 90f, 90f);// flips so correct
            rotation *= Quaternion.Euler(0f, 180f, 0f);

            
            riverCards[i].transform.DOMove(worldPosition, 0.25f);
            riverCards[i].transform.DORotateQuaternion(rotation, 0.25f);


        }
    }


    public void UpdateCardPositions()
    {
        if (handCards.Count == 0)
            return;

        float cardSpacing = spaceB / maxHandSize;

        float firstCardPosition =
            0.5f - (handCards.Count - 1) * cardSpacing / 2f;

        Spline spline = splineContainer.Spline;

        for (int i = 0; i < handCards.Count; i++)
        {
            float t = firstCardPosition + i * cardSpacing;

            Vector3 localPosition = spline.EvaluatePosition(t);
            Vector3 localTangent = spline.EvaluateTangent(t);
            Vector3 localUp = spline.EvaluateUpVector(t);

            Vector3 worldPosition = splineContainer.transform.TransformPoint(localPosition);

            Vector3 worldTangent = splineContainer.transform.TransformDirection(localTangent);

            Vector3 worldUp = splineContainer.transform.TransformDirection(localUp);


            Quaternion rotation = Quaternion.LookRotation(worldUp, -worldTangent);
            rotation *= Quaternion.Euler(0f, 90f, 90f);// flips so correct
            rotation *= Quaternion.Euler(0f, 180f, 0f);



            handCards[i].transform.DOMove(worldPosition, 0.25f);
            handCards[i].transform.DORotateQuaternion(rotation, 0.25f);
        }



    }

    public void ReturnLastDrawnCard()
    {
        if (handCards.Count > 0)
        {
            GameObject lastCard = handCards[handCards.Count - 1];
            ReturnCard(lastCard);
        }
    }


    public void ReturnCard(GameObject card)
    {
        if (!cardPrefabs.ContainsKey(card))
            return;

        GameObject originalPrefab = cardPrefabs[card];

        // Remove it from the hand immediately
        handCards.Remove(card);
        cardPrefabs.Remove(card);

        // Stop any hand-position tween currently affecting this card
        card.transform.DOKill();

        
        UpdateCardPositions();

        // Animate this card back to the draw pile
        Sequence returnSequence = DOTween.Sequence();

        returnSequence.Append(
            card.transform.DOMove(spawnPoint.position, 0.35f)
        );

        returnSequence.Join(
            card.transform.DORotateQuaternion(spawnPoint.rotation, 0.35f)
        );

        // Only actually return/destroy it once the animation finishes
        returnSequence.OnComplete(() =>
        {
            deck.ReturnCardToDeck(originalPrefab);
            Destroy(card);
        });
    }



}


    





