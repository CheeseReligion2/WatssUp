using UnityEngine;

public class Card : MonoBehaviour
{
    [serializeField] private ScriptableCard cardData { get; private set; }

    public void SetUp(scriptableCard data)
    {
        cardData = data;
    }


}
