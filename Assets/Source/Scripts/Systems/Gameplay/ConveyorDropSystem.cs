using DG.Tweening;
using Kuhpik;
using MoreMountains.NiceVibrations;
using NaughtyAttributes;
using Supyrb;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorDropSystem : GameSystem
{
    [SerializeField, BoxGroup("Developer")] [Tag] string conveyorTag;

    public override void OnInit()
    {
        game.Conveyor = FindObjectOfType<ConveyorComponent>();

        game.Player.Trigger.OnTriggerEnterEvent += TriggerEnterCheck;

        game.ConveyorList = new List<ItemComponent>();
    }
    void TriggerEnterCheck(Transform other, Transform original)
    {
        if (other.CompareTag(conveyorTag) && game.PlayerItemList.Count > 0 && game.PeoplePlatformList.Count > 0)
        {
            for (int i = game.PlayerItemList.Count - 1; i >= 0; i--)
            {
                if (game.PlayerItemList[i].ItemType == ItemType.Baggage)
                {
                    ItemComponent item = game.PlayerItemList[i];

                    item.transform.parent = game.Conveyor.BaggagePointList[0];
                    if (game.ConveyorList.Count > 0) item.transform.parent = game.ConveyorList[game.ConveyorList.Count - 1].StackPoint;

                    Extensions.StackSorting(game.Player.StackPoint, game.PlayerItemList, i);

                    Sequence mySeq = Extensions.MoveItem(item, Random.Range(5, 10), 0.3f, 1f, 1f, Vector3.zero);
                    mySeq.OnComplete(() =>
                    {
                        DOTween.Kill(item.transform);
                    });
                    game.ConveyorList.Add(item);

                    Signals.Get<VibrationSignal>().Dispatch(HapticTypes.LightImpact);
                }
            }

            Signals.Get<AirplaneStateSignal>().Dispatch(AirplaneState.Ready);
        } 
    }
}
