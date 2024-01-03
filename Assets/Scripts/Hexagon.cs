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
    }
    private TextMeshProUGUI hexagonText;
    private Image hexagonImage;

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
    }
}
