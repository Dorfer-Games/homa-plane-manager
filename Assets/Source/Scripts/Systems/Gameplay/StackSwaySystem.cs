using Kuhpik;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class StackSwaySystem : GameSystem
{
    [SerializeField, BoxGroup("Settings")] int minStackHeight = 3;
    [SerializeField, BoxGroup("Settings")] float integrityPerItem = 0.15f;

    [SerializeField, BoxGroup("Sway")] float swayLerp = 2f;
    [SerializeField, BoxGroup("Sway")] float swaySpeed = 1f;
    [SerializeField, BoxGroup("Sway")] float swayShake = 0.10f;
    [SerializeField, BoxGroup("Sway")] float swayStrength = 2f;

    float swayId;
    public override void OnInit()
    {
        swayId = Random.Range(0f, 1024f);
    }
    public override void OnUpdate()
    {
        // Stack height is valid
        List<ItemComponent> stackContents = game.PlayerItemList;
        if (stackContents.Count > minStackHeight)
        {
            float scale = 1f;

            // Looping every item in the stack
            int i = 0;
            for (int j = stackContents.Count - 1; j >= 0; j--)
            {
                // Get
                float integrity = Mathf.Clamp((stackContents.Count - i) * integrityPerItem, 0f, 0.5f);
                float swayPositionSpeed = swayId + (Time.time * swaySpeed);

                // Float
                float xPosition = (Mathf.Sin(swayPositionSpeed) * integrity * swayStrength) * scale;
                float yPosition = (Mathf.Cos(swayPositionSpeed) * integrity * swayStrength) * scale;

                // Shake
                xPosition += Random.Range(-swayShake, swayShake) * scale;
                yPosition += Random.Range(-swayShake, swayShake) * scale;

                // Setup
                Vector3 newPosition = new Vector3(xPosition, 0f, yPosition);
                Vector3 lerpPosition = Vector3.Lerp(stackContents[j].transform.localPosition, newPosition, (scale * swayLerp * Time.deltaTime * integrity));
                stackContents[j].transform.localPosition = lerpPosition;

                // Count
                i++;
            }
        }
    }
}
