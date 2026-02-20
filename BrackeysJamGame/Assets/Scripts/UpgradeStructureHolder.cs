using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStructureHolder : MonoBehaviour
{
    public UpgradeStructure structure;
    public Image image;
    public TextMeshProUGUI remainingSacrifices;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI nameText;
    public bool unlocked;

    public void Awake()
    {
        structure.realPrice = structure.basePrice;
        image.color = new Color32(50, 50, 50, 100);
        unlocked = false;
        remainingSacrifices.enabled = true;
        priceText.enabled = false;
        nameText.enabled = false;
    }

    private void Update()
    {
        if(CurrencyHandler.pigsSacrificed >= structure.unlockAmount)
        {
            image.color = new Color32(255, 255, 255, 255);
            unlocked = true;
            priceText.enabled = true;
            nameText.enabled = true;
            remainingSacrifices.enabled = false;
        }
        if (!unlocked)
        {
            remainingSacrifices.text = (structure.unlockAmount - CurrencyHandler.pigsSacrificed).ToString();
        }
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
