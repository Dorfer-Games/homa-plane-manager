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
        game.Ground.transform.DOLocalMove(game.Ground.transform.localPosition, 3f)
            .OnComplete(() =>
            {
                Signals.Get<ControllerChangeSignal>().Dispatch(ControllerType.Airplane);
                Signals.Get<EffectSignal>().Dispatch(Camera.main.transform, EffectType.Camera, new Vector3(cooldown / 2.2f, 0f, 0f));
                game.Ground.gameObject.SetActive(true);

                game.Ground.transform.DOLocalMove(position, cooldown_2)
                   .OnComplete(() =>
                   {
                       game.Ground.transform.DOLocalMoveZ(0, cooldown_1).SetEase(Ease.OutExpo)
                            .OnComplete(() =>
                            {
                                game.Airplane.LadderRaiseZone.SetActive(true);

                                Signals.Get<AirplaneStateSignal>().Dispatch(AirplaneState.Idle);
                                Signals.Get<ControllerChangeSignal>().Dispatch(ControllerType.Player);
                            });
                   });
            });
    }
}