using Kuhpik;
using static My;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;
using Supyrb;

public class FinalizeLoading : GameSystem
{
    [SerializeField] Material UIGrayMat;
    public override void OnInit()
    {
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "session_" + player.SessionCounter.ToString());
        //player.SessionCounter++;

        Signals.Clear();
        FindObjectOfType<Cheats>(true).PreStart();
        GrayMaterials();

        game.TutorialArrow = FindObjectOfType<TutorialArrowComponent>(true);
        game.TutorialArrow2D = FindObjectOfType<TutorialArrow2DComponent>(true);
        PlayerSet();
        //FindObjectOfType<TutorialSystem>().Begin();

        //player.Resources ??= new List<int>();

        FirstLaunch();
    }
    void PlayerSet()
    {
        game.PlayerOld = FindObjectOfType<PlayerComponent>();
        
    }
    private void FirstLaunch()
    {
        if (!player.FirstLaunch)
        {
            player.FirstLaunch = true;

            player.VibroOn = true;
            player.SoundOn = true;
            player.MusicOn = true;
            player.Ads = true;

            player.Speed = config.PlayerSpeed;

         /*   game.Event("first_game_launch");
            game.Event("level_1_started");
            game.Event("tutorial_started");
            game.AMEvent("first_game_launch");

            FirebaseAnalytics.LogEvent("first_game_launch");*/
        }
    }

    private void GrayMaterials()
    {
        foreach (var button in FindObjectsOfType<Button>(true))
            foreach (var image in button.GetComponentsInChildren<Image>(true))
                if (image.material == UIGrayMat)
                    image.material = new Material(UIGrayMat);
    }
}
