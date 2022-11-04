using Kuhpik;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameSettings : GameSystem
{
    [SerializeField] GameObject SettingsPanel;
    [SerializeField] Button ShowButton;
    [SerializeField] Button ExitButton;
    [SerializeField] Button MusicButton;
    [SerializeField] Button SoundButton;
    [SerializeField] Button VibroButton;

    [Header("Settings Button")]
    [SerializeField] Sprite SettingsSpriteClosed;
    [SerializeField] Sprite SettingsSpriteOpened;

    [Header("Buttons sprites")]
    [SerializeField] Sprite MusicButtonOn;
    [SerializeField] Sprite MusicButtonOff;
    [SerializeField] Sprite SoundButtonOn;
    [SerializeField] Sprite SoundButtonOff;
    [SerializeField] Sprite VibroButtonOn;
    [SerializeField] Sprite VibroButtonOff;

    [Header("Mixers")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioMixer musicMixer;

    const float volumeMuteValue = -80f; // this is a minimal volume value to mute for audio mixers
    public override void OnInit()
    {
        if (SettingsPanel)
        {
            if (MusicButton) MusicButton.onClick.AddListener(SetMusic);
            if (SoundButton) SoundButton.onClick.AddListener(SetSound);
            if (VibroButton) VibroButton.onClick.AddListener(SetVibro);
            ShowButton.onClick.AddListener(ShowPanel);
            if (ExitButton) ExitButton.onClick.AddListener(ShowPanel);
            Set();
        }
    }
    void ShowPanel()
    {
        SettingsPanel.SetActive(!SettingsPanel.activeInHierarchy);
        Set();
    }
    void Set()
    {
        if (SoundButton)
            SoundButton.GetComponent<Image>().sprite = player.SoundOn ? SoundButtonOn : SoundButtonOff;
        if (MusicButton)
            MusicButton.GetComponent<Image>().sprite = player.MusicOn ? MusicButtonOn : MusicButtonOff;
        if (VibroButton)
            VibroButton.GetComponent<Image>().sprite = player.VibroOn ? VibroButtonOn : VibroButtonOff;
        ShowButton.GetComponent<Image>().sprite = !SettingsPanel.activeInHierarchy ? SettingsSpriteClosed : SettingsSpriteOpened;

        audioMixer.SetFloat("Volume", player.SoundOn ? 0f : volumeMuteValue);
        musicMixer.SetFloat("Volume", player.MusicOn ? 0f : volumeMuteValue);
        Bootstrap.Instance.SaveGame();
    }
    void SetVibro()
    {
        player.VibroOn = !player.VibroOn;
        Set();
    }
    void SetSound()
    {
        player.SoundOn = !player.SoundOn;
        Set();
    }
    void SetMusic()
    {
        player.MusicOn = !player.MusicOn;
        Set();
    }
}
