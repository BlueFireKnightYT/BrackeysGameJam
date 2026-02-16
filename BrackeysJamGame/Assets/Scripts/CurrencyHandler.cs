using TMPro;
using UnityEngine;

public class CurrencyHandler : MonoBehaviour
{
    public static int baconAmount;
    public TextMeshProUGUI baconText;

    private void Update()
    {
        baconText.text = baconAmount.ToString();
    }
}
