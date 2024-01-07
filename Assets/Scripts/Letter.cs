using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public static class Letter
{
    //list of all letters to be pulled from to put a letter in a hex
private static readonly char[] Letters =
    {
        'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a',
        'b', 'b',
        'c', 'c',
        'd', 'd', 'd', 'd',
        'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e',
        'f', 'f',
        'g', 'g', 'g',
        'h', 'h',
        'i', 'i', 'i', 'i', 'i', 'i', 'i', 'i', 'i',
        'j',
        'k',
        'l', 'l', 'l', 'l',
        'm', 'm',
        'n', 'n', 'n', 'n', 'n', 'n',
        'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o',
        'p', 'p',
        'q',
        'r', 'r', 'r', 'r', 'r', 'r',
        's', 's', 's', 's',
        't', 't', 't', 't', 't', 't',
        'u', 'u', 'u', 'u',
        'v', 'v',
        'w', 'w',
        'x',
        'y', 'y',
        'z'
    };
    public static char GenerateLetter(){ //pick random letter
        return Letters[Random.Range(0, Letters.Length)];
    }
}
