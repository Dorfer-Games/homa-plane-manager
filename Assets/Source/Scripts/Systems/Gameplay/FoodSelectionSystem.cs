using DG.Tweening;
using Kuhpik;
using MoreMountains.NiceVibrations;
using NaughtyAttributes;
using Supyrb;
using System.Linq;
using UnityEngine;

public class FoodSelectionSystem : GameSystem
{
    [SerializeField, BoxGroup("Settings")] int foodMax = 5;

    public override void OnInit()
    {
        Signals.Get<SignalFillZone>().AddListener(FoodCheck);

        foreach (var table in TableFoodComponent.Hashset.ToList())
            table.TriggerZone.SetActive(false);
    }
    void FoodCheck(FillZoneComponent zone)
    {
        foreach (var table in TableFoodComponent.Hashset.ToList())
        {
            if (zone.gameObject == table.TriggerZone)
            {
                if (isSelection(table.ItemType)) FoodSelection(table);

                break;
            }
        }
    }
    void FoodSelection(TableFoodComponent table)
    {
        table.TriggerZone.SetActive(false);

        var foodData = game.FoodList.FirstOrDefault(x => x.Type == table.ItemType);

        Transform food = Instantiate(foodData.Prefab).transform;
        food.position = table.SpawnPoint.position;

        food.parent = game.Player.StackPoint;
        if (game.PlayerItemList.Count > 0) food.parent = game.PlayerItemList[game.PlayerItemList.Count - 1].StackPoint;

        ItemComponent component = food.GetComponent<ItemComponent>();
        game.PlayerItemList.Add(component);

        Sequence mySeq = Extensions.MoveItem(component, game.PlayerItemList.Count, 0.25f, 1f, 1f, Vector3.zero);
        mySeq.OnComplete(() =>
        {
            DOTween.Kill(component.transform);
        });

        table.TriggerZone.SetActive(true);
        Signals.Get<VibrationSignal>().Dispatch(HapticTypes.LightImpact);
    }
    bool isSelection(ItemType type)
    {
        int foodAmount = 0;
        foreach (var item in game.PlayerItemList)
            if (item.ItemType == type) foodAmount++;

        if (foodAmount >= foodMax) return false;
        else return true;
    }
}