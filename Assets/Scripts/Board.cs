using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Hexagon[] allHexagons;
    // Start is called before the first frame update
    void Awake()
    {
        allHexagons = GetComponentsInChildren<Hexagon>();
        allHexagons[0].InitialiseHome();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
