using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using System.Linq;
using UnityEngine;

public class FoodOrderSystem : GameSystem
{
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

            Extensions.BubbleUIUpdate(BubbleUIType.Attention, people.Component.BubblePoint);
            Extensions.BubbleUIUpdate(BubbleUIType.Order, people.Component.BubblePoint, people.FoodType, people.FoodAmount);

            people.IsFood = true;
        } else if (WaitAmount() <= 0)
        {
            foreach (var table in TableFoodComponent.Hashset.ToList())
                table.TriggerZone.SetActive(false);

            for (int i = game.PlayerItemList.Count - 1; i >= 0; i--)
                Destroy(game.PlayerItemList[i].gameObject);

            game.PlayerItemList.Clear();

            Signals.Get<AirplaneStateSignal>().Dispatch(AirplaneState.Landing);
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