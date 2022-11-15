using System;
using UnityEngine;

[Serializable]
public class BubbleUITransferData
{
    public BubbleUIType Type;
    public Transform Target;

    public ItemType FoodType;
    public int Amount;
}