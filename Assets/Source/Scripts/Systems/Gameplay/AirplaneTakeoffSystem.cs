using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using System.Linq;
using UnityEngine;

public class AirplaneTakeoffSystem : GameSystem
{
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
            foreach (var shelf in ShelfComponent.Hashset.ToList())
                if (shelf.PointList.Count > shelf.ItemList.Count) return;

            if (game.PeopleOnPlaneList.Count > 0) return;
            if (game.Player.transform.position.y < game.Airplane.LadderRaiseZone.transform.position.y - 0.25f ||
                game.Player.transform.position.x > game.Airplane.Ladder.position.x - 0.25f) return;

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

                game.Ground.transform.DOLocalMoveY(-height, cooldown)
                    .OnComplete(() =>
                    {
                        Signals.Get<AirplaneStateSignal>().Dispatch(AirplaneState.Flight);
                    });
            });
    }
}
