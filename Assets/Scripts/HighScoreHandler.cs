using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class HighScoreHandler : MonoBehaviour {
  [SerializeField] private TMP_Text scoreList;
  private GameManager.TopScores topScores;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
    scoreList.text = "";
    int count = 1;
    topScores = GameManager.Instance.LoadListFromFile();
    List<GameManager.ScoreData> sortedScores = topScores.scores.OrderByDescending(s => s.Score).ToList();
    for (int i = 0; i < sortedScores.Count; i++) {
      if (count <= 10) {
        scoreList.text += $"{count}.  {sortedScores[i].UserName} -- {sortedScores[i].Score}\n";
        count++;
      } else {
        break;
      }
    }
  }

  public void BackToMenu() {
    SceneManager.LoadScene(0);
  }
}
