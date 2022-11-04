
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;

public class LineProgressBarComponent : MonoBehaviour
{
    [InfoBox("No Children but Bar")]
    [SerializeField] Image ProgressBarImage;
    [SerializeField] int cellsCount = 5;
    [SerializeField] float fillTime = 1f;
    [SerializeField] List<Color> colors = new List<Color>();

    List<Image> Bars = new List<Image>();
    int colorIndex;
    float percent;
    int BarIndex;
    bool preseted;
    void PreSet()
    {
        if (preseted) return;
        preseted = true;
        for (int i = 0; i < 2; i++)
            Bars.Add(Instantiate(ProgressBarImage, ProgressBarImage.transform.position, ProgressBarImage.transform.rotation, ProgressBarImage.transform.parent));
        
        Bars[0].transform.SetSiblingIndex(1);
        Bars[1].transform.SetSiblingIndex(2);
        Bars[0].fillAmount = 0;
        Bars[1].fillAmount = 0;

        if (colors.Count == 0)
        {
            string[] presetColors = new string[] { "#7395FB", "#66CD46", "#D33F6A", "#FBF27B", "#FF77FF", "#FF9355", "#6EEDEF", "#B870FF" };
            foreach (var item in presetColors)
                if (ColorUtility.TryParseHtmlString(item, out Color presetColor))
                    colors.Add(presetColor);
        }
    }

    public void Set(int num, bool smooth = true)
    {
        PreSet();
        ColorIndex(num);
        ProgressBarImage.fillAmount = num >= cellsCount * 2 ? 1f : 0f;
        ProgressBarImage.color = GetColor(colorIndex - 2);

        percent = num % cellsCount == 0 && num > 0 ? 1f : (float)(num % cellsCount) / (float)cellsCount;

        BarIndex = Mathf.CeilToInt(((float)num) / (float)cellsCount) % 2;
        Bars[BarIndex].color = GetColor(colorIndex);
        Bars[BarIndex].DOKill();
        Bars[BarIndex].fillAmount = (num % cellsCount == 1) ? 0 : Bars[BarIndex].fillAmount;
        Bars[BarIndex].DOFillAmount(percent, smooth ? fillTime : 0);
        Bars[BarIndex].transform.SetSiblingIndex(2);

        Bars[(BarIndex + 1) % 2].color = GetColor(colorIndex - 1);
        Bars[(BarIndex + 1) % 2].transform.SetSiblingIndex(1);
        Bars[(BarIndex + 1) % 2].fillAmount = (!smooth && num > 5) ? 1 : Bars[(BarIndex + 1) % 2].fillAmount;
    }
    void ColorIndex(int num)
    {
        int colorsLength = Mathf.CeilToInt((((Mathf.Max(0, num - 1) % (colors.Count * cellsCount))) / cellsCount));
        colorIndex =  colorsLength >= colors.Count ? 0 : colorsLength;
    }

    Color GetColor(int index)
    {
        return colors[index < 0 ? colors.Count + index : index % colors.Count];
    }
}
