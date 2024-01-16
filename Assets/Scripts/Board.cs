using System.Collections.Generic;
using Unity.VisualScripting;
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
    private List<string> listOfLettersPressed = new();
    private SpellCheck spellCheck = new();
    
    // Properties
    public Hexagon[] AllHexagons
    {
        get => allHexagons;
        set => allHexagons = value;
    }
    public bool Team1Turn
    {
        get => team1Turn;
        set => team1Turn = value;
    }
    public CurrentWord CurrentWordObjectOnScreen
    {
        get => currentWordObjectOnScreen;
        set => currentWordObjectOnScreen = value;
    }
    public List<string> ListOfLettersPressed 
    {
        get => listOfLettersPressed;
        set => listOfLettersPressed = value;
    }

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
        spellCheck = new SpellCheck();
        CurrentWordObjectOnScreen = GetComponentInChildren<CurrentWord>();
    }

    public void ChangeTurn() { 
        if (Team1Turn){
            Team1Turn = false;
        } else{
            Team1Turn = true;
        }
    }

    private void CurrentWordUpdate(string letter) {
        ListOfLettersPressed.Add(letter);
        string word = string.Join("", ListOfLettersPressed);
        CurrentWordObjectOnScreen.UpdateCurrentWord(word);
    }

    private void CurrentWordRemove(string letter){
        ListOfLettersPressed.Reverse();
        ListOfLettersPressed.Remove(letter);
        ListOfLettersPressed.Reverse();
        string word = string.Join("", ListOfLettersPressed);
        CurrentWordObjectOnScreen.UpdateCurrentWord(word);
    }

    private void ClearPressedHexagonsValidWord() {
        foreach (Hexagon hex in allHexagons)
        {
            if (IsHexagonPressed(hex))
            {
                ClearHexagon(hex);
            }
        }
    }

    private bool IsHexagonPressed(Hexagon hex) {
        return hex.HexagonCurrentState == "pressedTeam1" || hex.HexagonCurrentState == "pressedTeam2";
    }

    private void ClearHexagon(Hexagon hex) {
        hex.DeleteLetter();
        hex.SetHexagonState(Neutral);
    }

    private void ClearPressedHexagonsInvaildWord() {
        foreach (Hexagon hex in allHexagons)
        {
            if (IsHexagonPressed(hex))
            {
                ClearInavlidHexagon(hex);
            }
        }
    }

    private void ClearInavlidHexagon(Hexagon hex) {
        hex.SetHexagonState(Neutral);
    }

    private HexagonStates GetCurrentTeam() {
        if (Team1Turn) {
            return PressedTeam1;
        } else {
            return PressedTeam2;
        }
    }

    public void HexagonPressed(Hexagon hex) {
        string hexState = hex.HexagonCurrentState;
        if (hexState == "neutral") {
            CurrentWordUpdate(hex.HexagonText.text);
            hex.SetHexagonState(GetCurrentTeam());

        } else if (hexState == "pressedTeam1" || hexState == "pressedTeam2") {
            CurrentWordRemove(hex.HexagonText.text);
            hex.SetHexagonState(Neutral);

        } else {
            //if state is home/territory/invisible then do nothing
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

    public void SubmitButtonPressed() {
        bool isValidWord = spellCheck.IsValidWord(CurrentWordObjectOnScreen.CurrentWordText.text);

        if (isValidWord)
        {
            ProcessValidWord();
        }
        else
        {
            ProcessInvalidWord();
        }
    }

    private void ProcessValidWord() {
        foreach (Hexagon hex in AllHexagons)
        {
            //MakePressedHexagonsTerritory(hex);
        }

        ClearPressedHexagonsValidWord();
        ChangeTurn();
        ResetWordState();
    }

    private void ProcessInvalidWord() {
        ClearPressedHexagonsInvaildWord();
        ResetWordState();
    }

    private void ResetWordState() {
        CurrentWordObjectOnScreen.UpdateCurrentWord("");
        ListOfLettersPressed.Clear();
    }

}
