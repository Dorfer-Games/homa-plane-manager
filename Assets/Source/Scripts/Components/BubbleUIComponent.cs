using NaughtyAttributes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BubbleUIComponent : MonoBehaviour
{
    [SerializeField, BoxGroup("Text")] List<TMP_Text> textList;

    [SerializeField, BoxGroup("Icon")] List<Image> imageList;

    public List<TMP_Text> TextList => textList;
    public List<Image> ImageList => imageList;
}