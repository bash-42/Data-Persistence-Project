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
    public int Score = 0;
  }

  [System.Serializable]
  public class TopScores {
    public List<ScoreData> scores = new List<ScoreData>();
  }

  public void SaveHighScore() {
    TopScores currentScores = LoadListFromFile();
    ScoreData data = new ScoreData();
    if (LastScore > HighScore) {
      HighScore = LastScore;
      HighScoreUserName = UserName;
      data.UserName = HighScoreUserName;
      data.Score = HighScore;
    } else {
      data.UserName = UserName;
      data.Score = LastScore;
    }

    AddScoreToListAndSave(currentScores, data);
  }

  public void LoadHighScore() {
    TopScores currentScores = LoadListFromFile();
    List<ScoreData> scores = currentScores.scores;
    List<ScoreData> sortedScores = scores.OrderByDescending(s => s.Score).ToList();

    HighScoreUserName = sortedScores[0].UserName;
    HighScore = sortedScores[0].Score;
  }

  public void AddScoreToListAndSave(TopScores topScores, ScoreData newScore) {
    topScores.scores.Add(newScore);
    topScores.scores.OrderByDescending(s => s.Score);
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
