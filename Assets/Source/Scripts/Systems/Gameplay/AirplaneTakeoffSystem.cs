using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using System.Linq;
using UnityEngine;

public class AirplaneTakeoffSystem : GameSystem
{
    [SerializeField, BoxGroup("Developer")] Vector2 length;
    [SerializeField, BoxGroup("Developer")] float height = 10f;
    [SerializeField, BoxGroup("Developer")] float cooldown = 3f;

    bool isCheck;
    public override void OnInit()
    {
        game.Ground = FindObjectOfType<GroundComponent>();
    }
    public override void OnUpdate()
    {
        if (game.PeopleOnPlaneList.Count > 0 && !isCheck) isCheck = true;

        if (isCheck)
        {
            if (game.PeopleOnPlaneList.Count > 0 || game.BaggageList.Count < game.PeoplePlaneList.Count) return;
            if (game.Player.transform.position.y < game.Airplane.LadderRaiseZone.transform.position.y - 0.25f ||
                game.Player.transform.position.x > game.Airplane.Ladder.position.x - 1f) return;

            isCheck = false;
            AirplaneTakeoff();
        }
    }
    void AirplaneTakeoff()
    {
        game.Airplane.DoorCollider.enabled = true;
        
        game.Airplane.Ladder.DOLocalRotate(new Vector3(0f, 0f, game.Airplane.LadderRotate.x), game.LadderCooldown)
            .OnComplete(() =>
            {
                game.Airplane.SetLadderStatus(false);

                float percent = length.x * 100f / length.y;
                float cooldown_1 = cooldown * percent / 100f;
                float cooldown_2 = cooldown - cooldown_1;

                game.Ground.transform.DOLocalMoveZ(-length.x, cooldown_1).SetEase(Ease.InExpo)
                    .OnComplete(() =>
                    {
                         Vector3 position = new Vector3(game.Ground.transform.position.x, -height, -length.y);
                         game.Ground.transform.DOLocalMove(position, cooldown_2)
                            .OnComplete(() =>
                            {
                                Signals.Get<AirplaneStateSignal>().Dispatch(AirplaneState.Flight);
                                Signals.Get<ControllerChangeSignal>().Dispatch(ControllerType.Player);
                                game.Ground.gameObject.SetActive(false);
                            });

                        Signals.Get<EffectSignal>().Dispatch(Camera.main.transform, EffectType.Camera, new Vector3(cooldown_2 * 1.2f, 0f, 0f));
                    });
            });

        game.Airplane.BaggageZone.SetActive(false);
        game.Airplane.BaggageDoor.DOLocalRotate(new Vector3(0f, 0f, game.Airplane.BaggageDoorRotate.x), game.LadderCooldown);

        foreach (var item in game.BaggageList)
            item.Model.gameObject.SetActive(false);

        Signals.Get<ControllerChangeSignal>().Dispatch(ControllerType.Airplane);
        Signals.Get<AirplaneStateSignal>().Dispatch(AirplaneState.Takeoff);
    }
}
