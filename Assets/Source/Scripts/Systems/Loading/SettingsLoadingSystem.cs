using DG.Tweening;
using HomaGames.HomaBelly;
using Kuhpik;
using MoreMountains.NiceVibrations;
using NaughtyAttributes;
using Supyrb;
using System.Collections;
using UnityEngine;

public class SettingsLoadingSystem : GameSystemWithScreen<SettingsUIScreen>
{
    [SerializeField, BoxGroup("Developer")] float cooldownVibration = 0.1f;

    bool isHide;
    float time = 0.25f;
    bool isVibration = true;
    public override void OnInit()
    {
        Signals.Get<VibrationSignal>().AddListener(Vibration);

        screen.SettingsButton.onClick.AddListener(() => SettingsHide());
        screen.VibrationsButton.onClick.AddListener(() => VibrationsActive());

        if (!player.IsGameLaunch) StartCoroutine(GameLaunch());

        SettingsHide();
        VibrationsUpdate();
    }
    void Vibration(HapticTypes type)
    {
        if (!player.IsVibration) return;

        if (isVibration)
        {
            MMVibrationManager.Haptic(type);
            isVibration = false;

            StartCoroutine(Cooldown());
        }
    }
    void SettingsHide()
    {
        screen.SettingsBar.transform.DORewind();

        if (isHide)
        {
            screen.SettingsBar.SetActive(isHide);
            screen.SettingsBar.transform.DOScaleY(1f, time);
        }
        else
        {
            screen.SettingsBar.transform.DOScaleY(0f, time)
                .OnComplete(() => screen.SettingsBar.SetActive(isHide));
        }

        isHide = !isHide;
    }
    void VibrationsActive()
    {
        player.IsVibration = !player.IsVibration;
        Bootstrap.Instance.SaveGame();

        VibrationsUpdate();
    }
    void VibrationsUpdate()
    {
        if (!player.IsVibration) screen.VibrationsImage.sprite = screen.VibrationsSprite[0];
        else screen.VibrationsImage.sprite = screen.VibrationsSprite[1];
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownVibration);

        isVibration = true;
    }
    IEnumerator GameLaunch()
    {
        VibrationsActive();
        player.IsGameLaunch = true;
        player.GameLevel = 1;

        yield return new WaitForSeconds(1f);

        HomaBelly.Instance.TrackDesignEvent("first_game_launch");
        HomaBelly.Instance.TrackDesignEvent("tutorial_started");
    }
}