using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Thanks to Code Monkey

public class HiScoreTable : MonoBehaviour
{
    [SerializeField] Transform entryTemplate;
    [SerializeField] Transform entryContainer;
    [SerializeField] float templateHeight = 20f;

    List<HiScoreEntry> hiScoreEntryList;
    List<Transform> transformList;

    GameManager gameManager;
    
    void Awake()
    {
        gameManager = GameManager.Instance;
        
        LoadData();

        if (gameManager != null)
        {
            hiScoreEntryList.Add(gameManager.RunScore);
            gameManager.SelfDestruct();
        }

        hiScoreEntryList = hiScoreEntryList.OrderBy(e => e.score).ToList();
        TrimList();

        SaveData();

        transformList = new List<Transform>();
        DrawTable();
    }

    void TrimList()
    {
        if (hiScoreEntryList.Count > 10)
        {
            hiScoreEntryList.RemoveAt(hiScoreEntryList.Count - 1);
            TrimList();
        }
    }

    void DrawTable()
    {
        transformList.Clear();
        
        foreach (HiScoreEntry entry in hiScoreEntryList)
        {
            CreateHiScoreEntryTransform(entry, entryContainer, transformList);
        }
    }

    void CreateHiScoreEntryTransform(HiScoreEntry entry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform rectTransform = entryTransform.GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);

        entryTransform.Find("Score Template").GetComponent<Text>().text = entry.FormatTime();
        entryTransform.Find("Name Template").GetComponent<Text>().text = entry.name;

        entryTransform.gameObject.SetActive(true);
        transformList.Add(entryTransform);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetLeaderBoard()
    {
        PlayerPrefs.DeleteKey("Leaderboard");
        SceneManager.LoadScene(2);
    }

    void SaveData()
    {
        HiScores hiScores = new HiScores { hiScoreList = hiScoreEntryList};
        string json = JsonUtility.ToJson(hiScores);

        PlayerPrefs.SetString("Leaderboard", json);
        PlayerPrefs.Save();
    }

    void LoadData()
    {
        if (PlayerPrefs.HasKey("Leaderboard"))
        {
            string json = PlayerPrefs.GetString("Leaderboard");
            HiScores hiScores = JsonUtility.FromJson<HiScores>(json);
            hiScoreEntryList = hiScores.hiScoreList;
        }
        else
        {
            hiScoreEntryList = new List<HiScoreEntry>()
            {
                new HiScoreEntry{name = "AAA", score = 3355},
                new HiScoreEntry{name = "AAA", score = 3355},
                new HiScoreEntry{name = "AAA", score = 3355},
                new HiScoreEntry{name = "AAA", score = 3355},
                new HiScoreEntry{name = "AAA", score = 3355},
                new HiScoreEntry{name = "AAA", score = 3355},
                new HiScoreEntry{name = "AAA", score = 3355},
                new HiScoreEntry{name = "AAA", score = 3355},
                new HiScoreEntry{name = "AAA", score = 3355},
                new HiScoreEntry{name = "AAA", score = 3355}
            };
        }
    }

    [System.Serializable]
    public class HiScores
    {
        public List<HiScoreEntry> hiScoreList;
    }
}
