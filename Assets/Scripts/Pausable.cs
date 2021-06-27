using UnityEngine;

public class Pausable : MonoBehaviour
{
    public bool Paused => _pauseManager.Paused;

    private PauseManager _pauseManager; 
    
    void Start()
    {
        _pauseManager = FindObjectOfType<PauseManager>();
    }

    void Update()
    {
    }
}
