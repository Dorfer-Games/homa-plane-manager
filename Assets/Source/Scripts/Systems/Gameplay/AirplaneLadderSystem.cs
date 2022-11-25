using DG.Tweening;
using HomaGames.HomaBelly;
using Kuhpik;
using MoreMountains.NiceVibrations;
using NaughtyAttributes;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneLadderSystem : GameSystemWithScreen<SettingsUIScreen>
{
    [SerializeField, BoxGroup("Developer")] float ladderCooldown = 1f;

    public bool IsCrea;

    public override void OnInit()
    {
        Signals.Get<AirplaneStateSignal>().AddListener(LadderZoneCheck);
        Signals.Get<SignalFillZone>().AddListener(LadderAction);

        game.LadderCooldown = ladderCooldown;

        game.Airplane.LadderLowerZone.SetActive(true);
        game.Airplane.LadderRaiseZone.SetActive(false);
        game.Airplane.BaggageZone.SetActive(false);
        game.Airplane.DoorCollider.enabled = true;

        game.IsCrea = IsCrea;
    }
    void LadderAction(FillZoneComponent zone)
    {
        if (zone.gameObject == game.Airplane.LadderLowerZone)
        {
            zone.gameObject.SetActive(false);
            game.Airplane.BaggageZone.SetActive(true);

            if (!game.Airplane.IsLadderOpen) LadderOpen(game.PeoplePlatformList, game.PeopleOnPlaneList);
            else StartCoroutine(PeopleRun(game.PeoplePlatformList, game.PeopleOnPlaneList));

            Signals.Get<VibrationSignal>().Dispatch(HapticTypes.MediumImpact);

            //homa event
            HomaBelly.Instance.TrackDesignEvent("level_" + player.GameLevel + "_started");
        }

        if (zone.gameObject == game.Airplane.LadderRaiseZone)
        {
            zone.gameObject.SetActive(false);

            if (!game.Airplane.IsLadderOpen) LadderOpen(game.PeoplePlaneList, game.PeopleFromPlaneList);

            Signals.Get<VibrationSignal>().Dispatch(HapticTypes.MediumImpact);
        }
    }
    void LadderOpen(List<PeopleData> from, List<PeopleData> to)
    {
        game.Airplane.BaggageDoor.DOLocalRotate(new Vector3(0f, 0f, game.Airplane.BaggageDoorRotate.y), ladderCooldown)
            .OnComplete(() =>
            {
                foreach (var item in game.BaggageList)
                {
                    Vector3 newRotate = new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), 0f);
                    item.transform.DOLocalRotate(newRotate, 0f);

                    item.Model.gameObject.SetActive(true);
                }  
            });

        game.Airplane.Ladder.DOLocalRotate(new Vector3(0f, 0f, game.Airplane.LadderRotate.y), ladderCooldown)
              .OnComplete(() =>
              {
                  game.Airplane.DoorCollider.enabled = false;
                  game.Airplane.SetLadderStatus(true);

                  game.Airplane.BaggageZone.SetActive(true);

                  DOTween.To(Blend, 0f, 0f, 5f)
                      .OnComplete(() =>
                      {
                          screen.Tap.SetActive(true);

                          StartCoroutine(PeopleRun(from, to));
                      });
              });
    }

    IEnumerator PeopleRun(List<PeopleData> from, List<PeopleData> to)
    {
        Signals.Get<NavigationUpdateSignal>().Dispatch();

        int amount = from.Count;
        if (!IsCrea) amount = 24;

        //blend
        if (IsCrea) 
        {
            float time = 6.6f;

            game.Airplane.Ladder.DOLocalRotate(new Vector3(0f, 0f, game.Airplane.LadderRotate.y), time)
              .OnComplete(() =>
              {
                  foreach (var place in game.Airplane.PlaceList)
                  {
                      if (place.isCrea)
                      {
                          foreach (var _place in place.PlaceList)
                              _place.transform.DOShakeScale(time * 1.3f, 0.05f);

                      }
                  }
              });

            DOTween.To(Blend, 0f, 0f, time)
              .OnComplete(() =>
              {
                  /*
                  game.Airplane.NavMesh.DOScale(new Vector3(35f, 
                      game.Airplane.NavMesh.localScale.y, 
                      game.Airplane.NavMesh.localScale.z), 5f);
                  */
                  float time = 3f;

                  foreach (var place in game.Airplane.PlaceLeftOne)
                      place.DOLocalMoveX(game.Airplane.Left.x, time).SetEase(Ease.Linear);

                  foreach (var place in game.Airplane.PlaceLeftTwo)
                      place.DOLocalMoveX(game.Airplane.Left.y, time).SetEase(Ease.Linear);

                  foreach (var place in game.Airplane.PlaceRightOne)
                      place.DOLocalMoveX(game.Airplane.Right.x, time).SetEase(Ease.Linear);

                  foreach (var place in game.Airplane.PlaceRightTwo)
                      place.DOLocalMoveX(game.Airplane.Right.y, time).SetEase(Ease.Linear);


                  DOTween.To(Blend, 0f, 100f, time).SetEase(Ease.Linear);
              });
        }

        for (int i = 0; i < amount; i++)
        {
            from[i].Component.Agent.enabled = true;
            to.Add(from[i]);

            yield return new WaitForSeconds(0.02f);
        }

        from.Clear();
    }
    void Blend(float value)
    {
        foreach (var blend in game.Airplane.Blend)
            blend.SetBlendShapeWeight(0, value);
    }
    void LadderZoneCheck(AirplaneState state)
    {
        if (state != AirplaneState.Ready) return;

        game.Airplane.LadderLowerZone.SetActive(true);

        //homa event
        HomaBelly.Instance.TrackDesignEvent("level_" + player.GameLevel + "_completed");
        player.GameLevel++;
        Bootstrap.Instance.SaveGame();
    }
}