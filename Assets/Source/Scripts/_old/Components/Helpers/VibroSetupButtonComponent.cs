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

public class VibroSetupButtonComponent : MonoBehaviour
{
    public string vibroName;
    public MoreMountains.NiceVibrations.HapticTypes type;
    public float timer;

    [SerializeField] Text vibroNameText;
    [SerializeField] Text CDText;
    [SerializeField] Button changeType;
    [SerializeField] Button CDPlus;
    [SerializeField] Button CDMinus;
    [SerializeField] float CD;
    
    public void Vibro()
    {
        if(timer<=0)
        {
            timer = CD;
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(type);
        }
    }
    public void Prestart(float cd)
    {
        CD = cd;
        CDPlus.onClick.AddListener(PlusCD);
        CDMinus.onClick.AddListener(MinusCD);
        changeType.onClick.AddListener(ChangeType);
        Texts();
    }
    void ChangeType()
    {
        int newtype = (int)type + 1;
        if (newtype == System.Enum.GetValues(typeof(MoreMountains.NiceVibrations.HapticTypes)).Length)
            newtype = 0;
        type = (MoreMountains.NiceVibrations.HapticTypes)newtype;
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(type);
        Texts();
    }
    void Texts()
    {
        vibroNameText.text = vibroName + "\n" + type.ToString();
        CDText.text = CD.ToString("F2");
    }

    void PlusCD()
    {
        CD += 0.01f;
        Texts();
    }

    void MinusCD()
    {
        CD = Mathf.Max(0, CD - 0.01f);
        Texts();
    }
}

