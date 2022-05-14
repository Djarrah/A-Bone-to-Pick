using System;

[Serializable]
public struct HiScoreEntry
{
    public int score;
    public string name;

    public string FormatTime()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(score);
        return timeSpan.ToString(@"mm\:ss");
    }
}
