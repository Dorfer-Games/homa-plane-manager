using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using UnityEngine;

public class MoneySelectionSystem : GameSystem
{
    [SerializeField, BoxGroup("Settings")] int moneyAmount = 5;

    [SerializeField, BoxGroup("Developer")] [Tag] string moneyTag;

    public override void OnInit()
    {
        game.Player.Trigger.OnTriggerEnterEvent += TriggerEnterCheck;
    }
    void TriggerEnterCheck(Transform other, Transform original)
    {
        if (other.CompareTag(moneyTag))
            MoneySelection(other);
    }
    void MoneySelection(Transform money)
    {
        Destroy(money.gameObject);
        Signals.Get<MoneyChangeSignal>().Dispatch(moneyAmount);
    }
}