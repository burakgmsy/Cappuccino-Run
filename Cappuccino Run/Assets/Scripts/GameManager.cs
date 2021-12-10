using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [Header("UI Managers")]
    public GameObject mainScreen;
    public GameObject winScreen;
    //public GameObject inGameScreen;
    public GameObject loseScreen;
    public float speed = 0;



    public void StartGame()
    {
        mainScreen.SetActive(false);
        //inGameScreen.SetActive(true);
        InputHandler.Instance.isGaming = true;
        speed = 4F;
    }
    public void LoseGame()
    {
        loseScreen.SetActive(true);
        InputHandler.Instance.isGaming = false;
        speed = 0;
    }
    public void WinGame()
    {
        winScreen.SetActive(true);
        InputHandler.Instance.isGaming = false;
    }

    public void LoadScreen()
    {
        SceneManager.LoadScene("GameScene");
    }


}
