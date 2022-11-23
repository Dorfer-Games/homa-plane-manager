using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using System.Collections;
using UnityEngine;

public class CreaSystem : GameSystem
{
    [SerializeField, BoxGroup("Developer")] [Tag] string creaTag;

    bool isCrea;
    public override void OnInit()
    {
        game.Player.Trigger.OnTriggerEnterEvent += TriggerEnterCheck;
    }

    void TriggerEnterCheck(Transform other, Transform original)
    {
        if (isCrea) return;

        if (other.CompareTag(creaTag) && game.PlayerItemList.Count > 0)
        {
            isCrea = true;

            StartCoroutine(Shake());
        }

    }
    IEnumerator Shake()
    {
        Signals.Get<ControllerChangeSignal>().Dispatch(ControllerType.Crea);

        int random = Random.Range(15, 20);

        for (int i = 0; i < random; i++)
        {
            int itemID = Random.Range(0, game.PlayerItemList.Count);
            ItemComponent item = game.PlayerItemList[itemID];

            item.transform.parent = game.Ground.transform;

            float moveTime = 0.5f;
            Vector3 position = new Vector3(item.transform.localPosition.x + Random.Range(-8f, 8f),
                0f,
                item.transform.localPosition.z + Random.Range(-8f, 8f));

            Vector3 rotation = new Vector3(Random.Range(0, 45f),
                Random.Range(0, 90f),
                Random.Range(0, 90f));

            Vector3 centerPos = Extensions.MidPoint(item.transform.localPosition, position);

            Sequence mySeq = DOTween.Sequence();
            mySeq.Append(item.transform.DOLocalMove(new Vector3(centerPos.x, item.transform.localPosition.y + 0.2f, centerPos.z), moveTime));
            mySeq.Join(item.Model.DOLocalRotate(rotation, moveTime));
            mySeq.Append(item.transform.DOLocalMove(position, moveTime));
            
            //mySeq.Join(item.transform.DOLocalRotate(newRotate, moveTime));
            //mySeq.Join(item.transform.DOLocalRotate(Vector3.zero, moveTime));
            mySeq.OnComplete(() =>
            {
                DOTween.Kill(item.transform);
            });

            Extensions.StackSorting(game.Player.StackPoint, game.PlayerItemList, itemID);

            yield return new WaitForSeconds(0.02f);
        }

        Signals.Get<ControllerChangeSignal>().Dispatch(ControllerType.Player);
    }
}
