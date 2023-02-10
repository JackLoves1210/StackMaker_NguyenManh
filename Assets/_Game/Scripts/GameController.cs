using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] public bool isStartGame;
    [SerializeField] public GameObject pnlEndGame;
    [SerializeField] private Text textCOins;
    [SerializeField] private GameManagerment gameManagerment;
    static int numberScene = 1;
    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        gameManagerment = gameObject.GetComponent<GameManagerment>();
        
        isStartGame = false;
        Time.timeScale = 1;
        //textCOins = 0;
    }

    public void GetCoin()
    {

    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Reset");
    }

    
    public void NextLevel(string nameScene)
    {
        numberScene++;
        nameScene = "Level " + numberScene;
        gameManagerment.GoToScene(nameScene);
    }

}
