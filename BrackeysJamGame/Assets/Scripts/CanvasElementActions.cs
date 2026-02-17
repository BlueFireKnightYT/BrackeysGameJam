using UnityEngine;

public class CanvasElementActions : MonoBehaviour
{
    public GameObject upgradeMenu;
    public GameObject sacrificeCirclePrefab;
    Vector2 cursorWorldPos;
    Vector2 spawnPos;

    private void Update()
    {
        cursorWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPos = cursorWorldPos - new Vector2(0, 1f);
    }
    public void UpgradeButtonPressed()
    {
        if(upgradeMenu.activeSelf == true)
        {
            upgradeMenu.SetActive(false);
        } else upgradeMenu.SetActive(true);
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

    public void DisableMenu()
    {
        upgradeMenu.SetActive(false);
    }
}
