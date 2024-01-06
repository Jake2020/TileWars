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

    public void FindTouchingHexagon(Hexagon[] allHexagons, Board boardObject){
        List<Hexagon> touchingHexagonsArray = new List<Hexagon>();
        touchingHexagonsArray.Add(allHexagons.FirstOrDefault(h => h.hexagonX == this.hexagonX && h.hexagonY == this.hexagonY-VERTICALOFFSET));

        touchingHexagonsArray[0].SetHexagonState(boardObject.neutral, allHexagons, boardObject);
    }

    public void SetHexagonState(HexagonStates state, Hexagon[] allHexagons, Board boardObject){
        if (state == boardObject.home) {
            hexagonText.text = "*";
            this.FindTouchingHexagon(allHexagons, boardObject);
        }
        
        if(state == boardObject.neutral){
            hexagonText.text = "N";
        }

        hexagonImage.color = state.FillColor;
        hexagonCurrentState = state.StateName;
    }
}
