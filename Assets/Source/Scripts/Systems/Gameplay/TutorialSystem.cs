using Crystal;
using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
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
    public override void OnInit()
    {
        safeArea = screen.GetComponentInParent<SafeArea>();
        rect = safeArea.GetSafeArea();
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

                if (game.PlayerItemList.Count <= 0) target = game.Platform.transform;
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
                        tweeningVertical = new Vector2(6f, 8f);

                        target = PeopleCheck(); 
                    }
                    else target = TableCheck();
                }

                if (player.TutorialOrder > 1 && game.PeoplePlaneList[1].FoodAmount <= 0) SaveTutorial(4);

                break;

            case 4:
                tweeningVertical = new Vector2(3.5f, 4.5f);

                if (game.PeopleFromPlaneList.Count > 0) SaveTutorial(5);

                if (!game.Airplane.LadderRaiseZone.gameObject.activeSelf) return;
                target = game.Airplane.LadderRaiseZone.transform;

                break;

            case 5:
                tweeningVertical = new Vector2(2.5f, 3.5f);

                if (!game.Airplane.BaggageZone.gameObject.activeSelf 
                    || game.Airplane.LadderLowerZone.gameObject.activeSelf 
                    || game.PeoplePlatformList.Count <= 0) return;

                target = game.Airplane.BaggageZone.transform;

                if (game.BaggageList.Count <= 0) SaveTutorial(6);

                break;

            case 6:
                tweeningVertical = new Vector2(2.5f, 3.5f);

                if (game.ConveyorList.Count > 0) SaveTutorial(7);

                if (!game.Airplane.BaggageZone.gameObject.activeSelf
                    || game.Airplane.LadderLowerZone.gameObject.activeSelf
                    || game.PeoplePlatformList.Count <= 0
                    || game.BaggageList.Count > 0) return;

                target = game.Conveyor.Zone;

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

        /*
        if (number == 1) HomaBelly.Instance.TrackDesignEvent("tutorial_step0_startPack");
        if (number == 2) HomaBelly.Instance.TrackDesignEvent("tutorial_step1_conveyorOpen");
        if (number == 3) HomaBelly.Instance.TrackDesignEvent("tutorial_step2_dozerOpen");
        if (number == 4) HomaBelly.Instance.TrackDesignEvent("tutorial_step3_craneOpen");
        if (number == 5) HomaBelly.Instance.TrackDesignEvent("tutorial_step4_dozerDrive");
        if (number == 6) HomaBelly.Instance.TrackDesignEvent("tutorial_step5_figureDestroy");
        if (number == 7) HomaBelly.Instance.TrackDesignEvent("tutorial_step6_conveyorDrop");
        if (number == 8) HomaBelly.Instance.TrackDesignEvent("tutorial_step7_figureDestroy");
        if (number == 9) HomaBelly.Instance.TrackDesignEvent("tutorial_step8_conveyorDrop");
        if (number == 10) HomaBelly.Instance.TrackDesignEvent("tutorial_step9_craneDrive");
        if (number == 11) HomaBelly.Instance.TrackDesignEvent("tutorial_step10_magnetTake");
        if (number == 12) HomaBelly.Instance.TrackDesignEvent("tutorial_step11_craneDrive");
        if (number == 13) HomaBelly.Instance.TrackDesignEvent("tutorial_step12_construction");
        if (number == 14) HomaBelly.Instance.TrackDesignEvent("tutorial_step13_upgrade");
        if (number == 15) HomaBelly.Instance.TrackDesignEvent("tutorial_step14_showConveyorUnlock");
        if (number == 16)
        {
            HomaBelly.Instance.TrackDesignEvent("tutorial_step15_showAssistantUnlock");
            HomaBelly.Instance.TrackDesignEvent("tutorial_completed");
        }
        */
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
    Transform PeopleCheck()
    {
        return game.PeoplePlaneList[player.TutorialOrder - 1].PlaceBlock.transform;
    }
    Transform TableCheck()
    {
        foreach (TableFoodComponent component in TableFoodComponent.Hashset.ToList())
            if (component.ItemType == OrderType()) return component.TriggerZone.transform;

        return null;
    }
    ItemType OrderType()
    {
        return game.PeoplePlaneList[player.TutorialOrder - 1].FoodType;
    }
    bool IsOrder()
    {
        return game.PeoplePlaneList[player.TutorialOrder - 1].IsFoodReady;
    }
    bool IsPlayerItem()
    {
        foreach (var item in game.PlayerItemList)
            if (item.ItemType == OrderType()) return true;

        return false;
    }
}