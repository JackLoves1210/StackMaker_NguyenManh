using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] Text textGold;
    [SerializeField] Text textLevel;
    [SerializeField] static int goldCollect = 0;
    [SerializeField] public GameObject pnlEndGame;
    [SerializeField] public GameObject pnlNextLevel;
    [SerializeField] private GameManagerment gameManagerment;
    static int numberScene = 1;
    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        gameManagerment = gameObject.GetComponent<GameManagerment>();
        
        GetLevel();
        pnlEndGame.SetActive(false);
        pnlNextLevel.SetActive(false);
        Time.timeScale = 1;
        
        getGold(0);
        //textCOins = 0;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Reset");
    }

    public void GetLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex + 1;
        textLevel.text = level.ToString();
    }
    public void getGold(int goldCollected)
    {
        goldCollect+=goldCollected;
        textGold.text    = goldCollect.ToString();
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void NextLevel(string nameScene)
    {
        GetLevel();
        gameManagerment.GoToScene(nameScene);
    }

    public void EndGame()
    {
        pnlEndGame.SetActive(true);
        Time.timeScale = 0;
    }

     public void PnlNextLevel()
    {
        pnlNextLevel.SetActive(true);
        Time.timeScale = 0;
    }
}
