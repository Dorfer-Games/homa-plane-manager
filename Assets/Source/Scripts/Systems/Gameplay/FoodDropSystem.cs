using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using UnityEngine;

public class FoodDropSystem : GameSystem
{
    [SerializeField, BoxGroup("Developer")] [Tag] string dropTag;

    public override void OnInit()
    {
        game.Player.Trigger.OnTriggerEnterEvent += TriggerEnterCheck;
    }
    void TriggerEnterCheck(Transform other, Transform original)
    {
        if (other.CompareTag(dropTag) && game.PlayerItemList.Count > 0 && game.PeoplePlaneList.Count > 0)
        {
            PlaceComponent place = other.GetComponentInParent<PlaceComponent>();
            foreach (var people in game.PeoplePlaneList)
            {
                if (people.Place == place)
                {
                    if (people.IsFood && people.FoodAmount > 0) FoodDrop(people);

                    break;
                }
            }
        }
    }
    void FoodDrop(PeopleData people)
    {
        for (int i = game.PlayerItemList.Count - 1; i >= 0; i--)
        {
            if (game.PlayerItemList[i].ItemType == people.FoodType)
            {
                ItemComponent item = game.PlayerItemList[i];
                item.transform.parent = people.Transform;

                Extensions.StackSorting(game.Player.StackPoint, game.PlayerItemList, i);

                Sequence mySeq = Extensions.MoveItem(item, Random.Range(10, 20), Random.Range(0.2f, 0.6f), 1f, 1f);
                mySeq.OnComplete(() =>
                {
                    DOTween.Kill(item.transform);
                    Destroy(item.gameObject);
                });

                people.FoodAmount--;
                Extensions.BubbleUIUpdate(BubbleUIType.Order, people.Component.BubblePoint, people.FoodType, people.FoodAmount);

                if (people.FoodAmount <= 0)
                {
                    Signals.Get<OrderUpdateSignal>().Dispatch();

                    break;
                }
            }
        }
    }
}
