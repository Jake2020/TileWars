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
    private TextMeshProUGUI hexagonText;
    private Image hexagonImage;
    private string hexagonCurrentState = "Neutral";
    public float hexagonX {get; private set; }
    public float hexagonY {get; private set; }
    public float hexagonZ {get; private set; }

    public const int VERTICALOFFSET = 80;
    public const int HORIZONTALOFFSET = 70;
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

    public void SetHexagonState(HexagonStates state, Hexagon[] allHexagons){
        if (state == FindObjectOfType<Board>().home) {
            hexagonText.text = "*";
            this.FindTouchingHexagon(allHexagons);
        }
        
        if(state == FindObjectOfType<Board>().neutral){
            hexagonText.text = "N";
        }

        hexagonImage.color = state.FillColor;
        hexagonCurrentState = state.StateName;
    }

    public void FindTouchingHexagon(Hexagon[] allHexagons){
        List<Hexagon> touchingHexagonsArray = new List<Hexagon>();
        touchingHexagonsArray.Add(allHexagons.FirstOrDefault(h => h.hexagonX == this.hexagonX && h.hexagonY == this.hexagonY-VERTICALOFFSET));

        touchingHexagonsArray[0].SetHexagonState(FindObjectOfType<Board>().neutral, allHexagons);
    }
}
