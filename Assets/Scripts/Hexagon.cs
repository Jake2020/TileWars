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
    private Board boardObject;
    private Button hexagonButton;
    private string hexagonCurrentState;
    private TextMeshProUGUI hexagonText;
    private Image hexagonImage;

    private float hexagonX;
    private float hexagonY;
    private float hexagonZ;

    public const int HORIZONTALOFFSET = 70; 
    public const int VERTICALOFFSET = 80; 
    public const int VERTICALDIAGONALOFFSET = 40; 

    // Properties
    public Board BoardObject => boardObject;
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
        boardObject = FindObjectOfType<Board>();
        hexagonText = GetComponentInChildren<TextMeshProUGUI>();
        hexagonImage = GetComponent<Image>();
        hexagonButton = GetComponent<Button>();
        
        hexagonX = transform.position.x;
        hexagonY = transform.position.y;
        hexagonZ = transform.position.z;
    }

    public List<Hexagon> FindTouchingHexagons() {
        List<Hexagon> touchingHexagonsArray = new List<Hexagon>();

        int[] horizontalOffsets = { 0, HORIZONTALOFFSET, HORIZONTALOFFSET, 0, -HORIZONTALOFFSET, -HORIZONTALOFFSET};
        int[] verticalOffsets = { VERTICALOFFSET, VERTICALDIAGONALOFFSET, -VERTICALDIAGONALOFFSET, -VERTICALOFFSET, -VERTICALDIAGONALOFFSET, VERTICALDIAGONALOFFSET  };

        for (int i = 0; i < 6; i++) { 
        
            float targetX = this.hexagonX + horizontalOffsets[i];
            float targetY = this.hexagonY + verticalOffsets[i];

            Hexagon touchingHexagon = boardObject.AllHexagons.FirstOrDefault(h => h.hexagonX == targetX && h.hexagonY == targetY);

            if (touchingHexagon != null)
            {
                touchingHexagonsArray.Add(touchingHexagon);
            }
        }

        return touchingHexagonsArray;
    } 

    public void SetHexagonState(HexagonStates state) {
        switch (state.StateName)
        {
            case "homeTeam1":
            case "homeTeam2":
                HexagonText.text = "*";
                MakeTouchingHexagonsNeutralAroundHome();
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

    public void MakeTouchingHexagonsNeutralAroundHome() {
        List<Hexagon> touchingHexagons = FindTouchingHexagons();
        foreach (Hexagon touchingHexagon in touchingHexagons) {
            string hexState = touchingHexagon.HexagonCurrentState;

            if (hexState == "invisible") {
                touchingHexagon.SetHexagonState(boardObject.Neutral);
            }
        }
    }  
}
