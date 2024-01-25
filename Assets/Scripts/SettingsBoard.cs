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

    private Color CreateNewColor(Color colorResultTeam1) {
        Color newColour = new()
        {
            r = colorResultTeam1.r * 1.2f,
            g = colorResultTeam1.g * 1.2f,
            b = colorResultTeam1.b * 1.2f
        };
        return newColour;
    }

    public void SaveColour() {
        Color newTeam1Pressed = CreateNewColor(ColorResultTeam1.color);
        Color newTeam2Pressed = CreateNewColor(ColorResultTeam2.color);

        PlayerPrefs.SetString("PressedTeam1Color", ColorUtility.ToHtmlStringRGB(newTeam1Pressed));
        PlayerPrefs.SetString("PressedTeam2Color", ColorUtility.ToHtmlStringRGB(newTeam2Pressed));

        PlayerPrefs.SetString("HomeTeam1Color", ColorUtility.ToHtmlStringRGB(ColorResultTeam1.color));
        PlayerPrefs.SetString("HomeTeam2Color", ColorUtility.ToHtmlStringRGB(ColorResultTeam2.color));

        PlayerPrefs.SetString("TerritoryTeam1Color", ColorUtility.ToHtmlStringRGB(ColorResultTeam1.color));
        PlayerPrefs.SetString("TerritoryTeam2Color", ColorUtility.ToHtmlStringRGB(ColorResultTeam2.color));

        Debug.Log("Colours saved");
    }
    
    public void LoadMenuScene() {
        SceneManager.LoadScene("Main Menu Scene");
    } 

    public void ApplySettings() {
        SaveColour();
    }
}
