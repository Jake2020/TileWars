using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetSpell;

public class SpellCheck
{
    NetSpell.SpellChecker.Dictionary.WordDictionary oDict;
    public bool IsWord(string word){
        NetSpell.SpellChecker.Spelling oSpell = new NetSpell.SpellChecker.Spelling(); 

        oSpell.Dictionary = oDict; 
        return oSpell.TestWord(word);
    }

    public SpellCheck()
    {
        NetSpell.SpellChecker.Dictionary.WordDictionary oDict = new NetSpell.SpellChecker.Dictionary.WordDictionary();
        oDict.DictionaryFile = "C:/Users/benhu/Documents/GitHub/TileWars/Assets/Packages/NetSpell.2.1.7/dic/en-GB.dic";
        oDict.Initialize();
    }

}
