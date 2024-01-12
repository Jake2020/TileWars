using UnityEngine;

public class Board : MonoBehaviour
{
    // Hexagon States   
    [SerializeField]
    private HexagonStates homeTeam1;
    [SerializeField]
    private HexagonStates homeTeam2;
    [SerializeField]
    private HexagonStates invisible;
    [SerializeField]
    private HexagonStates neutral;
    [SerializeField]
    private HexagonStates pressedTeam1;
    [SerializeField]
    private HexagonStates pressedTeam2;
    [SerializeField]
    private HexagonStates territoryTeam1;
    [SerializeField]
    private HexagonStates territoryTeam2;

    // Fields
    private Hexagon[] allHexagons;

    // Properties
    public Hexagon[] AllHexagons => allHexagons;

    public HexagonStates HomeTeam1 => homeTeam1;
    public HexagonStates HomeTeam2 => homeTeam2;
    public HexagonStates Invisible => invisible;
    public HexagonStates Neutral => neutral;
    public HexagonStates PressedTeam1 => pressedTeam1;
    public HexagonStates PressedTeam2 => pressedTeam2;
    public HexagonStates TerritoryTeam1 => territoryTeam1;
    public HexagonStates TerritoryTeam2 => territoryTeam2;


    // Class Methods
    void Start() {
        allHexagons = GetComponentsInChildren<Hexagon>();

        SetHomeBases();        
    }

    private void SetHomeBases() {
        allHexagons[9].SetHexagonState(HomeTeam1);
        allHexagons[30].SetHexagonState(HomeTeam2);
    }
}
