using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public Hexagon.HexagonStates home;
    public Hexagon.HexagonStates territory;
    public Hexagon.HexagonStates neutral;
    public Hexagon.HexagonStates invisible;


    private Hexagon[] allHexagons;
   
    void Start()
    {
        allHexagons = GetComponentsInChildren<Hexagon>();

        foreach (Hexagon hex in allHexagons){
            hex.SetHexagonState(invisible, allHexagons);
        }

        allHexagons[9].SetHexagonState(home, allHexagons);
    }

    void Update()
    {
        
    }
}
