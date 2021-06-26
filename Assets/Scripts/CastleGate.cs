using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CastleGate : MonoBehaviour
{
    public TextMeshPro txt;
    public float[] secondsToPeople;
    public int people = 0;
    private float secondsPassed = 0.0f;
    
    void Start()
    {
        
    }

    void Update()
    {
        secondsPassed += Time.deltaTime;

        for (int i = 0; i < secondsToPeople.Length; i++)
        {
            if (secondsPassed < secondsToPeople[i])
            {
                people = i;
                break;
            }
        }

        if (people == 10)
        {
            // TODO: Lose!!!
            Debug.Log("You lose!");
        }

        txt.text = people + "";
    }
}