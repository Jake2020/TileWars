using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsBoard : MonoBehaviour
{
    //Fields
    private Image colorResultTeam1;
    private Image colorResultTeam2;

    //Properties
    public Image ColorResultTeam1
    {
        get => colorResultTeam1;
        set => colorResultTeam1 = value;
    }
    public Image ColorResultTeam2
    {
        get => colorResultTeam2;
        set => colorResultTeam2 = value;
    }

    //Methods
    void Awake() {
        ColorResultTeam1 = GameObject.FindGameObjectWithTag("ColorTeam1").GetComponent<Image>();
        ColorResultTeam2 = GameObject.FindGameObjectWithTag("ColorTeam2").GetComponent<Image>();
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
