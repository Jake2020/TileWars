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

    private Color CreateNewColor(Color colorResultTeam1, float saturationDelta, float lightnessDelta) {
        // Convert RGB to HSL
        Color.RGBToHSV(colorResultTeam1, out float h, out float s, out float l);
        
        // Modify saturation and lightness
        s = Mathf.Clamp01(s + saturationDelta);
        l = Mathf.Clamp01(l + lightnessDelta);

        // Convert HSL back to RGB
        Color modifiedColor = Color.HSVToRGB(h, s, l);
        return modifiedColor;
    
    }

    public void SaveColour() {
        Color newTeam1Pressed = CreateNewColor(ColorResultTeam1.color, -0.4f, 1.2f);
        Color newTeam2Pressed = CreateNewColor(ColorResultTeam2.color, -0.4f, 1.2f);

        PlayerPrefs.SetString("PressedTeam1Color", ColorUtility.ToHtmlStringRGB(newTeam1Pressed));
        PlayerPrefs.SetString("PressedTeam2Color", ColorUtility.ToHtmlStringRGB(newTeam2Pressed));

        PlayerPrefs.SetString("HomeTeam1Color", ColorUtility.ToHtmlStringRGB(ColorResultTeam1.color));
        PlayerPrefs.SetString("HomeTeam2Color", ColorUtility.ToHtmlStringRGB(ColorResultTeam2.color));

        PlayerPrefs.SetString("TerritoryTeam1Color", ColorUtility.ToHtmlStringRGB(ColorResultTeam1.color));
        PlayerPrefs.SetString("TerritoryTeam2Color", ColorUtility.ToHtmlStringRGB(ColorResultTeam2.color));

    }
    
    public void LoadMenuScene() {
        SceneManager.LoadScene("Main Menu Scene");
    } 

    public void ApplySettings() {
        SaveColour();
    }

    public void SmallBoard() {
        PlayerPrefs.SetInt("BoardCols", 7);
        PlayerPrefs.SetInt("BoardRows", 9);
    }

    public void MediumBoard() {
        PlayerPrefs.SetInt("BoardCols", 9);
        PlayerPrefs.SetInt("BoardRows", 11);
    }

    public void LargeBoard() {
        PlayerPrefs.SetInt("BoardCols", 11);
        PlayerPrefs.SetInt("BoardRows", 13);
    }
}
