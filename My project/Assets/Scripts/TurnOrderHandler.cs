using System;
using UnityEngine;

public class TurnOrderHandler : MonoBehaviour
{
    public GameObject player1text;
    public GameObject player2text;
    public GameObject player3text;
    public GameObject player4text;
    public float output;
    public float playersTurn;
    public GameObject mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        output = UnityEngine.Random.Range(1,2);
        playersTurn = 2;

        StartTurn();
    }
 void DrawCards()
    {
        // Code to draw cards for the current player
    }

    // Update is called once per frame
    void StartTurn()
    {
        {
            playerCameraRotation();
            DrawCards();
        }
    }
    void playerCameraRotation()
    {
        switch (playersTurn)
        {
            case 1:
                mainCamera.transform.rotation = Quaternion.Euler(90f, 0, 0);
             
                break;
            case 2:
                mainCamera.transform.rotation = Quaternion.Euler(90f, 90f, 0);
           
                break;
            case 3:
                mainCamera.transform.rotation = Quaternion.Euler(90f, 180f, 0);
              
                break;
            case 4:
                mainCamera.transform.rotation = Quaternion.Euler(90f, 270f, 0);
            
                break;
          
    }


    
    
  }
}
