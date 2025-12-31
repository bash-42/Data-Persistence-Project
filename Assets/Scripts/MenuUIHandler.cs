using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour {
  [SerializeField] private TMP_InputField userName;
  [SerializeField] private TMP_Text highScoreDisplay;
  private string userInput;
  private string highScoreUserName;
  private string highScore;

  private void Start() {
    highScoreUserName = GameManager.Instance.HighScoreUserName;
    highScore = GameManager.Instance.HighScore.ToString();
    highScoreDisplay.text = $"High Score - {highScoreUserName} - {highScore}";
  }

  public void StartNew() {
    SceneManager.LoadScene(2);
  }

  public void ShowHighScores() {
    SceneManager.LoadScene(1);
  }

  public void GetUserName() {
    userInput = userName.text;
    GameManager.Instance.UserName = userInput;
    Debug.Log("The name entered was: " + userInput);
  }

  public void Exit() {
    GameManager.Instance.SaveHighScore();
#if UNITY_EDITOR
    EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
  }
}
