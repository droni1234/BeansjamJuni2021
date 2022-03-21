using UnityEngine;

public class Pausable : MonoBehaviour
{
    public bool Paused => _pauseManager != null && _pauseManager.Paused;

    private PauseManager _pauseManager;

    private void Start()
    {
        _pauseManager = FindObjectOfType<PauseManager>();
    }
}
