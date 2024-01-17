using NetSpell.SpellChecker;
using NetSpell.SpellChecker.Dictionary;
using UnityEngine;
using System.Collections.Generic;

public class SpellCheck
{
    //Fields
    private readonly Spelling spelling;
    private readonly WordDictionary wordDictionary;

    //Constructor
    public SpellCheck()
    {
        wordDictionary = new WordDictionary
        {
            DictionaryFile = Application.dataPath + "/Packages/NetSpell.2.1.7/dic/en-GB.dic"
        };
        wordDictionary.Initialize();

        spelling = new Spelling
        {
            Dictionary = wordDictionary
        };
    }

    //Class Methods
    public bool IsValidWord(string word)
    {
        return word.Length > 2 && spelling.TestWord(word);
    }

    public bool CanFormValidWord(Hexagon[] AllHexagons) {
        List<string> letters = new();
        foreach (Hexagon hex in AllHexagons) {
            letters.Add(hex.HexagonText.text);
        }
        
        return true;
    }
}
