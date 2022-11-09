using System;
using UnityEngine;

[Serializable]
public class PeopleData
{
    public Transform Transform;
    public CharacterComponent Component;

    public PlaceComponent Place;

    public int Stage;
    public Transform Target;
}