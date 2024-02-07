using System;
using System.Collections.Generic;
using System.Linq;

public static class Letter
{
    private static readonly Random random = new();

    private static readonly List<char> Letters = new()
    {
        'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a',
        'b', 'b', 'b',
        'c', 'c',
        'd', 'd', 'd', 'd',
        'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e',
        'f', 'f',
        'g', 'g', 'g', 'g',
        'h', 'h', 'h',
        'i', 'i', 'i', 'i', 'i', 'i', 'i',
        'j',
        'k',
        'l', 'l', 'l', 'l',
        'm', 'm',
        'n', 'n', 'n', 'n', 'n', 'n',
        'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o',
        'p', 'p', 'p', 'p',
        'q',
        'r', 'r', 'r', 'r', 'r', 'r',
        's', 's', 's', 's', 's', 's',
        't', 't', 't', 't', 't', 't', 't', 't',
        'u', 'u', 'u',
        'v',
        'w', 'w',
        'x',
        'y', 'y',
        'z'
    };

    public static void AddLetterToList(string word) {
        List<char> charList = word.ToCharArray().ToList();
        foreach (char letter in charList) {
            Letters.Add(letter);
        }
    }

    public static void DeleteLetterFromList(char letter) {
        for (int i = 0; i < Letters.Count; i++) {
            if (Letters[i] == letter) {
                Letters.RemoveAt(i);
                return;
            }
        }
    }

    public static string GenerateLetter() {
        char newLetter = Letters[random.Next(0, Letters.Count)];
        DeleteLetterFromList(newLetter);
        return newLetter.ToString();
    }
}
