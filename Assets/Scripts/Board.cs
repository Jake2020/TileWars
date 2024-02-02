using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using Unity.Collections;
using System.Collections;

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

    // Serialized Fields
    [SerializeField]
    private AudioSource audioPressed;
    [SerializeField]
    private AudioSource audioUnPressed;
    [SerializeField]
    private AudioSource audioWordSubmit;
    [SerializeField]
    private AudioSource audioWordFailed;
    [SerializeField]
    private AudioSource audioVictory;
    [SerializeField]
    private AudioSource chooseSettingsNoise;
    [SerializeField]
    private Button playAgainButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private GameObject winnerBlock;
    [SerializeField]
    private GameObject hexagonPrefab;
    [SerializeField]
    private Transform boardTransform;

    // Fields
    private TextMeshProUGUI winnerBlockText;
    private List<Hexagon> allHexagons;
    private bool bonusTurnActive;    
    private CurrentWord currentWordObjectOnScreen; 
    private List<string> listOfLettersPressed = new();
    private SpellCheck spellCheck = new();
    private bool team1Turn = true;
    
    // Properties
    public List<Hexagon> AllHexagons
    {
        get => allHexagons;
        set => allHexagons = value;
    }
    public bool BonusTurnActive
    {
        get => bonusTurnActive;
        set => bonusTurnActive = value;
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
    public bool Team1Turn
    {
        get => team1Turn;
        set => team1Turn = value;
    }        

    // Hexagon States
    public HexagonStates HomeTeam1 => homeTeam1;
    public HexagonStates HomeTeam2 => homeTeam2;
    public HexagonStates Invisible => invisible;
    public HexagonStates Neutral => neutral;
    public HexagonStates PressedTeam1 => pressedTeam1;
    public HexagonStates PressedTeam2 => pressedTeam2;
    public HexagonStates TerritoryTeam1 => territoryTeam1;
    public HexagonStates TerritoryTeam2 => territoryTeam2;


    // Class Methods

    void Awake() {
        InitializeHexagonsOnBoard(); 
        InitilizeComponents();
    }

    void Start() {
        InitializeColors();
        MakeAllHexagonsInvisible();
        SetHomeBases();
    }

    private void InitializeHexagonsOnBoard() {
        float boardCols = PlayerPrefs.GetInt("BoardCols", 7);
        float boardRows = PlayerPrefs.GetInt("BoardRows", 9);

        float x = 0;
        float y = 0;

        for (int i = 0; i < boardRows; i++) {
            if (i % 2 == 0) {
                //create short row
                for (int k = 1; k <= (int)Math.Floor((double)boardCols / 2); k++) {
                    x = k * Hexagon.HORIZONTALOFFSET * 2;
                    y = i * Hexagon.VERTICALOFFSET / 2;
                    Vector3 position = new(x - 350, y - 200);
                    CreateHexagon(position);
                }
            } else {
                //create long row
                for (int k = 1; k <= (int)Math.Ceiling((double)boardCols / 2); k++) {
                    x = k * Hexagon.HORIZONTALOFFSET * 2 - Hexagon.HORIZONTALOFFSET;
                    y = i * (Hexagon.VERTICALOFFSET / 2);
                    Vector3 position = new(x - 350, y - 200);
                    CreateHexagon(position);
                }
            }
        }    
    }

    private void CreateHexagon(Vector3 position) {
        GameObject newHexagonObject = Instantiate(hexagonPrefab, boardTransform);
        newHexagonObject.transform.SetLocalPositionAndRotation(position, Quaternion.identity);
        Hexagon newHexagon = newHexagonObject.GetComponent<Hexagon>();
        newHexagon.HexagonX = position.x;
        newHexagon.HexagonY = position.y;
    }

    public void PlayAgain() {
        winnerBlock.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        ClearLetters();
        MakeAllHexagonsInvisible();
        SetHomeBases();
    }

    private void ClearLetters() {
        foreach (Hexagon hex in AllHexagons) {
            hex.DeleteLetter();
        }
    }

    private void InitializeColors() {
        string[] colorKeys = {
            "PressedTeam1Color", "PressedTeam2Color",
            "HomeTeam1Color", "HomeTeam2Color",
            "TerritoryTeam1Color", "TerritoryTeam2Color"
        };

        foreach (string key in colorKeys)
        {
            string colorString = "#" + PlayerPrefs.GetString(key);

            if (ColorUtility.TryParseHtmlString(colorString, out Color parsedColor))
            {
                switch (key)
                {
                    case "PressedTeam1Color":
                        PressedTeam1.FillColor = parsedColor;
                        break;
                    case "PressedTeam2Color":
                        PressedTeam2.FillColor = parsedColor;
                        break;
                    case "HomeTeam1Color":
                        HomeTeam1.FillColor = parsedColor;
                        break;
                    case "HomeTeam2Color":
                        HomeTeam2.FillColor = parsedColor;
                        break;
                    case "TerritoryTeam1Color":
                        TerritoryTeam1.FillColor = parsedColor;
                        break;
                    case "TerritoryTeam2Color":
                        TerritoryTeam2.FillColor = parsedColor;
                        break;
                    default:
                        Debug.LogWarning($"Color key {key} not recognized.");
                        break;
                }
            }
        }
    }

    private void InitilizeComponents() {
        AllHexagons = GetComponentsInChildren<Hexagon>().ToList();
        spellCheck = new SpellCheck();
        CurrentWordObjectOnScreen = GetComponentInChildren<CurrentWord>();
        winnerBlockText = transform.Find("Winner Block").GetComponentInChildren<TextMeshProUGUI>();
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

    private void CheckBonusTurn() {
        if (BonusTurnActive == true) {
            ChangeTurn();
            BonusTurnActive = false;

        }
    }

    private void CheckHomesAreSet() {
        bool noHomeTeam1 = true;
        bool noHomeTeam2 = true;
        foreach (Hexagon hex in AllHexagons) {
            if (hex.HexagonCurrentState == "homeTeam1") {
                noHomeTeam1 = false;
            }
            if (hex.HexagonCurrentState == "homeTeam2") {
                noHomeTeam2 = false;
            }
        }
        if (noHomeTeam1 == true) {
            SetNewHome("homeTeam1");
        }
        if (noHomeTeam2 == true) {
            SetNewHome("homeTeam2");
        }
    }

    private void CheckBoardIsPlayable() {
        if (!SpellCheck.CanFormValidWord(AllHexagons)) {
            ShuffleLetters();
        }
    }

    private HexagonStates GetCurrentTeam() {
        if (Team1Turn) {
            return PressedTeam1;
        } else {
            return PressedTeam2;
        }
    }

    private string GetOpponentHomeState(string hexState) {
        return team1Turn ? "homeTeam2" : "homeTeam1";
    }

    public void HexagonPressed(Hexagon hex) {
        string hexState = hex.HexagonCurrentState;
        if (hexState == "neutral") {
            audioPressed.Play();
            CurrentWordUpdate(hex.HexagonText.text);
            hex.SetHexagonState(GetCurrentTeam());

        } else if (hexState == "pressedTeam1" || hexState == "pressedTeam2") {
            audioUnPressed.Play();
            CurrentWordRemove(hex.HexagonText.text);
            hex.SetHexagonState(Neutral);

        } else {
            //if state is home/territory/invisible then do nothing
        }
    }

    private bool IsHexagonPressed(Hexagon hex) {
        return hex.HexagonCurrentState == "pressedTeam1" || hex.HexagonCurrentState == "pressedTeam2";
    }

    private bool IsHomeState(string hexState) {
        return hexState == "homeTeam1" || hexState == "homeTeam2";
    }

    public void LoadMainMenu() {
        chooseSettingsNoise.Play();
        StartCoroutine(LoadSceneCoroutine("Main Menu Scene"));
    }

    private IEnumerator LoadSceneCoroutine(string scene) {
        yield return new WaitWhile(() => chooseSettingsNoise.isPlaying);
        SceneManager.LoadScene(scene);
    }

    private void MakeAllHexagonsInvisible(){
        foreach (Hexagon hex in AllHexagons){ 
            hex.SetHexagonState(Invisible);
        }
    }

    public void MakePressedHexagonsTerritory(Hexagon hex) {
        string hexState = hex.HexagonCurrentState;

        if (hexState == "territoryTeam1" || hexState == "homeTeam1") {
            ProcessHexagonTerritory(hex, "Team1");
        }
        else if (hexState == "territoryTeam2" || hexState == "homeTeam2") {
            ProcessHexagonTerritory(hex, "Team2");
        }
    }

    public void MakeTouchingHexagonsNeutral(List<Hexagon> touchingHexagonsArray) {
        foreach (Hexagon touchingHexagon in touchingHexagonsArray)
        {
            string hexState = touchingHexagon.HexagonCurrentState;

            if ((team1Turn && ShouldMakeNeutralForTeam1(hexState)) || (!team1Turn && ShouldMakeNeutralForTeam2(hexState)))
            {
                touchingHexagon.SetHexagonState(Neutral);

                if (IsHomeState(hexState))
                {
                    BonusTurnActive = true;
                    touchingHexagon.SetLetter();
                    SetNewHome(GetOpponentHomeState(hexState));
                }
            }
        }
    }

    private void ProcessValidWord() {
        foreach (Hexagon hex in AllHexagons)
        {
            MakePressedHexagonsTerritory(hex);
        }
        Letter.AddLetterToList(CurrentWordObjectOnScreen.CurrentWordText.text);
        ClearPressedHexagonsValidWord();
        ChangeTurn();
        ResetWordState();
        CheckBoardIsPlayable();
        CheckHomesAreSet();
        CheckBonusTurn();
        audioWordSubmit.Play();
    }

    private void ProcessInvalidWord() {
        ClearPressedHexagonsInvaildWord();
        ResetWordState();
        audioWordFailed.Play();
    }

    private void ProcessHexagonTerritory(Hexagon hex, string team) {
        List<Hexagon> touchingHexes = hex.FindTouchingHexagons();

        foreach (Hexagon touchingHex in touchingHexes)
        {
            string touchingHexState = touchingHex.HexagonCurrentState;

            if (touchingHexState == $"pressed{team}")
            {
                ProcessTouchingHex(touchingHex, team);
            }
        }
    }

    private void ProcessTouchingHex(Hexagon touchingHex, string team) {
        touchingHex.DeleteLetter();
        if (team == "Team1") {
            touchingHex.SetHexagonState(territoryTeam1);
        } else {
            touchingHex.SetHexagonState(territoryTeam2);
        }
        
        List<Hexagon> touchingHexagonsArray = touchingHex.FindTouchingHexagons();
        MakeTouchingHexagonsNeutral(touchingHexagonsArray);

        MakePressedHexagonsTerritory(touchingHex);
    }

    private void ResetWordState() {
        CurrentWordObjectOnScreen.UpdateCurrentWord("");
        ListOfLettersPressed.Clear();
    }

    private void SetHomeBases() {

        //the math only works if there are two more rows than columns in the board eg. 7 cols and 9 rows
        int boardCols = PlayerPrefs.GetInt("BoardCols");
        int base1, base2;

        int halfBoardColsRoundedUp = (int)Math.Ceiling((double)boardCols / 2);
        int halfBoardColsRoundedDown = (int)Math.Floor((double)boardCols / 2);
        int totalHexes = halfBoardColsRoundedUp * halfBoardColsRoundedUp * 2;

        base1 = boardCols + halfBoardColsRoundedDown;
        base2 = totalHexes - base1;

        AllHexagons[base1 - 1].SetHexagonState(HomeTeam1);
        AllHexagons[base2 - 1].SetHexagonState(HomeTeam2);
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

    private void ShuffleLetters() {
        foreach (Hexagon hex in AllHexagons) {
            if (hex.HexagonCurrentState == "neutral") {
                hex.SetLetter();
            }
        }
        Debug.Log("SHUFFLING");
        CheckBoardIsPlayable();
    }

    private bool ShouldMakeNeutralForTeam1(string hexState) {
        return hexState != "homeTeam1" && hexState != "territoryTeam1" && hexState != "pressedTeam1";
    }

    private bool ShouldMakeNeutralForTeam2(string hexState) {
        return hexState != "homeTeam2" && hexState != "territoryTeam2" && hexState != "pressedTeam2";
    }

    private void SetNewHome(string team) {
        Hexagon hex = SelectRandomHexagonOfType($"territory{team[4..]}");
        if (hex != null && !BonusTurnActive) {
            ChangeTurn();
            hex.SetHexagonState(team == "homeTeam2" ? HomeTeam2 : HomeTeam1);
            ChangeTurn(); // Changing turn before and after so that when the new home is set, it's that player's turn, so their home doesn't turn their territory neutral
        }
    }

    private Hexagon SelectRandomHexagonOfType(string state){
        List<Hexagon> targetHexes = AllHexagons.Where(hex => hex.HexagonCurrentState == state).ToList();
        if (targetHexes.Count == 0) {
            HasWon();
            return null;
        } else {
            return targetHexes[UnityEngine.Random.Range(0, targetHexes.Count)];
        }
    }

    private void HasWon() {
        if (!team1Turn) {
            winnerBlockText.text = "Team 1 has Won!";
        } else {
            winnerBlockText.text = "Team 2 has Won!";
        }
        audioVictory.Play();
        winnerBlock.SetActive(true);
        playAgainButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        ChangeTurn();
    }

    private void DeleteHexagons() {
        foreach (Hexagon hex in AllHexagons)
        {
           Destroy(hex.gameObject);
        }
        AllHexagons.Clear();
    }
}
