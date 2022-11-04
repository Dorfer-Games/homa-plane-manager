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

public class HapticSetupSystem : GameSystem
{
    public static HapticSetupSystem instance;
    [SerializeField] VibroSetupButtonComponent ButtonPrefab;
    Dictionary<string, VibroSetupButtonComponent> dictNameButton = new Dictionary<string, VibroSetupButtonComponent>();
    VibroPanelComponent screen;
    public override void OnInit()
    {
        instance = this;
        screen = FindObjectOfType<VibroPanelComponent>(true);
    }
    public void Haptic(string Name, MoreMountains.NiceVibrations.HapticTypes type, float cd = 0)
    {
        if (!dictNameButton.ContainsKey(Name))
            AddHaptic(Name, type, cd);

        if (player.VibroOn)
            dictNameButton[Name].Vibro();
    }
    void AddHaptic(string Name, MoreMountains.NiceVibrations.HapticTypes type, float cd)
    {
        VibroSetupButtonComponent newButton = Instantiate(ButtonPrefab, screen.transform);
        newButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -100 * (screen.transform.childCount - 1));
        newButton.vibroName = Name;
        newButton.type = type;
        newButton.Prestart(cd);
        dictNameButton.Add(newButton.vibroName, newButton);
    }
    private void Update()
    {
        foreach (var item in dictNameButton)
            if (item.Value.timer > 0) item.Value.timer -= Time.deltaTime;
    }
}

