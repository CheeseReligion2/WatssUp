using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.InputSystem;

public class HandManagerScript : MonoBehaviour
{
    [SerializeField] private int maxHandSize;

    [SerializeField] private SplineContainer splineContainer;

    [SerializeField] private Deck deck;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Transform playerHandOne;


    private List<GameObject> handCards = new List<GameObject>();

    private Dictionary<GameObject, GameObject> cardPrefabs =
    new Dictionary<GameObject, GameObject>();

    private void Update()
    {

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Draw();
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ReturnLastDrawnCard();
        }

            
    }

    private void Draw()
    {
        if (handCards.Count >= maxHandSize) return;

        GameObject cardPrefab = deck.DrawCard(); // makes the specific card drawn the top card of the deck, defined in the deck script

        GameObject newCard = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity, playerHandOne); // spawns the card drawn at the spawn point, with no rotation, and as a child of the player hand
        handCards.Add(newCard);
        cardPrefabs.Add(newCard, cardPrefab); // remebers each card in hand so they can be put back into the deck 

        UpdateCardPositions();
    }


    private void UpdateCardPositions()
    {
        if (handCards.Count == 0)
            return;

        float cardSpacing = 0.3f / maxHandSize;

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

    private void hoverCard(GameObject card)
    {

    }
    



}


    





