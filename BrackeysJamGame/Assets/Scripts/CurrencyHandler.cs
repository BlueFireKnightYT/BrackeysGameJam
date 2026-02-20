using TMPro;
using UnityEngine;

public class CurrencyHandler : MonoBehaviour
{
    public static int baconAmount;
    public TextMeshProUGUI baconText;
    public static int pigsSacrificed;
    public TextMeshProUGUI sacrificedText;

    private void Update()
    {
        if (baconAmount < 1000)
        baconText.text = baconAmount.ToString();
        else if(baconAmount >= 1000 && baconAmount < 1000000)
        {
            float baconAmountK = baconAmount / 1000f;
            baconText.text = baconAmountK.ToString("F2") + "K";
        }
        else if(baconAmount >= 1000000)
        {
            float baconAmountM = baconAmount / 1000000f;
            baconText.text = baconAmountM.ToString("F2") + "M";
        }

        sacrificedText.text = pigsSacrificed.ToString();
    }
}
