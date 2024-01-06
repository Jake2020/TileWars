using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public static class Letter
{
    //list of all letters to be pulled from to put a letter in a hex
    private static readonly char[] Letters = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};

    public static char GenerateLetter(){ //pick random letter
        return Letters[Random.Range(0, Letters.Length)];
    }
}
