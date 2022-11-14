using Kuhpik;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class ResourceUIScreen : UIScreen
{
    [SerializeField] [BoxGroup("Resources")] TMP_Text moneyText;

    public TMP_Text MoneyText => moneyText;
}