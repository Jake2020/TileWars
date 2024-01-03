using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public Hexagon.HexagonStates home;
    public Hexagon.HexagonStates territory;
    public Hexagon.HexagonStates neutral;


    private Hexagon[] allHexagons;
    // Start is called before the first frame update
    void Start()
    {
        allHexagons = GetComponentsInChildren<Hexagon>();
        allHexagons[0].SetHexagonState(territory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
