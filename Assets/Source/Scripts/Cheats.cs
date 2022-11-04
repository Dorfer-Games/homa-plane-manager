using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static My;

public class Cheats : GameSystem
{
    CheatsPanelComponent CheatsPanel;
    [SerializeField] List<string> UITargetsClicked = new List<string>();

    GameObject Canvas;

    bool CheatsHided;
    Dictionary<int, float> gameSpeeds = new Dictionary<int, float>() { { 1, 2f }, { 2, 0.3f } };
    int CurGameSpeedIndex;

    public void PreStart()
    {
        CheatsPanel = FindObjectOfType<CheatsPanelComponent>(true);
        CheatsPanel.gameObject.SetActive(false);
        gameSpeeds.Add(0, Time.timeScale);
        Canvas = FindObjectOfType<UIManager>().gameObject;
        CheatsPanel.NoAdsButtonText.text = "ADS " + (player.Ads ? "ON" : "OFF");
        CheatsPanel.CheatsSwitcherButton.onClick.AddListener(CheatsSwitcher);
        Signals.Get<SignalOpenCheatPanel>().AddListener(CheatsPanelToggle);
    }
    void CheatsPanelToggle(bool status)
    {
        CheatsPanel.gameObject.SetActive(status);
    }
    public void Restart()
    {
        Bootstrap.Instance.GameRestart(0);
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    public void Money()
    {
        player.Money += 10000;
    }

    public void GameSpeed()
    {
        CurGameSpeedIndex++;
        Time.timeScale = gameSpeeds[CurGameSpeedIndex % gameSpeeds.Count];
        if (CheatsPanel.GameFastSpeedText)
            CheatsPanel.GameFastSpeedText.text = Time.timeScale.ToString("") + "x";
    }

    public void NoAds()
    {
        player.Ads = !player.Ads;
        CheatsPanel.NoAdsButtonText.text = "ADS " + (player.Ads ? "ON" : "OFF");
    }

    void CheatsSwitcher()
    {
        CheatsHided = !CheatsHided;
        for (int i = 1; i < CheatsPanel.transform.childCount; i++)
            CheatsPanel.transform.GetChild(i).transform.localScale = CheatsHided ? Vector3.zero : Vector3.one;
        CheatsPanel.CheatsSwitcherButton.GetComponentInChildren<Text>().color = CheatsHided ? new Color(1, 1, 1, 0) : Color.yellow;
    }
    public void Save()
    {
        Bootstrap.Instance.SaveGame();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            if (Canvas.activeSelf)
                Canvas.SetActive(false);
            else Canvas.SetActive(true);

        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            UITargetsClicked.Clear();
            foreach (var item in results)
                UITargetsClicked.Add(item.ToString());
        }
    }
}
