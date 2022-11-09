using UnityEngine;
using Supyrb;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SignalOpenCheatPanel : Signal<bool> { }

public class DebugConsoleOpener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] string password = "7458";
    [SerializeField] int secsToOpen = 8;
    [SerializeField] List<GameObject> CheatsObjects = new List<GameObject>();
    [SerializeField] bool Closeable = true;

    TouchScreenKeyboard keyboard;
    float timer;
    bool cheatsOpened;
    bool cheatsUnlocked;
    bool touched;
    private void Start()
    {
        foreach (var item in CheatsObjects)
            item.SetActive(false);
        Signals.Get<SignalOpenCheatPanel>().Dispatch(false);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        touched = true;

        if (cheatsUnlocked)
        {
            cheatsOpened = !cheatsOpened;
            Signals.Get<SignalOpenCheatPanel>().Dispatch(Closeable ? cheatsOpened : true);
            foreach (var item in CheatsObjects)
                item.SetActive(Closeable ? cheatsOpened : true);
        }
    }

    void Update()
    {
        timer = touched ? timer + Time.deltaTime : 0;

        if (timer >= secsToOpen && !cheatsUnlocked && keyboard == null)
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumberPad);

        if (keyboard != null && keyboard.text.Length == 4 && keyboard.text == password)
        {
            keyboard.active = false;
            keyboard = null;
            cheatsUnlocked = true;
            cheatsOpened = true;
            Signals.Get<SignalOpenCheatPanel>().Dispatch(Closeable ? cheatsOpened : true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        touched = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        touched = false;
    }
}