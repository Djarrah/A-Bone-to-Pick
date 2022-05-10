using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] Text timeText;
    float time; // will be converted into mm:ss for the score

    [HideInInspector] public bool Cutscenes = true;
    public bool GameActive = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (GameActive) { TimeAdvances(); }
    }

    private void TimeAdvances()
    {
        time += Time.deltaTime;
        timeText.text = $"Time: {time}";
    }
}
