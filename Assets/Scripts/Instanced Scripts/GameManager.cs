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

    [SerializeField] GameObject pauseText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject winText;

    [SerializeField] Text playerNameText;

    [SerializeField] Score scoreScript;

    public HiScoreEntry RunScore { get; private set; } // ENCAPSULATION

    private void Awake()
    {
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
        Destroy(gameObject);
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
        string playerName = playerNameText.text.ToUpper();
        int score = scoreScript.Seconds;

        RunScore = new HiScoreEntry { name = playerName, score = score };
        SceneManager.LoadScene(2);
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
