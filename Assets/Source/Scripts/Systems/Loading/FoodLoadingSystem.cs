using Kuhpik;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class FoodLoadingSystem : GameSystem
{
    [SerializeField, BoxGroup("Developer")] List<FoodData> foodList;

    public override void OnInit()
    {
        game.FoodList = foodList;
    }
}