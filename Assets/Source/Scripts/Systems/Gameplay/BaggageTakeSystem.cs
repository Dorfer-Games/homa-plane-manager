using DG.Tweening;
using Kuhpik;
using MoreMountains.NiceVibrations;
using NaughtyAttributes;
using Supyrb;
using UnityEngine;

public class BaggageTakeSystem : GameSystem
{
    [SerializeField, BoxGroup("Developer")] [Tag] string shelfTag;

    public override void OnInit()
    {
        game.Player.Trigger.OnTriggerEnterEvent += TriggerEnterCheck;
    }

    void TriggerEnterCheck(Transform other, Transform original)
    {
        if (other.CompareTag(shelfTag) && game.BaggageList.Count > 0 && game.PeoplePlatformList.Count > 0)
        {
            foreach (var item in game.BaggageList)
            {
                item.transform.parent = game.Player.StackPoint;
                if (game.PlayerItemList.Count > 0) item.transform.parent = game.PlayerItemList[game.PlayerItemList.Count - 1].StackPoint;

                game.PlayerItemList.Add(item);

                Sequence mySeq = Extensions.MoveItem(item, game.PlayerItemList.Count, 0.3f, 1f, 1f, new Vector3(0f, 90f, 0f));
                mySeq.OnComplete(() =>
                {
                    DOTween.Kill(item.transform);
                });

                Signals.Get<VibrationSignal>().Dispatch(HapticTypes.LightImpact);
            }

            game.BaggageList.Clear();
        }
    }
}