using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SettingsBoard : MonoBehaviour
{
    //Serialized Fields
    [SerializeField]
    private AudioSource applySettingsNoise;
    [SerializeField]
    private AudioSource chooseSettingsNoise;

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
    public AudioSource ChooseSettingsNoise
    {
        get => chooseSettingsNoise;
        set => chooseSettingsNoise = value;
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
        chooseSettingsNoise.Play();
        StartCoroutine(LoadSceneCoroutine("Main Menu Scene"));
    } 

    public void ApplySettings() {
        SaveColour();
        applySettingsNoise.Play();
    }

    public void SmallBoard() {
        PlayerPrefs.SetInt("BoardCols", 7);
        PlayerPrefs.SetInt("BoardRows", 9);
        chooseSettingsNoise.Play();
    }

    public void MediumBoard() {
        PlayerPrefs.SetInt("BoardCols", 9);
        PlayerPrefs.SetInt("BoardRows", 11);
        chooseSettingsNoise.Play();
    }

    public void LargeBoard() {
        PlayerPrefs.SetInt("BoardCols", 11);
        PlayerPrefs.SetInt("BoardRows", 13);
        chooseSettingsNoise.Play();
    }

    private IEnumerator LoadSceneCoroutine(string scene) {
        yield return new WaitWhile(() => chooseSettingsNoise.isPlaying);
        SceneManager.LoadScene(scene);
    }
}
