using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour {
  public static GameManager Instance;

  public string UserName;
  public string HighScoreUserName = "Name";
  public int LastScore = 0;
  public int HighScore = 0;


  private void Awake() {
    if (Instance != null) {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    DontDestroyOnLoad(gameObject);
    LoadHighScore();
  }

  [System.Serializable]
  public class ScoreData {
    public string UserName = "Name";
    public int HighScore = 0;
  }

  [System.Serializable]
  public class TopScores {
    public List<ScoreData> scores = new List<ScoreData>();
  }

  public void SaveHighScore() {
    TopScores currentScores = LoadListFromFile();
    // HighScore = LastScore;
    // HighScoreUserName = UserName;
    ScoreData data = new ScoreData();
    // data.UserName = HighScoreUserName;
    // data.HighScore = HighScore;
    data.UserName = UserName;
    data.HighScore = LastScore;

    AddScoreToListAndSave(currentScores, data);
  }

  public void LoadHighScore() {
    TopScores currentScores = LoadListFromFile();
    List<ScoreData> scores = currentScores.scores;
    int lastIndex = scores.Count - 1;
    HighScoreUserName = scores[lastIndex].UserName;
    HighScore = scores[lastIndex].HighScore;
  }

  public void AddScoreToListAndSave(TopScores topScores, ScoreData newScore) {
    topScores.scores.Add(newScore);
    topScores.scores.OrderByDescending(s => s.HighScore);
    SaveListToFile(topScores);
  }

  public void SaveListToFile(TopScores dataList) {
    string jsonString = JsonUtility.ToJson(dataList, true);
    File.WriteAllText(Application.persistentDataPath + "/savefile.json", jsonString);
  }

  public TopScores LoadListFromFile() {
    string path = Application.persistentDataPath + "/savefile.json";
    Debug.Log(path);
    if (File.Exists(path)) {
      string jsonString = File.ReadAllText(path);
      return JsonUtility.FromJson<TopScores>(jsonString);
    } else {
      Debug.LogWarning("Save file not found at: " + path);
      return new TopScores();
    }
  }
}
