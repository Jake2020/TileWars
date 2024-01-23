using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBoard : MonoBehaviour {
    // Class Methods
    public void LoadGameScene() {
        SceneManager.LoadScene("Game Scene");
    }

    public void LoadSettingsScene() {
        SceneManager.LoadScene("Settings Scene");
    }
}
