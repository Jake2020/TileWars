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

    private string hexagonCurrentState = "Neutral";
    private TextMeshProUGUI hexagonText;
    private Image hexagonImage;
    

    public float hexagonX {get; private set; }
    public float hexagonY {get; private set; }
    public float hexagonZ {get; private set; }

    public const int HORIZONTALOFFSET = 70;
    public const int VERTICALOFFSET = 80;
    public const int VERTICALDIAGONALOFFSET = 40;
   
    public string HexagonCurrentState
        {
            get { return hexagonCurrentState; }
        }

    void Awake()
    {
        hexagonText = GetComponentInChildren<TextMeshProUGUI>();
        hexagonImage = GetComponent<Image>();
        
        hexagonX = transform.position.x;
        hexagonY = transform.position.y;
        hexagonZ = transform.position.z;
    }

    public void FindTouchingHexagons(Hexagon[] allHexagons, Board boardObject)
{
    List<Hexagon> touchingHexagonsArray = new List<Hexagon>();

    // Define the offsets for the six neighboring hexagons
    int[] horizontalOffsets = { 0, HORIZONTALOFFSET, HORIZONTALOFFSET, 0, -HORIZONTALOFFSET, -HORIZONTALOFFSET};
    int[] verticalOffsets = { VERTICALOFFSET, VERTICALDIAGONALOFFSET, -VERTICALDIAGONALOFFSET, -VERTICALOFFSET, -VERTICALDIAGONALOFFSET, VERTICALDIAGONALOFFSET  };

    // Find the six neighboring hexagons
    for (int i = 0; i < 6; i++)
    {
        float targetX = this.hexagonX + horizontalOffsets[i];
        float targetY = this.hexagonY + verticalOffsets[i];

        Hexagon touchingHexagon = allHexagons.FirstOrDefault(h => h.hexagonX == targetX && h.hexagonY == targetY);

        if (touchingHexagon != null)
        {
            touchingHexagonsArray.Add(touchingHexagon);
        }
    }

    // Set the state for each neighboring hexagon
    foreach (Hexagon touchingHexagon in touchingHexagonsArray)
    {
        touchingHexagon.SetHexagonState(boardObject.neutral, allHexagons, boardObject);
    }
}


    public void SetHexagonState(HexagonStates state, Hexagon[] allHexagons, Board boardObject){
        if (state == boardObject.homeTeam1) {
            hexagonText.text = "*";
            this.FindTouchingHexagons(allHexagons, boardObject);
        }

        if (state == boardObject.homeTeam2) {
            hexagonText.text = "*";
            this.FindTouchingHexagons(allHexagons, boardObject);
        }
        
        if(state == boardObject.neutral){
            hexagonText.text = Letter.GenerateLetter().ToString();
        }

        hexagonImage.color = state.FillColor;
        hexagonCurrentState = state.StateName;
    }
}
