using System.Collections.Generic;
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
    private bool team1Turn = true;
    private CurrentWord currentWordObjectOnScreen;
    private List<string> listOfLettersPressed;
    private SpellCheck spellCheck = new();
    
    // Properties
    public Hexagon[] AllHexagons => allHexagons;
    public bool Team1Turn => team1Turn;
    public CurrentWord CurrentWordObjectOnScreen => currentWordObjectOnScreen;
    public List<string> ListOfLettersPressed => listOfLettersPressed;
    public SpellCheck SpellCheck => spellCheck; 

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
        InitilizeComponents();
        MakeAllHexagonsInvisible();
        SetHomeBases();       
    }

    private void InitilizeComponents() {
        allHexagons = GetComponentsInChildren<Hexagon>();
    }

    public void HexagonPressed(Hexagon hex) {
        string hexState = hex.HexagonCurrentState;
        if (hexState == "neutral") {
            //CurrentWordUpdate(hex.HexagonText.text);
            if (team1Turn) {
                hex.SetHexagonState(PressedTeam1);
            } else {
                hex.SetHexagonState(PressedTeam2);
            }
        } else if (hexState == "pressedTeam1" && team1Turn) {
            //CurrentWordRemove(hex.HexagonText.text);
            hex.SetHexagonState(Neutral);
        } else if (hexState == "pressedTeam2" && !team1Turn) {
            //CurrentWordRemove(hex.HexagonText.text);
            hex.SetHexagonState(Neutral);
        }
    }

    private void MakeAllHexagonsInvisible(){
        foreach (Hexagon hex in AllHexagons){ 
            hex.SetHexagonState(Invisible);
        }
    }

    private void SetHomeBases() {
        allHexagons[9].SetHexagonState(HomeTeam1);
        allHexagons[30].SetHexagonState(HomeTeam2);
    }
}
