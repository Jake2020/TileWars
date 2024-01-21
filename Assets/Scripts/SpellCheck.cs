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
        return word.Length > 2 && spelling.TestWord(word.ToUpper());
    }

    private bool CanTheseLettersMakeAWord(List<string> letters) {
        IEnumerable<string> letterEnumerable = letters;

        for (int length = 3; length <= 5; length++) {
            List<string> permutations = GetPermutationsWithDuplicates(letterEnumerable, length).Select(perm => string.Join("", perm)).ToList();

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
    
    private IEnumerable<IEnumerable<T>> GetPermutationsWithDuplicates<T>(IEnumerable<T> list, int length) {

        Dictionary<T, int> elementCounts = list.GroupBy(e => e).ToDictionary(g => g.Key, g => g.Count());

        IEnumerable<IEnumerable<T>> GetPermutationsInternal(int remainingLength) {
            if (remainingLength == 1) {
                return list.Select(e => new T[] { e });
            }

            return GetPermutationsInternal(remainingLength - 1)
                .SelectMany(partialPermutation =>
                    elementCounts
                        .Where(kv => kv.Value > partialPermutation.Count(e => EqualityComparer<T>.Default.Equals(kv.Key, e)))
                        .Select(kv => partialPermutation.Concat(new T[] { kv.Key })));
        }

        return GetPermutationsInternal(length);
    }
}


