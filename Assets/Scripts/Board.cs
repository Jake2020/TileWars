using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Hexagon[] allHexagons;

    public Board boardObject;

    public Hexagon.HexagonStates home;
    public Hexagon.HexagonStates invisible;
    public Hexagon.HexagonStates neutral;
    public Hexagon.HexagonStates territory;
   
    void Start()
    {
        allHexagons = GetComponentsInChildren<Hexagon>();
        boardObject = FindObjectOfType<Board>();

        foreach (Hexagon hex in allHexagons){
            hex.SetHexagonState(invisible, allHexagons, boardObject);
        }

        allHexagons[9].SetHexagonState(home, allHexagons, boardObject);
    }

    void Update()
    {
        
    }
}
