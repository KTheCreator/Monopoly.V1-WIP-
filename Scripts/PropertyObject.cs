using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Property Object")]
public class PropertyObject:ScriptableObject
{
    public string propertyName;
    public setColour setColour;
    public int propertyValue;
    public string whoOwns = null;
    public costOfBuilding costOfBuilding;
    public int[] tierPrices;
    public int costWithHotels;
    public int mortgage;
    public int unmortgage;

    
}
public enum setColour
{
    Brown,
    Light_Blue,
    Pink,
    Orange,
    Red,
    Yellow,
    Green, 
    Dark_Blue

}
public enum costOfBuilding
{
    Fifty = 50,
    Hunna=100,
    HunnaFifty=150,
    TwoHunna = 200
}
