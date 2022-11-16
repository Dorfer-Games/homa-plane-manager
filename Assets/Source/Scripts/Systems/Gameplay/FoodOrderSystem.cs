using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using System.Linq;
using UnityEngine;

public class FoodOrderSystem : GameSystem
{
    [SerializeField, BoxGroup("Settings")] Vector2 orderCooldown;

    [SerializeField, BoxGroup("Developer")] int orderStartAmount;
    [SerializeField, BoxGroup("Developer")] Vector2 foodAmount;

    public override void OnInit()
    {
        Signals.Get<AirplaneStateSignal>().AddListener(AttentionCreate);
        Signals.Get<OrderUpdateSignal>().AddListener(OrderCreate);
    }
    void AttentionCreate(AirplaneState state)
    {
        if (state != AirplaneState.Flight) return;

        foreach (var table in TableFoodComponent.Hashset.ToList())
            table.TriggerZone.SetActive(true);

        foreach (var people in game.PeoplePlaneList)
        {
            people.FoodType = game.FoodList[Random.Range(0, game.FoodList.Count)].Type;
            people.FoodAmount = Random.Range((int)foodAmount.x, (int)foodAmount.y + 1);

            Extensions.BubbleUIUpdate(BubbleUIType.Attention, people.Component.BubblePoint);
        }

        for (int i = 0; i < Mathf.Clamp(orderStartAmount, 0, HungryAmount()); i++)
            OrderCreate();
    }
    void OrderCreate()
    {
        if (HungryAmount() > 0)
        {
            PeopleData people = game.PeoplePlaneList[PeopleID()];
            people.IsFood = true;

            float time = Random.Range(orderCooldown.x, orderCooldown.y);
            people.Transform.DOScale(people.Transform.localScale, time)
                .OnComplete(() =>
                {
                    people.IsFoodReady = true;

                    ZoneUpdate();

                    Extensions.BubbleUIUpdate(BubbleUIType.Attention, people.Component.BubblePoint);
                    Extensions.BubbleUIUpdate(BubbleUIType.Order, people.Component.BubblePoint, people.FoodAmount, people.FoodType);
                });
        } else if (WaitAmount() <= 0)
        {
            foreach (var table in TableFoodComponent.Hashset.ToList())
                table.TriggerZone.SetActive(false);

            for (int i = game.PlayerItemList.Count - 1; i >= 0; i--)
                Destroy(game.PlayerItemList[i].gameObject);

            game.PlayerItemList.Clear();

            Signals.Get<AirplaneStateSignal>().Dispatch(AirplaneState.Landing);
        }

        ZoneUpdate();
    }
    void ZoneUpdate()
    {
        foreach (var place in game.Airplane.PlaceList)
            place.Zone.SetActive(false);

        foreach (var people in game.PeoplePlaneList)
        {
            if (people.IsFoodReady && people.FoodAmount > 0)
                people.PlaceBlock.Zone.SetActive(true);
        }
    }
    int HungryAmount()
    {
        int amount = 0;

        foreach (var people in game.PeoplePlaneList)
            if (!people.IsFood) amount++;

        return amount;
    }
    int WaitAmount()
    {
        int amount = 0;

        foreach (var people in game.PeoplePlaneList)
            if (people.IsFood && people.FoodAmount > 0) amount++;

        return amount;
    }
    int PeopleID()
    {
        int peopleID = 0;

        bool isReady = false;
        while (!isReady)
        {
            peopleID = Random.Range(0, game.PeoplePlaneList.Count);

            if (!game.PeoplePlaneList[peopleID].IsFood) isReady = true;
        }

        return peopleID;
    }
}