using UnityEngine;
using TMPro;

public class CurrentWord : MonoBehaviour
{
    // Fields
    [SerializeField]
    private TextMeshProUGUI currentWordText;
    public TextMeshProUGUI CurrentWordText => currentWordText;

    // Methods
    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        currentWordText = GetComponentInChildren<TextMeshProUGUI>();
        if (currentWordText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found in children.", this);
        }
    }

    public void UpdateCurrentWord(string word)
    {
        if (currentWordText != null)
        {
            currentWordText.text = word;
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not initialized.", this);
        }
    }
}