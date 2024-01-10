using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentWord : MonoBehaviour
{
    private TextMeshProUGUI currentWordText;

    public TextMeshProUGUI CurrentWordText{
        get{
            return currentWordText;
        }
        private set{}
    }

    void Awake()
    {
        currentWordText = GetComponentInChildren<TextMeshProUGUI>(); //pull text component into variable
    }

    public void UpdateCurrentWord(string word){
        currentWordText.text = word;
    }
}
