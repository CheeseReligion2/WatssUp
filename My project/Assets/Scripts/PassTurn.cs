using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PassTurn : MonoBehaviour
{
    [SerializeField] GameObject player1Board;
    [SerializeField] GameObject player2Board;

    [SerializeField] GameObject player3Board;
    [SerializeField] GameObject player4Board;
    public int whosTurn; 

    void Start()
    {
        whosTurn = (Random.Range(1, 4));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PassTurnFunction()
    {
        Debug.Log("Pass Turn Function Called");
      player1Board.transform.position = new Vector3(-10, 0, 0);
    }
}
