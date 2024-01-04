using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Hexagon : MonoBehaviour
{
    [System.Serializable]
    public class HexagonStates
    {
        [SerializeField]
        private Color fillColor;

        [SerializeField]
        private string stateName;

        public Color FillColor
        {
            get { return fillColor; }
        }

        public string StateName
        {
            get { return stateName; }
        }
    }
    private TextMeshProUGUI hexagonText;
    private Image hexagonImage;
    private string hexagonCurrentState = "Neutral";

    public string HexagonCurrentState
        {
            get { return hexagonCurrentState; }
        }

    void Awake()
    {
        hexagonText = GetComponentInChildren<TextMeshProUGUI>();
        hexagonImage = GetComponent<Image>();
    }

    public void SetHexagonState(HexagonStates state){
        if (state == FindObjectOfType<Board>().home) {
            hexagonText.text = "*";
        }
        hexagonImage.color = state.FillColor;
        hexagonCurrentState = state.StateName;
    }

    public void DecideHexagonState(){
        if (this.HexagonCurrentState == "Neutral"){
            hexagonText.text = "N";
            return;
        }
        
    }
}
