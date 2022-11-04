using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.ComponentModel.Design;
using System;
using NaughtyAttributes;

public class PlayerGameData : GameSystem
{
    [SerializeField] PlayerData PlayerData;
    [SerializeField] GameData GameData;

    [Button]
    void Save()
    {
        Bootstrap.Instance.SaveGame();
    }
}
