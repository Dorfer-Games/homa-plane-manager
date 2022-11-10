using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using UnityEngine;

public class AirplaneTakeoffSystem : GameSystem
{
    [SerializeField, BoxGroup("Developer")] float height = 10f;
    [SerializeField, BoxGroup("Developer")] float cooldown = 3f;
    
    public override void OnInit()
    {
        Signals.Get<AirplaneStateSignal>().AddListener(AirplaneTakeoff);

        game.Ground = FindObjectOfType<GroundComponent>();
    }
    void AirplaneTakeoff(AirplaneState state)
    {
        if (state != AirplaneState.Takeoff) return;

        game.Ground.transform.DOLocalMoveY(-height, cooldown)
            .OnComplete(() =>
            {
                Signals.Get<AirplaneStateSignal>().Dispatch(AirplaneState.Flight);
            });
    }
}
