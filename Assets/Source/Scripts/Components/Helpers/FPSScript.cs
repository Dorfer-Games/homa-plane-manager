using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSScript : MonoBehaviour
{
    Text txt;
    TMPro.TextMeshProUGUI TMtext;
    int FPS;
    [SerializeField] bool TimeShow;
    void Start()
    {
        TryGetComponent<Text>(out txt);
        TryGetComponent<TMPro.TextMeshProUGUI>(out TMtext);
    }
    // Update is called once per frame
    void Update()
    {
        if (TimeShow)
        {
            if (txt) txt.text = My.TimeMinSec(Time.time, true).ToString();
            if (TMtext) TMtext.text = My.TimeMinSec(Time.time, true).ToString();
            return;
        }

        if (FPS < 10 * (int)(1f / Time.deltaTime))
            FPS++;
        if (FPS > 10 * (int)(1f / Time.deltaTime))
            FPS--;

        if (txt) txt.text = ((int)(FPS / 10)).ToString();
        if (TMtext) TMtext.text = ((int)(FPS / 10)).ToString();
    }
}
