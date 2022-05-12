using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool GameActive = true;
    public bool GameOver = false;
    public bool GameWon = false;

    [SerializeField] GameObject pauseText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        GameActive = !GameActive;
        pauseText.SetActive(!pauseText.activeSelf);
    }
}
