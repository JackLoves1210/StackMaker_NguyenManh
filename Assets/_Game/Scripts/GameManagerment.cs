using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerment : MonoBehaviour
{
    private int currentIndex = 0;
    //string nextSceneName = "Level 1";


    public void GoToScene(string nameScene)
    {
        //currentIndex = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log("currentIndex:" + currentIndex);
        //currentIndex++;
        //Debug.Log("Ount:" + SceneManager.sceneCount);
        //nextSceneName = SceneManager.GetSceneAt(currentIndex).name;
        //Debug.Log("nextSceneName:" + nextSceneName);
        SceneManager.LoadScene(nameScene);
    }
    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Quit Compeleted!");
    }

}
