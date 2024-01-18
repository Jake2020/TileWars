using NetSpell.SpellChecker;
using NetSpell.SpellChecker.Dictionary;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;

public class SpellCheck
{
    //Fields
    private readonly Spelling spelling;
    private readonly WordDictionary wordDictionary;
    private bool foundValidWord = false;


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

    private bool CanTheseLettersMakeAWord(List<string> letters) {

        IEnumerable<string> letterEnumerable = letters;
        DoPermutationsMakeWord(letterEnumerable, 5);

        return foundValidWord;
    }

    public bool CanFormValidWord(Hexagon[] AllHexagons) {

        List<string> letters = new();
        foreach (Hexagon hex in AllHexagons) {
            if (!string.IsNullOrWhiteSpace(hex.HexagonText.text) && hex.HexagonText.text != "*" ) {
                letters.Add(hex.HexagonText.text);
            }
        }

        bool wordIsPlayable = CanTheseLettersMakeAWord(letters);

        return wordIsPlayable;
    }

    private IEnumerable<IEnumerable<T>> DoPermutationsMakeWord<T>(IEnumerable<T> list, int length) {
        if (length == 1) {
            return list.Select(t => new T[] { t });
        }
        return DoPermutationsMakeWord(list, length - 1).SelectMany(t => list, (t1, t2) => t1.Concat(new T[] { t2 }));
    }
}


