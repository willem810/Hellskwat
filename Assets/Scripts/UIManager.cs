using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject ingameUI;
    public GameObject GameOverUI;
    public GameObject menuUI;
    public Image livesUI;
    public Text bullets;
    public Text score;
    public Text GameoverText;

    public Bounds defaultBounds;

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        //defaultBounds = livesUI.bounds;
        ingameUI.SetActive(true);
        GameOverUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver()
    {
        ingameUI.SetActive(false);
        GameOverUI.SetActive(true);
        GameoverText.text = "Score: " + Gamemanager.instance.player.score;
    }

    public void updateLives()
    {

        livesUI.sprite = Gamemanager.instance.player.getLivesSprite();
        //resize();
    }

    public void updateScore()
    {
        score.text = "Score: " + Gamemanager.instance.player.score;
        //resize();
    }

    public void showMenu(bool show)
    {
        menuUI.SetActive(show);
    }

    public void closeGame()
    {
        Debug.Log("Closing game..");
        Application.Quit();
    }

    public void UpdateBullets(int totalBullets)
    {
        bullets.text = "x " + totalBullets;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }

    public void Restart()
    {
        SceneManager.LoadScene("start");
    }

    //void resize()
    //{
    //    //stel originele bounds zijn 1 x 1
    //    // nieuwe bounds zijn 3x4
    //    // 3 * (1/3) 

    //    float xDif = defaultBounds.size.x / livesUI.sprite.bounds.size.x;
    //    float yDif = defaultBounds.size.y / livesUI.sprite.bounds.size.y;

    //    livesUI.transform.localScale = new Vector3(xDif, yDif, livesUI.transform.localScale.z);
    //}


}
