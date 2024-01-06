using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Hexagon[] allHexagons;

    public Board boardObject;

    public Hexagon.HexagonStates homeTeam1;
    public Hexagon.HexagonStates homeTeam2;
    public Hexagon.HexagonStates invisible;
    public Hexagon.HexagonStates neutral;
    public Hexagon.HexagonStates pressedTeam1;
    public Hexagon.HexagonStates pressedTeam2;
    public Hexagon.HexagonStates territoryTeam1;
    public Hexagon.HexagonStates territoryTeam2;
   
    void Start()
    {
        allHexagons = GetComponentsInChildren<Hexagon>();
        boardObject = FindObjectOfType<Board>();

        foreach (Hexagon hex in allHexagons){
            hex.SetHexagonState(invisible, allHexagons, boardObject);
        }

        allHexagons[9].SetHexagonState(homeTeam1, allHexagons, boardObject);
        allHexagons[30].SetHexagonState(homeTeam2, allHexagons, boardObject);
    }

    void Update()
    {
        
    }
}
