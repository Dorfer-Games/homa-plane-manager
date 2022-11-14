using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using UnityEngine;

public class PeoplePaymentSystem : GameSystem
{
    [SerializeField, BoxGroup("Settings")] float time = 0.5f;
    [SerializeField, BoxGroup("Settings")] float offset = 5f;

    [SerializeField, BoxGroup("Developer")] GameObject moneyPrefab;

    public override void OnInit()
    {
        Signals.Get<PaymentSignal>().AddListener(Payment);
    }
    void Payment(Transform spawn, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Transform money = Instantiate(moneyPrefab, game.Airplane.transform).transform;
            money.position = spawn.position;

            Vector3 position = new Vector3(0f, 1.4f, money.localPosition.z + Random.Range(-offset, offset));
            Vector3 centerPos = Extensions.MidPoint(money.localPosition, position);

            Sequence mySeq = DOTween.Sequence();
            mySeq.Append(money.DOLocalMove(new Vector3(centerPos.x, spawn.localPosition.y * 2.5f, centerPos.z), time));
            mySeq.Append(money.DOLocalMove(position, time));
            mySeq.OnComplete(() =>
            {
                DOTween.Kill(money.transform);
            });
        }
    }
}
