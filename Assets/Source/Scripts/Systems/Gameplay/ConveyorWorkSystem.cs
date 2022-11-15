using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using UnityEngine;

public class ConveyorWorkSystem : GameSystem
{
    [SerializeField, BoxGroup("Settings")] float cooldown;

    public override void OnInit()
    {
        Signals.Get<AirplaneStateSignal>().AddListener(ConveyorClear);

        ConveyorWork();
    }
    public override void OnUpdate()
    {
        if (game.ConveyorList.Count <= 0) return;

        for (int i = game.ConveyorList.Count - 1; i >= 0; i--)
        {
            if (game.ConveyorList[i].ConveyorPoint != null) continue;

            Transform point = ConveyorPoint();
            if (point != null)
            {
                ItemComponent item = game.ConveyorList[i];
                item.transform.parent = point;

                Sequence mySeq = Extensions.MoveItem(item, Random.Range(5, 10), Random.Range(0.2f, 0.6f), 1f, 1f, Vector3.zero);
                mySeq.OnComplete(() =>
                {
                    DOTween.Kill(item.transform);
                });

                item.SetConveyorPoint(point);
            } else break;
        }
    }
    void ConveyorWork()
    {
        for (int i = 1; i < game.Conveyor.BaggagePointList.Count; i++)
        {
            int pointID = i + 1;
            if (pointID >= game.Conveyor.BaggagePointList.Count) pointID = 1;

            game.Conveyor.BaggagePointList[i].DOLocalMove(game.Conveyor.BaggagePointList[pointID].localPosition, cooldown);
        }

        game.Conveyor.BaggagePointList[0].DOLocalMove(game.Conveyor.BaggagePointList[0].localPosition, cooldown + 0.02f)
            .OnComplete(() =>
            {
                ConveyorWork();
            });
    }
    void ConveyorClear(AirplaneState state)
    {
        if (state != AirplaneState.Landing) return;

        foreach (var item in game.ConveyorList)
            Destroy(item.gameObject);

        game.ConveyorList.Clear();
    }
    Transform ConveyorPoint()
    {
        float currentDistance = 99999;
        Transform point = null;
        for (int i = 1; i < game.Conveyor.BaggagePointList.Count; i++)
        {
            float distance = Vector3.Distance(game.Conveyor.BaggagePointList[i].position, game.Conveyor.BaggagePointList[0].position);
            if (currentDistance > distance)
            {
                currentDistance = distance;
                point = game.Conveyor.BaggagePointList[i];
            }
        }

        foreach (var item in game.ConveyorList)
            if (item.ConveyorPoint == point) return null;

        return point;
    }
}