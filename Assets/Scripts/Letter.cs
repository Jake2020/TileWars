using System;
using System.Collections.Generic;

public static class Letter
{
    private static readonly Random random = new Random();

    private static readonly List<char> Letters = new List<char>
    {
        'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a',
        'b', 'b', 'b',
        'c', 'c',
        'd', 'd', 'd', 'd',
        'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e',
        'f', 'f',
        'g', 'g', 'g', 'g',
        'h', 'h', 'h',
        'i', 'i', 'i', 'i', 'i', 'i', 'i', 'i', 'i',
        'j',
        'k',
        'l', 'l', 'l', 'l',
        'm', 'm',
        'n', 'n', 'n', 'n', 'n', 'n',
        'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o',
        'p', 'p', 'p',
        'q',
        'r', 'r', 'r', 'r', 'r', 'r',
        's', 's', 's', 's', 's',
        't', 't', 't', 't', 't', 't', 't',
        'u', 'u', 'u',
        'v',
        'w', 'w',
        'x',
        'y', 'y',
        'z'
    };

    public static string GenerateLetter()
    {
        return Letters[random.Next(0, Letters.Count)].ToString();
    }
}
