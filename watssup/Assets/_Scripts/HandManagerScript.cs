using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.InputSystem;

public class HandManagerScript : MonoBehaviour
{
    [SerializeField] private int maxHandSize; 

    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private SplineContainer splineContainer;

    [SerializeField] private Transform spawnPoint;

    private List<GameObject> handCards = new List<GameObject>();

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Draw();
        }
    }

    private void Draw()
    {
        if (handCards.Count >= maxHandSize) return;

        GameObject newCard = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity);
        handCards.Add(newCard);
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
            rotation *= Quaternion.Euler(0f, 90f, 90f);

            
            handCards[i].transform.DOMove(worldPosition, 0.25f);
            handCards[i].transform.DORotateQuaternion(rotation, 0.25f);
        }
    }


    





}