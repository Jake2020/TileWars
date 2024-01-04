using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hexagon : MonoBehaviour
{
    [System.Serializable]
    public class HexagonStates
    {
        public Color fillColor;
        public string stateName;
    }
    private TextMeshProUGUI hexagonText;
    private Image hexagonImage;
    private string hexagonCurrentState;

    void Awake()
    {
        hexagonText = GetComponentInChildren<TextMeshProUGUI>();
        hexagonImage = GetComponent<Image>();
    }

    public void SetHexagonState(HexagonStates state){
        if (state == FindObjectOfType<Board>().home) {
            hexagonText.text = "*";
        }
        hexagonImage.color = state.fillColor;
        hexagonCurrentState = state.stateName;
    }

    public void GetHexagonState(){

    }

    public void DecideHexagonState(Hexagon[] allHexagons){
        foreach (Hexagon hex in allHexagons){
            
        }
    }
}
