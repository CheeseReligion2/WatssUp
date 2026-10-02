using UnityEngine;

public class Calculator : MonoBehaviour
{
    [SerializeField] float P1Score;
    [SerializeField] float P2Score;
    [SerializeField] float P3Score;
    [SerializeField] float P4Score;
    public BoardScript p1Board;
    public BoardScript p2Board;
    public BoardScript p3Board;

    public BoardScript p4Board;

    float[] numbers = new float[4];
    float Min;

    public void UpdateScores()
    {
        P1Score = p1Board.totalValue;
        P2Score = p2Board.totalValue;
        P3Score = p3Board.totalValue;
        P4Score = p4Board.totalValue;
    }

    public float Calculate()
    {
        UpdateScores();

        numbers[0] = 21 - P1Score;
        numbers[1] = 21 - P2Score;
        numbers[2] = 21 - P3Score;
        numbers[3] = 21 - P4Score;
        
        for (int i =0; i < numbers.Length; i++)
        {
            Min = numbers[0];
            if(numbers[i+1] <  Min)
            {
                Min = numbers[i+1];
            }
        }
        return Min;
    }
}
