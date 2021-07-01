using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Canvas menuCanvas;
    public bool menuVisible = true;
    public bool gameInProgress = false;
    public PauseManager pauseManager;
    public int gameScene = 2;
    public bool endScreen = false;
    
    void Awake()
    {
        //SceneManager.sceneUnloaded += ReloadScene;
        ReloadScene(SceneManager.GetActiveScene());
#if UNITY_WEBGL
        Destroy(GameObject.Find("Exit"));
#endif
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

        if(endScreen && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void StartGame()
    {
        if (gameInProgress)
        {
            EndGame();
            SceneManager.LoadScene(gameScene, LoadSceneMode.Additive);
        }

        
        menuVisible = false;
        gameInProgress = true;
        UnPauseAll();
    }

    public void EndGame()
    {
        pauseManager.Pause();
        menuVisible = true;
        gameInProgress = false;
        SceneManager.UnloadSceneAsync(gameScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
    }

    public void ReloadScene(Scene current)
    {
        SceneManager.LoadScene(gameScene, LoadSceneMode.Additive);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Victory()
    {
        //EndGame();
        SceneManager.UnloadSceneAsync(2);
        SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
        endScreen = true;
        menuVisible = false;
    }

    public void Defeat()
    {
        //EndGame();
        SceneManager.UnloadSceneAsync(2);
        SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
        endScreen = true;
        menuVisible = false;
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
