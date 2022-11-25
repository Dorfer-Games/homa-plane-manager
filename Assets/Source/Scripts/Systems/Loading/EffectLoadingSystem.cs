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

                int random = Random.Range(0, 2);
                if (random == 0)
                {
                    newPosition = new Vector3(spawn.position.x, spawn.position.y - 1.5f, spawn.position.z);
                    effect = Instantiate(effectList[3]);
                    effect.transform.position = newPosition;
                } else
                {
                    newPosition = new Vector3(spawn.position.x,spawn.position.y + 1.5f, spawn.position.z);

                    effect = Instantiate(emojiList[Random.Range(0, emojiList.Count)]);
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

            case EffectType.Crea:
                effect = Instantiate(effectList[4]);
                effect.transform.parent = spawn;
                effect.transform.localPosition = new Vector3(-3.48f, -0.68f, -5.32f);
                Destroy(effect, 1f);

                for (int i = 0; i < 100; i++)
                {
                    int randomID = Random.Range(0, game.PeoplePlaneList.Count);
                    PeopleData people = game.PeoplePlaneList[randomID];

                    Vector3 positionPeople = new Vector3(people.Transform.localPosition.x + Random.Range(-50f, 50f),
                        people.Transform.localPosition.y,
                        people.Transform.localPosition.z + Random.Range(-50f, 50f));

                    Vector3 centerPos = Extensions.MidPoint(people.Transform.localPosition, positionPeople);

                    float time_1 = Random.Range(0.5f, 1f);
                    float time_2 = Random.Range(0.5f, 1f);
                    Sequence mySeq = DOTween.Sequence();
                    mySeq.Append(people.Transform.DOLocalMove(new Vector3(centerPos.x, Random.Range(1f, 10f), centerPos.z), time_1));
                    mySeq.Join(people.Transform.DOShakeScale(time_1, 0.25f));
                    mySeq.Append(people.Transform.DOLocalMove(positionPeople, time_2));
                    mySeq.OnComplete(() =>
                    {
                        Destroy(people.Transform.gameObject);
                    });

                    game.PeoplePlaneList.Remove(people);
                }

                break;
        }
    }
}