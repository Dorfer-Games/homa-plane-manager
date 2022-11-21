using DG.Tweening;
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
        GameObject effect = null; Vector3 newPosition = Vector3.zero;
        switch (type)
        {
            default:

                break;

            case EffectType.PeopleOrder:
                if (position == Vector3.zero)
                {
                    newPosition = new Vector3(spawn.position.x, spawn.position.y - 1.5f, spawn.position.z);
                    effect = Instantiate(effectList[3]);
                    effect.transform.position = newPosition;
                } else
                {
                    newPosition = new Vector3(spawn.position.x, spawn.position.y + 2.25f, spawn.position.z);

                    effect = Instantiate(emojiList[1]);
                    effect.transform.position = newPosition;
                }
                Destroy(effect, 1f);

                break;

            case EffectType.MoneyTake:
                newPosition = new Vector3(spawn.position.x, spawn.position.y + 0.4f, spawn.position.z);

                effect = Instantiate(effectList[0]);
                effect.transform.position = newPosition;
                Destroy(effect, 1f);

                break;

            case EffectType.PlaceOpen:
                effect = Instantiate(effectList[1]);
                effect.transform.position = spawn.position;
                Destroy(effect, 1f);

                break;

            case EffectType.Camera:
                effect = Instantiate(effectList[2], spawn);
                Destroy(effect, position.x);

                break;

            case EffectType.VIP:
                spawn.DOLocalMove(spawn.localPosition, Random.Range(0f, 2f))
                  .OnComplete(() =>
                  {
                      newPosition = new Vector3(spawn.position.x, spawn.position.y + 3f, spawn.position.z);

                      effect = Instantiate(emojiList[0]);
                      effect.transform.position = newPosition;
                      Destroy(effect, 1f);
                  });

                break;
        }
    }
}