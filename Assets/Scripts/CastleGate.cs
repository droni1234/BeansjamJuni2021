using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CastleGate : MonoBehaviour
{
    public TextMeshPro txt;
    public float[] secondsToPeople;
    public AudioClip[] clips;
    public int people = 0;
    private float secondsPassed = 0.0f;
    private AudioSource _audio;

    void Start()
    {
        people = 0;
        _audio = GetComponent<AudioSource>();
        _audio.clip = clips[people];
        _audio.Play();
    }

    void Update()
    {
        secondsPassed += Time.deltaTime;

        var lastIndex = people;
        for (int i = 0; i < secondsToPeople.Length; i++)
        {
            if (secondsPassed < secondsToPeople[i])
            {
                lastIndex = i;
                break;
            }
        }

        if (people != lastIndex)
        {
            people = lastIndex;
            
            if (clips[people])
            {
                _audio.clip = clips[people];
                _audio.Play();
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