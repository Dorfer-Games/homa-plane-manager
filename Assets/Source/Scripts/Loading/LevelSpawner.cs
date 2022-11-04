using Kuhpik;
using static My;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;
using Random = UnityEngine.Random;

public class LevelSpawner : GameSystem
{
    [SerializeField] int startLevel = -1;
    [SerializeField] int totalLevelsCount = 2;

    public override void OnInit()
    {
        string path = "Level_" + (startLevel >= 0 ? startLevel.ToString() : (player.Level % totalLevelsCount + 1).ToString());
        Instantiate(Resources.Load<GameObject>(path));
    }
}
