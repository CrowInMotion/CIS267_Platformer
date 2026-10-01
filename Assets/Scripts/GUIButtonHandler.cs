using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GUIButtonHandler : MonoBehaviour
{
    public GameObject menu;
    private bool sceneLoaded = false;
    private bool gamePaused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        showPauseMenu();
    }

    public void loadGame()
    {
        //need a way to ensure that this menu does not get destroyed between scene loads
        DontDestroyOnLoad(gameObject);
        //set bool to say the scene has been loaded
        sceneLoaded = true;
        menu.SetActive(false);
        //Load level one
        SceneManager.LoadScene("SampleScene");
    }
    public void exitGame()
    {
        Application.Quit();
        Debug.Log("Exit Application..");
    }

    public void showPauseMenu()
    {
        //look and see if the user paused the game
        if(Input.GetKeyDown(KeyCode.P) && sceneLoaded)
        {
            if(!gamePaused)
            {

                menu.SetActive(true);
                //actually pause the game
                Time.timeScale = 0;
                gamePaused = true;
            }
            else
            {
                menu.SetActive(false);
                Time.timeScale = 1;
                gamePaused = false;
            }
        }
    }
}
