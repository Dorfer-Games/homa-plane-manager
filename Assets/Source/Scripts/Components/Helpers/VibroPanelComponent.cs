using Kuhpik;
using static My;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;
using Supyrb;
using System.Linq;

public class VibroPanelComponent : MonoBehaviour
{
    bool show;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void Hide()
    {
        show = !show;
        gameObject.SetActive(show);
    }
}
