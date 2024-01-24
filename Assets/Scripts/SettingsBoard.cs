using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsBoard : MonoBehaviour
{
    //Fields
    private Image colourResult;

    //Properties
    public Image ColourResult
    {
        get => colourResult;
        set => colourResult = value;
    }

    //Methods
    void Awake() {
        ColourResult = GetComponentInChildren<Image>();
    }
    public void ChangeColour() {

    }
    public void LoadMenuScene() {
        SceneManager.LoadScene("Main Menu Scene");
    } 

    public void ApplySettings() {
        //apply settings
    }
}
