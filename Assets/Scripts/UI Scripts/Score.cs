using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text timeText;
    float time; // will be converted into seconds integers
    int seconds; // will be converted into mm:ss for the score

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        timeText = GetComponent<Text>();
    }

    private void Update()
    {
        if (gameManager.GameActive) { TimeAdvances(); }
    }

    private void TimeAdvances()
    {
        time += Time.deltaTime;
        seconds = (int)time;
        timeText.text = $"Time: {seconds}";
    }
}
