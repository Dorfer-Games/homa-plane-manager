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
using TMPro;

public class FinishPanel : GameSystem
{
    public override void OnInit()
    {
        GameObject.Find("RatryButton").GetComponent<Button>().onClick.AddListener(Restart);
        GameObject.Find("NextButton").GetComponent<Button>().onClick.AddListener(Restart);

        if (game.IsVictory)
        {
            GameObject.Find("FailPanel").SetActive(false);
            GameObject.Find("WinPanel").GetComponent<Animation>().Play();
            player.Level++;
        }
        else
        {
            GameObject.Find("WinPanel").SetActive(false);
            GameObject.Find("FailPanel").GetComponent<Animation>().Play();
        }
    }
    public void Restart()
    {
        Bootstrap.Instance.GameRestart(0);
    }
}
