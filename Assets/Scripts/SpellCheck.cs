using NetSpell.SpellChecker;
using NetSpell.SpellChecker.Dictionary;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine.Events;

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
            DictionaryFile = Application.dataPath + "/Packages/NetSpell.2.1.7/dic/en-US.dic"
        };
        wordDictionary.Initialize();

        spelling = new Spelling
        {
            Dictionary = wordDictionary
        };
    }

    //Class Methods
    public bool IsValidWord(string word) {
        return word.Length > 2 && spelling.TestWord(word);
    }

    private bool CanTheseLettersMakeAWord(List<string> letters) {
        IEnumerable<string> letterEnumerable = letters;

        for (int length = 3; length <= 5; length++) {
            List<string> permutations = GetPermutations(letterEnumerable, length).Select(perm => string.Join("", perm)).ToList();

            foreach (string potentialWord in permutations) {
                if (IsValidWord(potentialWord)) {
                    Debug.Log(potentialWord);
                    return true;
                }
            }
        }
        return false;
    }

    public bool CanFormValidWord(Hexagon[] AllHexagons) {
        List<string> letters = new();
        foreach (Hexagon hex in AllHexagons) {
            if (!string.IsNullOrWhiteSpace(hex.HexagonText.text) && hex.HexagonText.text != "*" ) {
                letters.Add(hex.HexagonText.text);
            }
        }
        return CanTheseLettersMakeAWord(letters);
    }
    
    private IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length) {
        if (length == 1) {
            return list.Select(t => new T[] { t });
        }
        return GetPermutations(list, length - 1).SelectMany(t => list.Where(o => !t.Contains(o)), (t1, t2) => t1.Concat(new T[] { t2 }));
    }
}


