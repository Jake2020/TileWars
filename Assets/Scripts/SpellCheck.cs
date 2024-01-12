using NetSpell.SpellChecker;
using NetSpell.SpellChecker.Dictionary;
using UnityEngine;

public class SpellCheck
{
    private readonly Spelling spelling;
    private readonly WordDictionary wordDictionary;

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

    public bool IsValidWord(string word)
    {
        return word.Length > 2 && spelling.TestWord(word);
    }
}
