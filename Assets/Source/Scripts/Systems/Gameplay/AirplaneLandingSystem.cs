using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using UnityEngine;

public class AirplaneLandingSystem : GameSystem
{
    [SerializeField, BoxGroup("Developer")] Vector2 length;
    [SerializeField, BoxGroup("Developer")] float cooldown = 3f;

    public override void OnInit()
    {
        Signals.Get<AirplaneStateSignal>().AddListener(AirplaneLanding);
    }
    void AirplaneLanding(AirplaneState state)
    {
        if (state != AirplaneState.Landing) return;

        float percent = length.x * 100f / length.y;
        float cooldown_1 = cooldown * percent / 100f;
        float cooldown_2 = cooldown - cooldown_1;

        Vector3 position = new Vector3(game.Ground.transform.position.x, game.Ground.transform.position.y, length.y);
        game.Ground.transform.position = position;

        position = new Vector3(game.Ground.transform.position.x, 0f, length.x);
        game.Ground.transform.DOLocalMove(position, cooldown_2)
           .OnComplete(() =>
           {
               game.Ground.transform.DOLocalMoveZ(0, cooldown_1)
                    .OnComplete(() =>
                    {
                        game.Airplane.LadderRaiseZone.SetActive(true);
                    });
           });
    }
}