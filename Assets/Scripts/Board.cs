using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public SpellCheck spellCheck = new SpellCheck();
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
        spellCheck = new SpellCheck();;

        MakeAllHexagonsInvisible(); //at game start, set hexes to invisible

        allHexagons[9].SetHexagonState(homeTeam1, allHexagons, boardObject); //setting home for team 1
        allHexagons[30].SetHexagonState(homeTeam2, allHexagons, boardObject); //setting home for team 2
    }

    void Update() //main game loop, is called every tick
    {
        
    }

    private void ClearPressedHexagons(){
        foreach (Hexagon hex in allHexagons){
            if (hex.HexagonCurrentState == "pressedTeam1" || hex.HexagonCurrentState == "pressedTeam2"){
                hex.DeleteLetter();
                hex.SetHexagonState(neutral, allHexagons, boardObject);
            }
        }
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

                if (hexState == "homeTeam2"){
                    touchingHexagon.SetLetter();
                    SetNewHome("homeTeam2");
                }
            }
            if (!team1Turn && hexState != "homeTeam2" && hexState != "territoryTeam2" && hexState != "pressedTeam2" ){
                touchingHexagon.SetHexagonState(boardObject.neutral, allHexagons, boardObject);

                if (hexState == "homeTeam1"){
                    touchingHexagon.SetLetter();
                    SetNewHome("homeTeam1");
                }
            }
        }
    }

    public void MakePressedHexagonsTerritory(Hexagon hex){ //go through all hexagons, any that are in a pressed state become territory, also update the neighboring hexagons 
        
            if (hex.HexagonCurrentState == "territoryTeam1" || hex.HexagonCurrentState ==  "homeTeam1"){
                List<Hexagon> touchingHexes = hex.FindTouchingHexagons(allHexagons, boardObject);
                foreach (Hexagon touchingHex in touchingHexes){
                    if (touchingHex.HexagonCurrentState == "pressedTeam1"){
                        touchingHex.DeleteLetter(); //make hex empty again

                        touchingHex.SetHexagonState(territoryTeam1, allHexagons, boardObject); //set to territory
                        List<Hexagon> touchingHexagonsArray = touchingHex.FindTouchingHexagons(allHexagons, boardObject); //find hexes touching the new territory hex 
                        MakeTouchingHexagonsNeutral(touchingHexagonsArray); //new territory hexes neighbors become neutral if applicable
                        MakePressedHexagonsTerritory(touchingHex);
                    }
                }               
            }
            if (hex.HexagonCurrentState == "territoryTeam2" || hex.HexagonCurrentState ==  "homeTeam2"){
                List<Hexagon> touchingHexes = hex.FindTouchingHexagons(allHexagons, boardObject);
                foreach (Hexagon touchingHex in touchingHexes){
                    if (touchingHex.HexagonCurrentState == "pressedTeam2"){
                        touchingHex.DeleteLetter(); //make hex empty again

                        touchingHex.SetHexagonState(territoryTeam2, allHexagons, boardObject); //set to territory
                        List<Hexagon> touchingHexagonsArray = touchingHex.FindTouchingHexagons(allHexagons, boardObject); //find hexes touching the new territory hex 
                        MakeTouchingHexagonsNeutral(touchingHexagonsArray); //new territory hexes neighbors become neutral if applicable
                        MakePressedHexagonsTerritory(touchingHex);
                    }
                }               
            }
        
    }

    public void ChangeTurn(){ //flip the turn bool to the other state, indicating its the other players turn
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

    private void SetNewHome(string team){
        if (team == "homeTeam2"){
            Hexagon hex = SelectRandomHexagonOfType("territoryTeam2");
            ChangeTurn();
            hex.SetHexagonState(homeTeam2, allHexagons, boardObject);
            ChangeTurn(); //changing turn before and after so that when the new home is set, its that players turn, so their home doesnt turn their territory neutral
        } else{
            Hexagon hex = SelectRandomHexagonOfType("territoryTeam1");
            ChangeTurn();
            hex.SetHexagonState(homeTeam1, allHexagons, boardObject);
            ChangeTurn();
        }
        ChangeTurn();
    }

    private Hexagon SelectRandomHexagonOfType(string state){
        List<Hexagon> targetHexes = new List<Hexagon>();
        foreach (Hexagon hex in allHexagons){
            if (hex.HexagonCurrentState == state){
                targetHexes.Add(hex);
            }
        }
        return targetHexes[UnityEngine.Random.Range(0, targetHexes.Count)];
        throw new NullReferenceException("didnt return hex");
    }
    

    public void SubmitButtonPressed(){
        
        foreach (Hexagon hex in allHexagons){
            MakePressedHexagonsTerritory(hex);
        }
        ClearPressedHexagons();
        ChangeTurn();
        Debug.Log(spellCheck.IsWord("apzzzple"));

    }
}
