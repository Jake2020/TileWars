using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Hexagon : MonoBehaviour
{
    // Fields
    private Board boardObject;
    private Button hexagonButton;
    [SerializeField]
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

    public float HexagonX
    {
        get => hexagonX;
        set => hexagonX = value;
    }
    public float HexagonY
    {
        get => hexagonY;
        set => hexagonY = value;
    }
    public float HexagonZ
    {
        get => hexagonZ;
        set => hexagonZ = value;
    }

    // Class Methods
    void Awake() {
        InitilizeComponents();
        hexagonButton.onClick.AddListener(() => boardObject.HexagonPressed(this));
    }

    void InitilizeComponents() {
        boardObject = FindObjectOfType<Board>();
        hexagonText = GetComponentInChildren<TextMeshProUGUI>();
        HexagonText.outlineWidth = 0.15F;
        HexagonText.outlineColor = Color.black;
        hexagonImage = GetComponent<Image>();
        hexagonButton = GetComponent<Button>();
    }

    public void DeleteLetter(){
        HexagonText.text = "";
    }

    public List<Hexagon> FindTouchingHexagons() {
        List<Hexagon> touchingHexagonsArray = new();

        int[] horizontalOffsets = { 0, HORIZONTALOFFSET, HORIZONTALOFFSET, 0, -HORIZONTALOFFSET, -HORIZONTALOFFSET};
        int[] verticalOffsets = { VERTICALOFFSET, VERTICALDIAGONALOFFSET, -VERTICALDIAGONALOFFSET, -VERTICALOFFSET, -VERTICALDIAGONALOFFSET, VERTICALDIAGONALOFFSET  };

        for (int i = 0; i < 6; i++) { 
        
            float targetX = this.HexagonX + horizontalOffsets[i];
            float targetY = this.HexagonY + verticalOffsets[i];

            Hexagon touchingHexagon = boardObject.AllHexagons.FirstOrDefault(h => h.HexagonX == targetX && h.HexagonY == targetY);

            if (touchingHexagon != null)
            {
                touchingHexagonsArray.Add(touchingHexagon);

            }
        }

        return touchingHexagonsArray;
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

    public void SetLetter(){
        HexagonText.text = Letter.GenerateLetter();
    }

    public void SetHexagonState(HexagonStates state) {
        switch (state.StateName)
        {
            case "homeTeam1":
            case "homeTeam2":
                HexagonText.text = "*";
                MakeTouchingHexagonsNeutralAroundHome();
                break;

            case "neutral":
                if (string.IsNullOrWhiteSpace(HexagonText.text)) { 
                    HexagonText.text = Letter.GenerateLetter();
                }
                break;

            default:
                Console.WriteLine("State not accepted");
                break;
        }

        HexagonImage.color = state.FillColor;
        HexagonCurrentState = state.StateName;
    }
}
