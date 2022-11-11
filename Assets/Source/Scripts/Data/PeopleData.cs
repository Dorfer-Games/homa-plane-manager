using System;
using UnityEngine;

[Serializable]
public class PeopleData
{
    public Transform Transform;
    public CharacterComponent Component;

    public PlaceComponent Place;
    public ItemComponent Baggage;

    public int Stage;
    public Transform Target;

    public bool IsFood;
    public ItemType FoodType;
    public int FoodAmount;
}