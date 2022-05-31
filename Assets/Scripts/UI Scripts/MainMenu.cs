using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject optionsScreen;
    [SerializeField] GameObject controlsScreen;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Loads the game proper
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    // loads the global leaderboard
    public void ShowLeaderBoard()
    {
        SceneManager.LoadScene(2);
    }

    // swaps screens with the options
    public void ShowOptions()
    {
        titleScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }

    // return back to the title screen
    public void BackToTitle()
    {
        titleScreen.SetActive(true);
        optionsScreen.SetActive(false);
    }

    public void ShowControls()
    {
        titleScreen.SetActive(false);
        controlsScreen.SetActive(true);
    }

    public void BackToTitleFromControls()
    {
        titleScreen.SetActive(true);
        controlsScreen.SetActive(false);
    }
}
