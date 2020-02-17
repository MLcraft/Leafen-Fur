using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level_1");
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("01_Menu");
        Time.timeScale = 1;
    }
    public void LoadHelp()
    {
        SceneManager.LoadScene("Help");
        Time.timeScale = 1;
    }
    public void ShowPaused()
    {
        if (!SceneManager.GetSceneByName("Pause_Menu").IsValid())
        {
            SceneManager.LoadScene("Pause_Menu", LoadSceneMode.Additive);
            Debug.Log("PAUSE");
            Time.timeScale = 0;
        }
    }
    public void HidePaused()
    {
        SceneManager.UnloadSceneAsync("Pause_Menu");
        Time.timeScale = 1;
    }
    public void GoodEnd()
    {
        SceneManager.UnloadSceneAsync("Choice");
        Time.timeScale = 1;
    }
    public void BadEnd()
    {
        string fox = GameObject.Find("GeneratorCollider").GetComponent<GeneratorController>().fox;
        if (fox == "Pack")
        {
            SceneManager.LoadScene("Bad_Ending");
            Time.timeScale = 1;
        }
        else
        {
            SceneManager.LoadScene("Bad_Ending_Pack");
            Time.timeScale = 1;
        }
    }
}
