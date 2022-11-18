using Crystal;
using DG.Tweening;
using HomaGames.HomaBelly;
using Kuhpik;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialSystem : GameSystemWithScreen<GameplayUIScreen>
{
    [SerializeField, BoxGroup("Developer")] GameObject pointerHorizontal;
    [SerializeField, BoxGroup("Developer")] GameObject pointerVertical;

    SafeArea safeArea;
    Rect rect;

    Transform target;
    GameObject pointer;
    bool isHorizontal;
    Vector2 tweeningVertical;
    List<ItemComponent> baggageList;
    public override void OnInit()
    {
        safeArea = screen.GetComponentInParent<SafeArea>();
        rect = safeArea.GetSafeArea();

        baggageList = new List<ItemComponent>();
        foreach (var people in game.PeoplePlatformList)
            baggageList.Add(people.Baggage);
    }
    public override void OnUpdate()
    {
        TurorialCheck();

        if (target) PositionCheck(target);
        else if (pointer) Destroy(pointer);
    }
    void TurorialCheck()
    {
        switch (player.Tutorial)
        {
            default:

                break;

            case 0:
                tweeningVertical = new Vector2(2.5f, 3.5f);

                target = game.Airplane.LadderLowerZone.transform;

                if (!target.gameObject.activeSelf) SaveTutorial(1); 

                break;

            case 1:
                tweeningVertical = new Vector2(2.5f, 3.5f);

                if (game.Airplane.LadderLowerZone.gameObject.activeSelf) return;

                Transform baggage = BaggageCheck();
                if (baggage != null) target = baggage;
                else target = game.Airplane.BaggageZone.transform;

                if (game.BaggageList.Count >= game.PeoplePlaneList.Count && game.PeopleOnPlaneList.Count <= 0 && game.BaggageList.Count > 0)
                    SaveTutorial(2);

                break;

            case 2:
                tweeningVertical = new Vector2(3.5f, 4.5f);

                if (game.BaggageList.Count < game.PeoplePlaneList.Count || game.PeopleOnPlaneList.Count > 0 || game.BaggageList.Count <= 0) return;

                target = game.Airplane.TutorialPointList[0];

                if (!game.Airplane.IsLadderOpen) SaveTutorial(3);

                break;

            case 3:
                tweeningVertical = new Vector2(3.5f, 4.5f);

                if (game.BaggageList.Count <= 0 || game.Airplane.IsLadderOpen) return;

                if (!IsOrder()) target = null;
                else
                {
                    if (IsPlayerItem()) 
                    {
                        tweeningVertical = new Vector2(8f, 10f);

                        target = PeopleCheck(); 
                    }
                    else target = TableCheck();
                }

                if (player.TutorialOrder > 1 && game.PeoplePlaneList[1].FoodAmount <= 0) SaveTutorial(4);

                break;

            case 4:
                tweeningVertical = new Vector2(7.0f, 8.0f);

                if (!game.Airplane.LadderRaiseZone.gameObject.activeSelf) return;

                Transform unlock = Unlock();
                if (game.PeopleFromPlaneList.Count > 0 || unlock == null) SaveTutorial(5);
                else target = unlock;

                break;

            case 5:
                tweeningVertical = new Vector2(3.5f, 4.5f);

                if (game.PeopleFromPlaneList.Count > 0) SaveTutorial(6);

                if (!game.Airplane.LadderRaiseZone.gameObject.activeSelf) return;
                target = game.Airplane.LadderRaiseZone.transform;

                break;

            case 6:
                tweeningVertical = new Vector2(2.5f, 3.5f);

                if (!game.Airplane.BaggageZone.gameObject.activeSelf 
                    || game.Airplane.LadderLowerZone.gameObject.activeSelf 
                    || game.PeoplePlatformList.Count <= 0) return;

                if (game.Player.transform.position.y > 1.8f) target = game.Airplane.TutorialPointList[1];
                else target = game.Airplane.BaggageZone.transform;

                if (game.BaggageList.Count <= 0) SaveTutorial(7);

                break;

            case 7:
                tweeningVertical = new Vector2(1.5f, 2.5f);

                if (game.ConveyorList.Count > 0) SaveTutorial(8);

                if (game.PeoplePlatformList.Count <= 0 || game.PlayerItemList.Count <= 0) return;

                target = game.Conveyor.Zone;

                break;

            case 8:
                tweeningVertical = new Vector2(2.5f, 3.5f);

                if (game.ConveyorList.Count <= 0) return;

                target = game.Airplane.LadderLowerZone.transform;

                if (!target.gameObject.activeSelf) SaveTutorial(9);

                break;
        }
    }
    void PositionCheck(Transform target)
    {
        Vector2 pointerPosition = Camera.main.WorldToScreenPoint(target.position);
        if (rect.Contains(pointerPosition))
        {
            if (pointer && isHorizontal) { Destroy(pointer); isHorizontal = false; }
            if (!pointer)
            {
                pointer = Instantiate(pointerVertical);
                pointer.transform.position = target.position;
                MoveArrow(pointer.transform);
            }
        }
        else
        {
            if (pointer && !isHorizontal) { Destroy(pointer); isHorizontal = true; }
            if (!pointer) pointer = Instantiate(pointerHorizontal, game.Player.transform);
            pointer.transform.LookAt(new Vector3(target.position.x, target.position.y, target.position.z));
        }
    }
    void SaveTutorial(int number)
    {
        target = null;

        player.Tutorial = number;
        Bootstrap.Instance.SaveGame();

        if (number == 1) HomaBelly.Instance.TrackDesignEvent("tutorial_step" + number + "_airplaneOpen");
        if (number == 2) HomaBelly.Instance.TrackDesignEvent("tutorial_step" + number + "_baggageLoad");
        if (number == 3) HomaBelly.Instance.TrackDesignEvent("tutorial_step" + number + "_airplaneTakeoff");
        if (number == 4) HomaBelly.Instance.TrackDesignEvent("tutorial_step" + number + "_passengerService");
        if (number == 5) HomaBelly.Instance.TrackDesignEvent("tutorial_step" + number + "_placeBuy");
        if (number == 6) HomaBelly.Instance.TrackDesignEvent("tutorial_step" + number + "_airplaneLanding");
        if (number == 7) HomaBelly.Instance.TrackDesignEvent("tutorial_step" + number + "_baggageUnload");
        if (number == 8) HomaBelly.Instance.TrackDesignEvent("tutorial_step" + number + "_conveyorDrop");
        if (number == 9)
        {
            HomaBelly.Instance.TrackDesignEvent("tutorial_step" + number + "_airplaneOpen");
            HomaBelly.Instance.TrackDesignEvent("tutorial_completed");
        }
    }
    void MoveArrow(Transform arrow)
    {
        float time = 0.5f;

        arrow.position = new Vector3(target.position.x, arrow.position.y, target.position.z);

        Sequence mySeq = DOTween.Sequence();
        mySeq.Append(arrow.DOLocalMoveY(tweeningVertical.y, time));
        mySeq.Join(arrow.DOScale(1f, time / 2));
        mySeq.Append(arrow.DOLocalMoveY(tweeningVertical.x, time));
        mySeq.Join(arrow.DOScale(new Vector3(1.3f, 0.7f, 1f), time / 2));
        mySeq.OnComplete(() =>
        {
            DOTween.Kill(pointer);
            MoveArrow(arrow);
        });
    }
    Transform BaggageCheck()
    {
        Transform baggage = null;
        float currentDistance = 99999;
        foreach (var item in baggageList)
        {
            if (!game.PlayerItemList.Contains(item) && !game.BaggageList.Contains(item))
            {
                float distance = Vector3.Distance(item.transform.position, game.Player.transform.position);
                if (currentDistance > distance)
                {
                    currentDistance = distance;
                    baggage = item.transform;
                }
            }
        }
        return baggage;
    }
    Transform Unlock()
    {
        Transform unlock = null;

        foreach (UnlockComponent _unlock in UnlockComponent.Hashset.ToList())
            if (_unlock.GetOpeningNumber() == 0) unlock = _unlock.transform;

        return unlock;
    }
    Transform PeopleCheck()
    {
        return game.PeoplePlaneList[Mathf.Clamp(player.TutorialOrder - 1, 0, game.PeoplePlaneList.Count)].PlaceBlock.transform;
    }
    Transform TableCheck()
    {
        foreach (TableFoodComponent component in TableFoodComponent.Hashset.ToList())
            if (component.ItemType == OrderType()) return component.TriggerZone.transform;

        return null;
    }
    ItemType OrderType()
    {
        return game.PeoplePlaneList[Mathf.Clamp(player.TutorialOrder - 1, 0, game.PeoplePlaneList.Count)].FoodType;
    }
    bool IsOrder()
    {
        return game.PeoplePlaneList[Mathf.Clamp(player.TutorialOrder - 1, 0, game.PeoplePlaneList.Count)].IsFoodReady;
    }
    bool IsPlayerItem()
    {
        foreach (var item in game.PlayerItemList)
            if (item.ItemType == OrderType()) return true;

        return false;
    }
}