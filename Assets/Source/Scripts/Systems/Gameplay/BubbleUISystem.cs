using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BubbleUISystem : GameSystemWithScreen<BubbleUIScreen>
{
    [SerializeField, BoxGroup("Prefabs")] List<GameObject> bubblePrefabList;

    [SerializeField, BoxGroup("Developer")] float speedPosition;
    [SerializeField, BoxGroup("Developer")] Vector3 adjustPosition;

    [SerializeField, BoxGroup("Debug"), ReadOnly] List<BubbleUIData> bubbleList = new List<BubbleUIData>();

    BubbleUIData bubbleData;
    public override void OnInit()
    {
        Signals.Get<BubbleUISignal>().AddListener(BubbleUpdate);
    }
    public override void OnUpdate()
    {
        foreach (BubbleUIData bubble in bubbleList.ToList())
        {
            if (bubble.Target)
            {
                Vector3 newPosition = Camera.main.WorldToScreenPoint(bubble.Target.position + adjustPosition);
                Vector3 lerpPosition = Vector3.Lerp(bubble.Transform.position, newPosition, (speedPosition * Time.deltaTime));

                bubble.Transform.position = lerpPosition;
            }
            else BubbleDestroy(bubble);
        }
    }
    void BubbleUpdate(BubbleUITransferData data)
    {
        bubbleData = bubbleList.FirstOrDefault(x => x.Target == data.Target && x.Type == data.Type);

        switch (data.Type)
        {
            default:

                break;

            case BubbleUIType.Attention:
                if (bubbleData == null) BubbleCreate(bubblePrefabList[0], data.Type, data.Target);
                else if (bubbleData != null) BubbleDestroy(bubbleData);

                break;

            case BubbleUIType.Order:
                if (bubbleData == null && data.Amount <= 0) return;
                if (bubbleData == null) BubbleCreate(bubblePrefabList[1], data.Type, data.Target);

                var foodData = game.FoodList.FirstOrDefault(x => x.Type == data.FoodType);
                bubbleData.Component.ImageList[0].sprite = foodData.Icon;

                bubbleData.Component.TextList[0].text = "x" + data.Amount;

                if (data.Amount <= 0) BubbleDestroy(bubbleData);

                break;

            case BubbleUIType.Unlock:
                if (bubbleData == null && data.Amount <= 0) return;
                if (bubbleData == null) BubbleCreate(bubblePrefabList[2], data.Type, data.Target);

                bubbleData.Component.TextList[0].text = data.Amount.ToString();

                if (data.Amount <= 0) BubbleDestroy(bubbleData);

                break;
        }
    }
    void BubbleCreate(GameObject prefab, BubbleUIType type, Transform target)
    {
        GameObject newBubble = Instantiate(prefab, screen.transform);

        Vector3 newPosition = Camera.main.WorldToScreenPoint(target.position + adjustPosition);
        newBubble.transform.position = newPosition;

        bubbleData = new BubbleUIData()
        {
            Type = type,
            Transform = newBubble.transform,
            Component = newBubble.GetComponent<BubbleUIComponent>(),
            Target = target
        };
        bubbleList.Add(bubbleData);
    }
    void BubbleDestroy(BubbleUIData bubble)
    {
        bubbleList.Remove(bubble);
        Destroy(bubble.Transform.gameObject);
    }
}