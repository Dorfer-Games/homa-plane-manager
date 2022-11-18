using DG.Tweening;
using Kuhpik;
using MoreMountains.NiceVibrations;
using NaughtyAttributes;
using Supyrb;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaggageDropSystem : GameSystem
{
    [SerializeField, BoxGroup("Developer")] [Tag] string shelfTag;

    public override void OnInit()
    {
        game.Player.Trigger.OnTriggerEnterEvent += TriggerEnterCheck;

        game.BaggageList = new List<ItemComponent>();
    }
    void TriggerEnterCheck(Transform other, Transform original)
    {
        if (other.CompareTag(shelfTag) && game.PlayerItemList.Count > 0 && game.PeoplePlatformList.Count <= 0)
        {
            for (int j = game.PlayerItemList.Count - 1; j >= 0; j--)
            {
                if (game.PlayerItemList[j].ItemType == ItemType.Baggage)
                {
                    ItemComponent item = game.PlayerItemList[j];

                    float moveTime = 0.5f;
                    int pointID = game.BaggageList.Count;
                    while (pointID > game.Airplane.BaggagePointList.Count - 1) pointID -= game.Airplane.BaggagePointList.Count;
                    item.transform.parent = game.Airplane.BaggagePointList[pointID];

                    int count = Mathf.FloorToInt(game.BaggageList.Count / game.Airplane.BaggagePointList.Count);
                    //Vector3 newRotate = new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), 0f);

                    Sequence mySeq = DOTween.Sequence();
                    mySeq.Append(item.transform.DOLocalMove(new Vector3(0f, item.StackPoint.localPosition.y * count, 0f), moveTime));
                    //mySeq.Join(item.transform.DOLocalRotate(newRotate, moveTime));
                    mySeq.Join(item.transform.DOLocalRotate(Vector3.zero, moveTime));
                    mySeq.OnComplete(() =>
                    {
                        DOTween.Kill(item.transform);
                    });

                    Extensions.StackSorting(game.Player.StackPoint, game.PlayerItemList, j);
                    game.BaggageList.Add(item);

                    Signals.Get<VibrationSignal>().Dispatch(HapticTypes.LightImpact);
                }
            }
        }
    }
}
