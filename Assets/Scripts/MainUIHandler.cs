using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIHandler : MonoBehaviour {
  public void ReturnToMenu() {
    SceneManager.LoadScene(0);
  }
}
