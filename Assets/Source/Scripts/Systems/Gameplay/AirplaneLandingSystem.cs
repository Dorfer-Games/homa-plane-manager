using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using UnityEngine;

public class AirplaneLandingSystem : GameSystem
{
    [SerializeField, BoxGroup("Developer")] float cooldown = 3f;

    public override void OnInit()
    {
        Signals.Get<AirplaneStateSignal>().AddListener(AirplaneLanding);
    }
    void AirplaneLanding(AirplaneState state)
    {
        if (state != AirplaneState.Landing) return;

        game.Ground.transform.DOLocalMoveY(0, cooldown)
            .OnComplete(() =>
            {
                game.Airplane.LadderRaiseZone.SetActive(true);
            });
    }
}