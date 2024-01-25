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
    public Color FillColor 
    {
        get => fillColor;
        set => fillColor = value;
    }

    public string StateName
    {
        get => stateName;
        set => stateName = value;

    } 
}
