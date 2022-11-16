using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using System.Collections.Generic;
using UnityEngine;

public class EffectLoadingSystem : GameSystem
{
    [SerializeField, BoxGroup("Settings")] List<GameObject> effectList;
    [SerializeField, BoxGroup("Settings")] List<GameObject> emojiList;

    public override void OnInit()
    {
        Signals.Get<EffectSignal>().AddListener(EffectCreate);
    }
    void EffectCreate(Transform spawn, EffectType type, Vector3 position)
    {
        GameObject effect = null;
        switch (type)
        {
            default:

                break;

            case EffectType.PeopleOrder:
                effect = Instantiate(emojiList[Random.Range(0, emojiList.Count)]);
                effect.transform.position = position;
                Destroy(effect, 1f);

                break;

            case EffectType.MoneyTake:
                effect = Instantiate(effectList[0]);
                effect.transform.position = spawn.position;
                Destroy(effect, 1f);

                break;

            case EffectType.PlaceOpen:
                effect = Instantiate(effectList[1]);
                effect.transform.position = spawn.position;
                Destroy(effect, 1f);

                break;
        }
    }
}