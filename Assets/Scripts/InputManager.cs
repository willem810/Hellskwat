using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public float mouseXSpeedMod;
    public float mouseYSpeedMod;
    public static InputManager instance;

    public bool PauseGame { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
	
	// Update is called once per frame
	void Update () {
        checkInput();	
	}

    void checkInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Gamemanager.instance.player.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Gamemanager.instance.player.Reload();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePauseGame();
        }
    }

    void togglePauseGame()
    {
        if (PauseGame) ContinueGame();
        else PauzeGame();
      
        PauseGame = !PauseGame;
    }

    public void PauzeGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        UIManager.instance.showMenu(true);
    }

    public void ContinueGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UIManager.instance.showMenu(false);
    }
}
