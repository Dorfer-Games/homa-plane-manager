using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

public class LineProgressBarTesterComponent : MonoBehaviour
{
    [InfoBox("press SPACE for smooth +1")]
    [SerializeField] LineProgressBarComponent bar;
    [InfoBox("press CTRL for nosmooth set NUM")]
    [SerializeField] int NUM = 0;
    [SerializeField] Text text;

    private void Start()
    {
        if (!bar)
            bar = FindObjectOfType<LineProgressBarComponent>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Add();
        if (Input.GetKeyDown(KeyCode.LeftControl))
            Set();
    }
    [Button] public void Add()
    {
        NUM++;
        text.text = NUM.ToString();
        bar.Set(NUM);
    }
    [Button] public void Set()
    {
        text.text = NUM.ToString();
        bar.Set(NUM);
    }
}
