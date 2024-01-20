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
        List<string> permutationsEnumerable = GetPermutations(letterEnumerable, 4)
            .Select(perm => string.Join("", perm))
            .ToList();

        foreach (string perm in permutationsEnumerable)
        {
            if (IsValidWord(perm))
            {
                Debug.Log(perm);
                return true;
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

        bool wordIsPlayable = CanTheseLettersMakeAWord(letters);

        return wordIsPlayable;
    }

    private IEnumerable<IEnumerable<T>> GetPermutationsWithRept<T>(IEnumerable<T> list, int length) {
        if (length == 1) return list.Select(t => new T[] { t });
        return GetPermutationsWithRept(list, length - 1)
            .SelectMany(t => list, 
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    private IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length) {
        if (length == 1) {
            return list.Select(t => new T[] { t });
        }
        return GetPermutations(list, length - 1).SelectMany(t => list.Where(o => !t.Contains(o)), (t1, t2) => t1.Concat(new T[] { t2 }));
    }
}


