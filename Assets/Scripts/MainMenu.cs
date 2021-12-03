using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //bool firstLoading = true;
    // Start is called before the first frame update
    public void PlayGame()
    {
        /*if(firstLoading)
        {*/
        //SceneManager.UnloadScene(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        /*}
        else{
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
        }*/
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
