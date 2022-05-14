using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// Thanks to Code Monkey

public class HiScoreTable : MonoBehaviour
{
    [SerializeField] Transform entryTemplate;
    [SerializeField] Transform entryContainer;
    [SerializeField] float templateHeight = 20f;

    List<HiScoreEntry> hiScoreEntryList;
    List<Transform> transformList;
    
    private void Awake()
    {
        transformList = new List<Transform>();

        hiScoreEntryList = new List<HiScoreEntry>()
        {
            new HiScoreEntry{name = "AAA", score = 999},
            new HiScoreEntry{name = "AAA", score = 999},
            new HiScoreEntry{name = "AAA", score = 999},
            new HiScoreEntry{name = "AAA", score = 999},
            new HiScoreEntry{name = "AAA", score = 999},
            new HiScoreEntry{name = "AAA", score = 999},
            new HiScoreEntry{name = "AAA", score = 999},
            new HiScoreEntry{name = "AAA", score = 999},
            new HiScoreEntry{name = "AAA", score = 999},
            new HiScoreEntry{name = "AAA", score = 999}
        };

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

        int scoreSeconds = entry.score; // for ordering purposes

        entryTransform.Find("Score Template").GetComponent<Text>().text = entry.FormatTime();
        entryTransform.Find("Name Template").GetComponent<Text>().text = entry.name;

        entryTransform.gameObject.SetActive(true);
        transformList.Add(entryTransform);
    }

    public void SaveData()
    {
        HiScores hiScores = new HiScores { hiScoreList = hiScoreEntryList};
        string json = JsonUtility.ToJson(hiScores);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
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
