using DG.Tweening;
using Kuhpik;
using MoreMountains.NiceVibrations;
using NaughtyAttributes;
using Supyrb;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneLadderSystem : GameSystem
{
    [SerializeField, BoxGroup("Developer")] float ladderCooldown = 1f;

    public override void OnInit()
    {
        Signals.Get<AirplaneStateSignal>().AddListener(LadderZoneCheck);
        Signals.Get<SignalFillZone>().AddListener(LadderAction);

        game.LadderCooldown = ladderCooldown;

        game.Airplane.LadderLowerZone.SetActive(true);
        game.Airplane.LadderRaiseZone.SetActive(false);
        game.Airplane.DoorCollider.enabled = true;
    }
    void LadderAction(FillZoneComponent zone)
    {
        if (zone.gameObject == game.Airplane.LadderLowerZone)
        {
            zone.gameObject.SetActive(false);

            if (!game.Airplane.IsLadderOpen) LadderOpen(game.PeoplePlatformList, game.PeopleOnPlaneList);
            else PeopleRun(game.PeoplePlatformList, game.PeopleOnPlaneList);

            Signals.Get<VibrationSignal>().Dispatch(HapticTypes.MediumImpact);
        }

        if (zone.gameObject == game.Airplane.LadderRaiseZone)
        {
            zone.gameObject.SetActive(false);

            if (!game.Airplane.IsLadderOpen) LadderOpen(game.PeoplePlaneList, game.PeopleFromPlaneList);

            Signals.Get<VibrationSignal>().Dispatch(HapticTypes.MediumImpact);
        }
    }
    void LadderOpen(List<PeopleData> from, List<PeopleData> to)
    {
        game.Airplane.BaggageDoor.DOLocalRotate(new Vector3(0f, game.Airplane.BaggageDoorRotate.y, 0f), ladderCooldown);

        game.Airplane.Ladder.DOLocalRotate(new Vector3(0f, 0f, game.Airplane.LadderRotate.y), ladderCooldown)
              .OnComplete(() =>
              {
                  game.Airplane.DoorCollider.enabled = false;
                  game.Airplane.SetLadderStatus(true);

                  PeopleRun(from, to);
              });
    }

    void PeopleRun(List<PeopleData> from, List<PeopleData> to)
    {
        Signals.Get<NavigationUpdateSignal>().Dispatch();

        foreach (var people in from)
        {
            people.Component.Agent.enabled = true;
            to.Add(people);
        }

        from.Clear();
    }
    void LadderZoneCheck(AirplaneState state)
    {
        if (state != AirplaneState.Ready) return;

        game.Airplane.LadderLowerZone.SetActive(true);
    }
}