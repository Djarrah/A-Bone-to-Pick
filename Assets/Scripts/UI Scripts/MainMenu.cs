using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Title Screen")]
    [SerializeField] GameObject titleScreen;
    [SerializeField] Button startButton;
    [SerializeField] Button leaderboardButton;
    [SerializeField] Button optionsButton;

    [Header("Options Screen")]
    [SerializeField] GameObject optionsScreen;
    [SerializeField] Button titleButton;

    // Loads the game proper
    public void StartGame()
    {
        // Until the arena is finished, we'll use the test scene instead
        SceneManager.LoadScene(3); // Swap to 1 when arena is finished
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
}
