using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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

        if (gameManager != null)
        {
            hiScoreEntryList.Add(gameManager.RunScore);
            gameManager.SelfDestruct();
        }

        hiScoreEntryList = hiScoreEntryList.OrderBy(e => e.score).ToList();
        TrimList();

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

    void SaveData()
    {
        HiScores hiScores = new HiScores { hiScoreList = hiScoreEntryList};
        string json = JsonUtility.ToJson(hiScores);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HiScores hiScores = JsonUtility.FromJson<HiScores>(json);
            hiScoreEntryList = hiScores.hiScoreList;
        }
    }

    [System.Serializable]
    public class HiScores
    {
        public List<HiScoreEntry> hiScoreList;
    }
}
