using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public bool GameActive = true;
    [HideInInspector] public bool GameWon = false;
    [HideInInspector] public bool GameLost = false;

    string playerName;
    int score;

    [SerializeField] GameObject pauseText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject winText;
    [SerializeField] Text playerNameText;
    [SerializeField] Score scoreScript;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        if (GameLost || GameWon) { return; }
        
        GameActive = !GameActive;
        pauseText.SetActive(!pauseText.activeSelf);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        GameActive = false;
        GameLost = true;

        gameOverText.SetActive(true);
    }

    public void WinGame()
    {
        GameActive = false;
        GameWon = true;

        winText.SetActive(true);
    }

    public void SubmitScore()
    {
        // maybe they're better as local var?
        playerName = playerNameText.text;
        score = scoreScript.Seconds;
    }
}
