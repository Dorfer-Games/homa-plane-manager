using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using UnityEngine;

public class ResourceLoadingSystem : GameSystemWithScreen<ResourceUIScreen>
{
    [SerializeField, BoxGroup("Testing")] int moneyAmount;
    [SerializeField, Button("Add coin")] void ButtonAction() => MoneyAmountChange(moneyAmount);
    public override void OnInit()
    {
        Signals.Get<MoneyChangeSignal>().AddListener(MoneyAmountChange);

        MoneyAmountUpdate(player.MoneyAmount, false);
    }
    void MoneyAmountChange(int amount)
    {
        player.MoneyAmount += amount;

        Bootstrap.Instance.SaveGame();
        MoneyAmountUpdate(player.MoneyAmount, true);
    }
    void MoneyAmountUpdate(int amount, bool isTweening)
    {
        screen.MoneyText.text = amount.ToString();

        if (isTweening)
        {
            screen.MoneyText.transform.DORewind();
            screen.MoneyText.transform.DOPunchScale(new Vector3(0.15f, 0.15f, 0.15f), 1f, 4, 1f);
        }
    }
}