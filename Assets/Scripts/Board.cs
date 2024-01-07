using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Hexagon[] allHexagons; //array for all hexagon game objects

    public Board boardObject; //variable for board object instance

    public bool team1Turn = true;

    public Hexagon.HexagonStates homeTeam1;
    public Hexagon.HexagonStates homeTeam2;
    public Hexagon.HexagonStates invisible;
    public Hexagon.HexagonStates neutral;
    public Hexagon.HexagonStates pressedTeam1;
    public Hexagon.HexagonStates pressedTeam2;
    public Hexagon.HexagonStates territoryTeam1;
    public Hexagon.HexagonStates territoryTeam2; //the states show up in unity inspector in board, can be edited there
   
    void Start()
    {
        allHexagons = GetComponentsInChildren<Hexagon>(); //pulls in the hexagon game objects into the array
        boardObject = FindObjectOfType<Board>(); //pulls board object instance into variable

        MakeAllHexagonsInvisible(); //at game start, set hexes to invisible

        allHexagons[9].SetHexagonState(homeTeam1, allHexagons, boardObject); //setting home for team 1
        allHexagons[30].SetHexagonState(homeTeam2, allHexagons, boardObject); //setting home for team 2
    }

    void Update() //main game loop, is called every tick
    {
        
    }
    private void MakeAllHexagonsInvisible(){ //loop through all hexes and make them invisible
        foreach (Hexagon hex in allHexagons){ 
            hex.SetHexagonState(invisible, allHexagons, boardObject);
        }
    }

    public void MakeTouchingHexagonsNeutral(List<Hexagon> touchingHexagonsArray){ //loop through array of touching hexagons, and make them all neutral when applicable (eg not replacing own home)
        foreach (Hexagon touchingHexagon in touchingHexagonsArray){
            string hexState = touchingHexagon.HexagonCurrentState;
            if (team1Turn && hexState != "homeTeam1" && hexState != "territoryTeam1" && hexState != "pressedTeam1" ){
                touchingHexagon.SetHexagonState(boardObject.neutral, allHexagons, boardObject);
            }
            if (!team1Turn && hexState != "homeTeam2" && hexState != "territoryTeam2" && hexState != "pressedTeam2" ){
                touchingHexagon.SetHexagonState(boardObject.neutral, allHexagons, boardObject);
            }
        }
    }

    public void MakePressedHexagonsTerritory(){ //go through all hexagons, any that are in a pressed state become territory, also update the neighboring hexagons 
        foreach (Hexagon hex in allHexagons){
            if (hex.HexagonCurrentState == "pressedTeam1"){

                hex.DeleteLetter(); //make hex empty again

                hex.SetHexagonState(territoryTeam1, allHexagons, boardObject); //set to territory
                List<Hexagon> touchingHexagonsArray = hex.FindTouchingHexagons(allHexagons, boardObject); //find hexes touching the new territory hex 
                MakeTouchingHexagonsNeutral(touchingHexagonsArray); //new territory hexes neighbors become neutral if applicable
            }
            if (hex.HexagonCurrentState == "pressedTeam2"){ //same as above but for team 2

                hex.DeleteLetter(); //make hex empty again

                hex.SetHexagonState(territoryTeam2, allHexagons, boardObject);
                List<Hexagon> touchingHexagonsArray = hex.FindTouchingHexagons(allHexagons, boardObject);
                MakeTouchingHexagonsNeutral(touchingHexagonsArray);
            }
        }
    }

    public void ChangeTurn(){
        if (team1Turn){
            team1Turn = false;
        } else{
            team1Turn = true;
        }
    }

    public void HexagonPressed(Hexagon hex){
        string hexState = hex.HexagonCurrentState;
        if (hexState == "neutral" && team1Turn){
            hex.SetHexagonState(boardObject.pressedTeam1, allHexagons, boardObject);
        }
        if (hexState == "neutral" && !team1Turn){
            hex.SetHexagonState(boardObject.pressedTeam2, allHexagons, boardObject);
        }
        if (hexState == "pressedTeam1" && team1Turn){
            hex.SetHexagonState(boardObject.neutral, allHexagons, boardObject);
        }
        if (hexState == "pressedTeam2" && !team1Turn){
            hex.SetHexagonState(boardObject.neutral, allHexagons, boardObject);
        }

    }

    public void SubmitButtonPressed(){
        
        MakePressedHexagonsTerritory();
        ChangeTurn();

    }
}
