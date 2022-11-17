using DG.Tweening;
using HomaGames.HomaBelly;
using Kuhpik;
using MoreMountains.NiceVibrations;
using NaughtyAttributes;
using Supyrb;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnlockSystem : GameSystem
{
    [SerializeField, BoxGroup("Settings")] float cooldown = 0.25f;
    [SerializeField, BoxGroup("Settings")] float cooldownMoney = 0.2f;

    [SerializeField, BoxGroup("Developer")] [Tag] string unlockTag;

    float nextUpdate;
    UnlockComponent unlockComponent;
    float nextMoney;
    public override void OnInit()
    {
        Signals.Get<AirplaneStateSignal>().AddListener(TriggerZoneActive);

        game.Player.Trigger.OnTriggerEnterEvent += TriggerEnterCheck;
        game.Player.Trigger.OnTriggerStayEvent += TriggerStayCheck;

        if (player.UnlockList == null) player.UnlockList = new List<UnlockData>();

        UpdateInformation();
    }
    void TriggerEnterCheck(Transform other, Transform original)
    {
        if (other.CompareTag(unlockTag))
        {
            nextUpdate = 0f;
            unlockComponent = other.GetComponentInParent<UnlockComponent>();
        }
    }
    void TriggerStayCheck(Transform other)
    {
        if (other.CompareTag(unlockTag))
        {
            if (nextUpdate > Time.time) return;

            UnlockCheck();
        }
    }
    void UnlockCheck()
    {
        if (player.MoneyAmount <= 0) return;

        int amount = Mathf.Clamp(Mathf.RoundToInt(unlockComponent.GetPrice() / 10), 1, player.MoneyAmount);
        int price = unlockComponent.GetPrice() - amount;

        Signals.Get<MoneyChangeSignal>().Dispatch(-amount);
        Extensions.BubbleUIUpdate(BubbleUIType.Unlock, unlockComponent.BubblePoint, price);

        unlockComponent.SetPrice(price);
        SaveInformation(unlockComponent.name, price);

        if (price <= 0)
        {
            Unlock(unlockComponent, true);

            player.UnlockAmount++;
            Bootstrap.Instance.SaveGame();

            UpdateInformation();

            Signals.Get<VibrationSignal>().Dispatch(HapticTypes.RigidImpact);

            HomaBelly.Instance.TrackDesignEvent(unlockComponent.name);
        } else Signals.Get<VibrationSignal>().Dispatch(HapticTypes.LightImpact);

        nextUpdate = Time.time + cooldown / price;

        //money gameobject
        if (nextMoney <= Time.time)
        {
            Transform money = Instantiate(game.MoneyPrefab, game.Ground.transform).transform;
            money.tag = "Untagged";

            money.position = game.Player.StackPoint.position;
            money.parent = unlockComponent.transform;

            Vector3 centerPos = Extensions.MidPoint(money.localPosition, Vector3.zero);

            Sequence mySeq = DOTween.Sequence();
            mySeq.Append(money.DOLocalMove(new Vector3(centerPos.x, money.localPosition.y * 1.5f, centerPos.z), cooldownMoney * 2));
            mySeq.Append(money.DOLocalMove(Vector3.zero, cooldownMoney * 2));
            mySeq.OnComplete(() =>
            {
                Destroy(money.gameObject);
            });

            nextMoney = Time.time + cooldownMoney;
        }
    }
    void UpdateInformation()
    {
        foreach (UnlockComponent unlock in UnlockComponent.Hashset.ToList())
        {
            SetStatus(unlock, false, true);

            if (unlock.GetOpeningNumber() > player.UnlockAmount) SetStatus(unlock, false, false);
            else
            {
                UnlockData unlockData = player.UnlockList.FirstOrDefault(x => x.Name == unlock.name);

                if (unlockData != null) unlock.SetPrice(unlockData.Price);

                if (unlock.GetPrice() > 0) Extensions.BubbleUIUpdate(BubbleUIType.Unlock, unlock.BubblePoint, unlock.GetPrice());
                else Unlock(unlock, false);
            }
        }
    }
    void Unlock(UnlockComponent component, bool isTweening)
    {
        SetStatus(component, true, false);
        component.Model.transform.parent = null;

        if (isTweening)
        {
            float time = 1f;
            Transform model = component.Model.transform;

            float posirionY = model.position.y;
            model.position = new Vector3(model.position.x, posirionY + 3f, model.position.z);
            model.DOMoveY(posirionY, time).SetEase(Ease.OutBounce)
                .OnComplete(() => 
                {
                    Signals.Get<NavigationUpdateSignal>().Dispatch(); 
                });

            Signals.Get<EffectSignal>().Dispatch(model, EffectType.PlaceOpen, Vector3.zero);
        }
        else Signals.Get<NavigationUpdateSignal>().Dispatch();

        Destroy(component.gameObject);
    }
    void SaveInformation(string name, int price)
    {
        var unlockData = player.UnlockList.FirstOrDefault(x => x.Name == name);

        if (unlockData == null)
        {
            unlockData = new UnlockData()
            {
                Name = name
            };
            player.UnlockList.Add(unlockData);
        }
        unlockData.Price = price;

        Bootstrap.Instance.SaveGame();
    }
    void SetStatus(UnlockComponent component, bool isModel, bool isTrigger)
    {
        component.Model.SetActive(isModel);
        component.Zone.gameObject.SetActive(isTrigger);
    }
    void TriggerZoneActive(AirplaneState state)
    {
        if (state == AirplaneState.Takeoff) 
        {
            foreach (TriggerZoneComponent component in TriggerZoneComponent.Hashset.ToList())
                component.transform.localPosition = new Vector3(1000f, 0f, 0f);
        } else if (state == AirplaneState.Idle)
        {
            foreach (TriggerZoneComponent component in TriggerZoneComponent.Hashset.ToList())
                component.transform.localPosition = Vector3.zero;
        }
    }
}