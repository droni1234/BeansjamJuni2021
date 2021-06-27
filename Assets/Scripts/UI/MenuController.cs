using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Canvas menuCanvas;
    public bool menuVisible = true;
    public bool gameInProgress = false;
    public PauseManager pauseManager;
    
    void Start()
    {
        SceneManager.sceneUnloaded += ReloadScene;
        ReloadScene(SceneManager.GetActiveScene());
    }

    void Update()
    {
        if (gameInProgress && Input.GetKeyDown(KeyCode.Escape))
        {
            menuVisible = !menuVisible;

            if (menuVisible)
            {
                PauseAll();
            }
            else
            {
                UnPauseAll();
            }
        }

        if (menuVisible != menuCanvas.gameObject.activeSelf)
        {
            menuCanvas.gameObject.SetActive(menuVisible);
        }
    }

    public void StartGame()
    {
        if (gameInProgress)
        {
            EndGame();
        }
        
        menuVisible = false;
        gameInProgress = true;
        UnPauseAll();
    }

    public void EndGame()
    {
        menuVisible = true;
        gameInProgress = false;
        SceneManager.UnloadSceneAsync(1);
    }

    public void ReloadScene(Scene current)
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void UnPauseAll()
    {
        pauseManager.UnPause();
    }
    
    public void PauseAll()
    {
        pauseManager.Pause();
    }
}