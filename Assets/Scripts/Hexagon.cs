using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Hexagon : MonoBehaviour
{
    // Fields
    private Button hexagonButton;
    private string hexagonCurrentState;
    private TextMeshProUGUI hexagonText;
    private Image hexagonImage;

    private float hexagonX;
    private float hexagonY;
    private float hexagonZ;

    // Properties
    public TextMeshProUGUI HexagonText => hexagonText;
    public string HexagonCurrentState 
    {
        get => hexagonCurrentState;
        set => hexagonCurrentState = value;
    }
    public Image HexagonImage => hexagonImage;

    public float HexagonX => hexagonX;
    public float HexagonY => hexagonY;
    public float HexagonZ => hexagonZ;

    void Awake() {
        InitilizeComponents();
    }

    void InitilizeComponents() {
        hexagonText = GetComponentInChildren<TextMeshProUGUI>();
        hexagonImage = GetComponent<Image>();
        hexagonButton = GetComponent<Button>();
        
        hexagonX = transform.position.x;
        hexagonY = transform.position.y;
        hexagonZ = transform.position.z;
    }

    public void SetHexagonState(HexagonStates state) {
        switch (state.StateName)
        {
            case "homeTeam1":
            case "homeTeam2":
                HexagonText.text = "*";
                // Find touching hexagons 
                // Make touching hexagons neutral
                break;
            case "neutral":
                HexagonText.text = Letter.GenerateLetter();
                break;
            default:
                Console.WriteLine("Unknown color.");
                break;
        }

        HexagonImage.color = state.FillColor;
        HexagonCurrentState = state.StateName;
    }    
}
