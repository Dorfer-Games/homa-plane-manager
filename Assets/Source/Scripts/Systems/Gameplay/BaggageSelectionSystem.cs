using DG.Tweening;
using Kuhpik;
using MoreMountains.NiceVibrations;
using NaughtyAttributes;
using Supyrb;
using System.Collections.Generic;
using UnityEngine;

public class BaggageSelectionSystem : GameSystem
{
    [SerializeField, BoxGroup("Developer")] [Tag] string baggageTag;

    public override void OnInit()
    {
        game.PlayerItemList = new List<ItemComponent>();

        game.Player.Trigger.OnTriggerEnterEvent += TriggerEnterCheck;
    }
    void TriggerEnterCheck(Transform other, Transform original)
    {
        if (other.CompareTag(baggageTag) && game.PeoplePlatformList.Count <= 0)
        {
            other.tag = "Untagged";

            other.parent = game.Player.StackPoint;
            if (game.PlayerItemList.Count > 0) other.parent = game.PlayerItemList[game.PlayerItemList.Count - 1].StackPoint;

            ItemComponent component = other.GetComponent<ItemComponent>();
            game.PlayerItemList.Add(component);

            Sequence mySeq = Extensions.MoveItem(component, game.PlayerItemList.Count, 0.25f, 1f, 1f, new Vector3(0f, 90f, 0f));
            mySeq.OnComplete(() =>
            {
                DOTween.Kill(component.transform);
            });

            Signals.Get<VibrationSignal>().Dispatch(HapticTypes.LightImpact);
        }
    }
}
