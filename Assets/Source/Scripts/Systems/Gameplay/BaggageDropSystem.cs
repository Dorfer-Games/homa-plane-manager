using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using System.Linq;
using UnityEngine;

public class BaggageDropSystem : GameSystem
{
    [SerializeField, BoxGroup("Developer")] [Tag] string shelfTag;

    public override void OnInit()
    {
        game.Player.Trigger.OnTriggerEnterEvent += TriggerEnterCheck;
    }
    void TriggerEnterCheck(Transform other, Transform original)
    {
        if (other.CompareTag(shelfTag) && game.PlayerItemList.Count > 0)
        {
            ShelfComponent component = other.GetComponentInParent<ShelfComponent>();

            if (component.PointList.Count == component.ItemList.Count) return;

            int count = component.PointList.Count - component.ItemList.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = game.PlayerItemList.Count - 1; j >= 0; j--)
                {
                    if (game.PlayerItemList[j].ItemType == ItemType.Baggage)
                    {
                        ItemComponent item = game.PlayerItemList[j];
                        item.transform.parent = component.PointList[component.ItemList.Count];

                        Extensions.StackSorting(game.Player.StackPoint, game.PlayerItemList, j);
                        component.ItemList.Add(item);

                        Sequence mySeq = Extensions.MoveItem(item, Random.Range(10, 20), Random.Range(0.2f, 0.6f), 1f, 1f);
                        mySeq.OnComplete(() =>
                        {
                            DOTween.Kill(item.transform);
                        });

                        BaggageCheck();

                        break;
                    }
                }
            }
            
        }
    }
    void BaggageCheck()
    {
        bool isReady = true;
        foreach (var shelf in ShelfComponent.Hashset.ToList())
        {
            if (shelf.PointList.Count > shelf.ItemList.Count)
            {
                isReady = false;
                break;
            }
        }

        if (isReady) game.Airplane.LadderRaiseZone.SetActive(true);
    }
}
