
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class CellProgressBarComponent : MonoBehaviour
{
    [SerializeField] Image[] ImageCells;
    [SerializeField] bool usePresetColors = true;
    [HideIf(EConditionOperator.And, "usePresetColors")] [SerializeField] List<Color> colors = new List<Color>();
    [SerializeField] int testNum;

    private void Start()
    {
        if (usePresetColors)
        {
            Color presetColor = new Color();
            string[] presetColors = new string[] { "#FFFFFF", "#7395FB", "#66CD46", "#FBF27B", "#FF9355", "#D33F6A", "#CD81F0", "#6EEDEF" };
            foreach (var item in presetColors)
                if (ColorUtility.TryParseHtmlString(item, out presetColor))
                    colors.Add(presetColor);
        }
    }

    public void Set(int num)
    {
        num = num % (colors.Count * ImageCells.Length);
        for (int i = 0; i < ImageCells.Length; i++)
            if (i < num % ImageCells.Length)
            {
                int Length = Mathf.CeilToInt((num / ImageCells.Length) + 1);
                int index = Length >= colors.Count ? 0 : Length;
                ImageCells[i].color = colors[index];
            }
            else ImageCells[i].color = colors[Mathf.CeilToInt(num / ImageCells.Length)];
    }

    [Button] public void Test()
    {
        Set(testNum);
    }
}
