using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using UnityEngine;

public class AirplaneLadderSystem : GameSystem
{
    [SerializeField, BoxGroup("Developer")] float ladderCooldown = 1f;

    public override void OnInit()
    {
        Signals.Get<SignalFillZone>().AddListener(LadderAction);

        game.Airplane.LadderLowerZone.SetActive(true);
        game.Airplane.LadderRaiseZone.SetActive(false);
        game.Airplane.DoorCollider.enabled = true;
    }
    void LadderAction(FillZoneComponent zone)
    {
        if (zone.gameObject == game.Airplane.LadderLowerZone)
        {
            zone.gameObject.SetActive(false);

            if (!game.Airplane.IsLadderOpen)
            {
                game.Airplane.Ladder.DOLocalRotate(new Vector3(0f, 0f, game.Airplane.LadderRotate.y), ladderCooldown)
                    .OnComplete(() =>
                    {
                        game.Airplane.DoorCollider.enabled = false;
                        game.Airplane.SetLadderStatus(true);

                        PeopleRun();
                    });
            } else PeopleRun();
        }

        if (zone.gameObject == game.Airplane.LadderRaiseZone)
        {
            zone.gameObject.SetActive(false);

            if (game.Airplane.IsLadderOpen)
            {
                game.Airplane.DoorCollider.enabled = true;

                game.Airplane.Ladder.DOLocalRotate(new Vector3(0f, 0f, game.Airplane.LadderRotate.x), ladderCooldown)
                    .OnComplete(() =>
                    {
                        game.Airplane.SetLadderStatus(false);

                        Signals.Get<AirplaneStateSignal>().Dispatch(AirplaneState.Takeoff);
                    });
            }
        }
    }
    void PeopleRun()
    {
        Signals.Get<NavigationUpdateSignal>().Dispatch();

        foreach (var people in game.PeoplePlatformList)
            game.PeopleOnPlaneList.Add(people);

        game.PeoplePlatformList.Clear();
    }
}