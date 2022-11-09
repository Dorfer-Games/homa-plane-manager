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
        Signals.Get<SignalFillZone>().AddListener(LadderOpen);
    }
    void LadderOpen(FillZoneComponent zone)
    {
        if (zone.gameObject == game.Airplane.LadderZone)
        {
            zone.gameObject.SetActive(false);

            if (!game.Airplane.IsLadderOpen)
            {
                game.Airplane.Ladder.DOLocalRotate(new Vector3(0f, 0f, game.Airplane.LadderRotate.y), ladderCooldown)
                    .OnComplete(() =>
                    {
                        game.Airplane.SetLadderStatus(true);

                        PeopleRun();
                    });
            } else PeopleRun();
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