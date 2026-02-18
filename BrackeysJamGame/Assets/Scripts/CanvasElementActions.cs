using UnityEngine;

public class CanvasElementActions : MonoBehaviour
{
    public GameObject upgradeMenu;
    public GameObject upgradeButton;
    public GameObject sacrificeCirclePrefab;
    public GameObject carrotDummyPrefab;
    Vector2 cursorWorldPos;
    Vector2 spawnPos;

    private void Update()
    {
        cursorWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPos = cursorWorldPos - new Vector2(0, 1f);
    }
    public void UpgradeButtonPressed()
    {
        upgradeMenu.SetActive(true);
        upgradeButton.SetActive(false);
    }

    public void PurchaseSacrificeCircle()
    {
        if(CurrencyHandler.baconAmount >= 5)
        {
            GameObject obj = Instantiate(sacrificeCirclePrefab, spawnPos, Quaternion.identity);
            CurrencyHandler.baconAmount -= 5;
            obj.GetComponent<ObjectDragger>().beenBought = true;
            DisableMenu();
        }   
    }

    public void PurchaseCarrotDummy()
    {
        if (CurrencyHandler.baconAmount >= 10)
        {
            GameObject obj = Instantiate(carrotDummyPrefab, spawnPos, Quaternion.identity);
            CurrencyHandler.baconAmount -= 10;
            obj.GetComponent<ObjectDragger>().beenBought = true;
            DisableMenu();
        }
    }

    public void DisableMenu()
    {
        upgradeMenu.SetActive(false);
        upgradeButton.SetActive(true);
    }
}
