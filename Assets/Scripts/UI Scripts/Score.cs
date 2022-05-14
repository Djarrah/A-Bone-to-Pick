using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text timeText;
    float time;
    public int Seconds { get; private set; } // ENCAPSULATION

    private void Start()
    {
        timeText = GetComponent<Text>();
    }

    private void Update()
    {
        if (GameManager.Instance.GameActive) { TimeAdvances(); }
    }

    private void TimeAdvances()
    {
        time += Time.deltaTime;
        Seconds = (int)time;
        timeText.text = $"Time: {Seconds}";
    }
}
