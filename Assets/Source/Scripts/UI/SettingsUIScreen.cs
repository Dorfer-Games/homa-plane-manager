using Kuhpik;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUIScreen : UIScreen
{
    [SerializeField, BoxGroup("Button")] Button settingsButton;
    [SerializeField, BoxGroup("Button")] Button vibrationsButton;

    [SerializeField, BoxGroup("Image")] Image vibrationsImage;

    [SerializeField, BoxGroup("GameObject")] GameObject settingsBar;

    [SerializeField, BoxGroup("Sprite")] List<Sprite> vibrationsSprite;


    public Button SettingsButton => settingsButton;
    public Button VibrationsButton => vibrationsButton;
    public Image VibrationsImage => vibrationsImage;
    public GameObject SettingsBar => settingsBar;
    public List<Sprite> VibrationsSprite => vibrationsSprite;
}