using System;
using UnityEngine;

[Serializable]
public class PeopleData
{
    public Transform Transform;
    public CharacterComponent Component;

    public PlaceBlockComponent PlaceBlock;
    public PlaceComponent Place;
    public ItemComponent Baggage;

    public int Stage;
    public Transform Target;

    public bool IsFood;
    public bool IsFoodReady;
    public ItemType FoodType;
    public int FoodAmount;
}