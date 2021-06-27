using TMPro;
using UnityEngine;

[RequireComponent(typeof(Pausable))]
public class CastleGate : MonoBehaviour
{
    public TextMeshPro txt;
    public float[] secondsToPeople;
    public AudioClip[] clips;
    public int people = 0;
    
    private AudioSource _audio;
    private Pausable _pause;
    private float _secondsPassed = 0.0f;
    private bool _introPlayed = false;

    void Start()
    {
        _pause = GetComponent<Pausable>();
        _audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_pause.Paused)
        {
            return;
        }

        if (!_introPlayed)
        {
            _introPlayed = true;
            people = 0;
            _audio.clip = clips[people];
            _audio.Play();
            return;
        }
        
        _secondsPassed += Time.deltaTime;

        var lastIndex = people;
        for (int i = 0; i < secondsToPeople.Length; i++)
        {
            if (_secondsPassed < secondsToPeople[i])
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
            FindObjectOfType<MenuController>().EndGame();
            // TODO: Lose!!!
            Debug.Log("You lose!");
        }

        txt.text = people + "";
    }
}