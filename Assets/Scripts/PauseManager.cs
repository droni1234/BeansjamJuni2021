using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public bool Paused = true;
    
    void Start()
    {
    }

    void Update()
    {
    }
    
    
    public void Pause()
    {
        Paused = true;
    }

    public void UnPause()
    {
        Paused = false;
    }
}
