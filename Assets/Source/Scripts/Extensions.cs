using UnityEngine;
using Supyrb;
using DG.Tweening;

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
    public static Sequence MoveItem(ItemComponent component, int count, float time, float scaleIncrease, float scaleDecrease, float rotate = 0f)
    {
        Transform item = component.transform;
        Vector3 centerPos = MidPoint(item.localPosition, Vector3.zero);

        Sequence mySeq = DOTween.Sequence();
        mySeq.Append(item.DOLocalMove(new Vector3(centerPos.x, 0.25f * count, centerPos.z), time));
        mySeq.Join(item.DOScale(scaleIncrease, time));
        mySeq.Join(item.DOLocalRotate(Vector3.zero, time));
        mySeq.Append(item.DOLocalMove(Vector3.zero, time));
        mySeq.Join(item.DOScale(scaleDecrease, time));
        mySeq.Join(component.Model.DOLocalRotate(new Vector3(0f, rotate, 0f), time));

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