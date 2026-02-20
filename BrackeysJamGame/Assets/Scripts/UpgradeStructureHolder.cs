using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStructureHolder : MonoBehaviour
{
    public UpgradeStructure structure;
    public Image image;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI nameText;

    public void Awake()
    {
        structure.basePrice = structure.realPrice;
    }
    private void Start()
    {
        image.sprite = structure.upgradeImage;
        priceText.text = structure.realPrice.ToString();
        nameText.text = structure.structureName.ToString();
    }

    public void UpdateText()
    {
        structure.realPrice = Mathf.RoundToInt(Mathf.Pow(structure.realPrice, 1.1f));
        image.sprite = structure.upgradeImage;
        priceText.text = structure.realPrice.ToString();
        nameText.text = structure.structureName.ToString();
    }
}
