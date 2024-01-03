using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hexagon : MonoBehaviour
{
    private TextMeshProUGUI hexagonText;

    // Start is called before the first frame update
    void Awake()
    {
        hexagonText = GetComponentInChildren<TextMeshProUGUI>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitialiseHome(){
        hexagonText.text = "*";
    }
}
