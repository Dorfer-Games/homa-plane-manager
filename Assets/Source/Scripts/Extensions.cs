using UnityEngine;
using Supyrb;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;

static class Extensions
{
    public static void PeopleAnimation(Animator animator, string animation, float speed = 0f)
    {
        if (animation != "None")
        {
            animator.SetFloat("speed", speed);
            animator.SetBool(animation, true);
        }
        foreach (var parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
                if (parameter.name != animation) animator.SetBool(parameter.name, false);
        }
    }
    public static void BubbleUIUpdate(BubbleUIType Type, Transform target, int amount = 0, ItemType foodType = ItemType.None)
    {
        var bubbleData = new BubbleUITransferData()
        {
            Type = Type,
            Target = target,
            FoodType = foodType,
            Amount = amount
        };

        Signals.Get<BubbleUISignal>().Dispatch(bubbleData);
    }
    public static void StackSorting(Transform point, List<ItemComponent> stack, int ID)
    {
        if (ID < stack.Count - 1)
        {
            int pointID = ID - 1;
            if (pointID < 0) stack[ID + 1].transform.parent = point;
            else stack[ID + 1].transform.parent = stack[ID - 1].StackPoint;

            stack[ID + 1].transform.DOLocalMove(Vector3.zero, 0.5f / stack.Count);
        }
        stack.RemoveAt(ID);
    }
    public static Sequence MoveItem(ItemComponent component, int count, float time, float scaleIncrease, float scaleDecrease, Vector3 rotate)
    {
        Transform item = component.transform;
        Vector3 centerPos = MidPoint(item.localPosition, Vector3.zero);

        Sequence mySeq = DOTween.Sequence();
        mySeq.Append(item.DOLocalMove(new Vector3(centerPos.x, 0.25f * count, centerPos.z), time));
        mySeq.Join(item.DOScale(scaleIncrease, time));
        mySeq.Join(item.DOLocalRotate(Vector3.zero, time));
        mySeq.Join(component.Model.DOLocalRotate(rotate, time));
        mySeq.Append(item.DOLocalMove(Vector3.zero, time));
        mySeq.Join(item.DOScale(scaleDecrease, time));

        return mySeq;
    }
    public static Vector3 MidPoint(Vector3 start, Vector3 finish)
    {
        Vector3 center = Vector3.zero;
        center.x = (start.x + finish.x) / 2;
        center.z = (start.z + finish.z) / 2;
        return center;
    }
}