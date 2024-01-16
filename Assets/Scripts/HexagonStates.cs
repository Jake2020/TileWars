using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class HexagonStates
{
    // Fields
    [SerializeField]
    private Color fillColor;

    [SerializeField]
    private string stateName;

    // Properties
    public Color FillColor => fillColor;

    public string StateName => stateName; 
}