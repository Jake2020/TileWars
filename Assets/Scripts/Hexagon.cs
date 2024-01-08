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
    public class HexagonStates //states are created on the board, and are used to assign hexes to either homes, neutrals, or territory
                               //the states show up in unity inspector in board, can be edited there
    {
        [SerializeField] //to make fields available and editable in unity editor
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

    private Button hexagonButton; //variable to hold each hexagons button component
    private string hexagonCurrentState; //just a reference for the state, doesnt change anything by itself
    private TextMeshProUGUI hexagonText; //variable to hold the text component of each hex
    private Image hexagonImage; //variable to hold image component of each hex
    

    public float hexagonX {get; private set; }
    public float hexagonY {get; private set; }
    public float hexagonZ {get; private set; } //x, y, and z coords of hexagon (exact number is different than unity inspector shows, distance between hexes is the same)

    public const int HORIZONTALOFFSET = 70; //how far to the left or right each hex is away from its adjacent column
    public const int VERTICALOFFSET = 80; //how far above or below the hexes in the same column are from each other
    public const int VERTICALDIAGONALOFFSET = 40; ///how far above or below the hexes in adjacent columns are from each other
   
    public string HexagonCurrentState{

            get { return hexagonCurrentState; }
        }

    void Awake(){

        hexagonText = GetComponentInChildren<TextMeshProUGUI>(); //pull text component into variable
        hexagonImage = GetComponent<Image>(); //pull image component into the variable
        hexagonButton = GetComponent<Button>(); //pull button component into the variable
        
        hexagonX = transform.position.x; //pulls x, y, and z coords from transform component into variables
        hexagonY = transform.position.y;
        hexagonZ = transform.position.z;

        hexagonButton.onClick.AddListener(() => FindObjectOfType<Board>().HexagonPressed(this));
    }

    public void DeleteLetter(){
        hexagonText.text = "";
    }

    public void SetLetter(){
        hexagonText.text = Letter.GenerateLetter();
    }

    public List<Hexagon> FindTouchingHexagons(Hexagon[] allHexagons, Board boardObject){ //create an array of hexes touching the current hex object, and turn them neutral

        List<Hexagon> touchingHexagonsArray = new List<Hexagon>();
        //define the offsets for where the six neighboring hexagons could be
        int[] horizontalOffsets = { 0, HORIZONTALOFFSET, HORIZONTALOFFSET, 0, -HORIZONTALOFFSET, -HORIZONTALOFFSET};
        int[] verticalOffsets = { VERTICALOFFSET, VERTICALDIAGONALOFFSET, -VERTICALDIAGONALOFFSET, -VERTICALOFFSET, -VERTICALDIAGONALOFFSET, VERTICALDIAGONALOFFSET  };

        
        for (int i = 0; i < 6; i++){ //loop through all 6 possible positios for neighboring hexagons
        
            float targetX = this.hexagonX + horizontalOffsets[i];
            float targetY = this.hexagonY + verticalOffsets[i];

            Hexagon touchingHexagon = allHexagons.FirstOrDefault(h => h.hexagonX == targetX && h.hexagonY == targetY); //do the search for a hex with the target x and y coords

            if (touchingHexagon != null) //if there is a hexagon there, add it to the array of touching hexagons
            {
                touchingHexagonsArray.Add(touchingHexagon);
            }
        }

        return touchingHexagonsArray;
    }

    //set the state of a hex 
    public void SetHexagonState(HexagonStates state, Hexagon[] allHexagons, Board boardObject){ //pass in the desired state, list of all hexagons, and a reference for the board
        if (state == boardObject.homeTeam1) {
            hexagonText.text = "*";
            List<Hexagon> touchingHexagonsArray = this.FindTouchingHexagons(allHexagons, boardObject); //if hex state becomes home, find neighbors and make them neutral
            boardObject.MakeTouchingHexagonsNeutral(touchingHexagonsArray);
        }

        if (state == boardObject.homeTeam2) {
            hexagonText.text = "*";
            List<Hexagon> touchingHexagonsArray = this.FindTouchingHexagons(allHexagons, boardObject); //if hex state becomes home, find neighbors and make them neutral
            boardObject.MakeTouchingHexagonsNeutral(touchingHexagonsArray);
        }
        
        if(state == boardObject.neutral && string.IsNullOrWhiteSpace(hexagonText.text) ){
            hexagonText.text = Letter.GenerateLetter(); //genereate random letter with Letter class
        }

        hexagonImage.color = state.FillColor; //set the hexes color to the state color assigned in unty editor
        hexagonCurrentState = state.StateName; //update hexes state variable for qol 
    }
    
}
