using UnityEngine;

public class CanvasElementActions : MonoBehaviour
{
    public GameObject upgradeMenu;
    public GameObject upgradeButton;
    public GameObject buildModePanel;
    public GameObject sacrificeCirclePrefab;
    public GameObject carrotDummyPrefab;

    RectTransform upgradeMenuRect;

    Vector2 destination = new Vector2(0, 0);
    Vector2 startPos = new Vector2(-650, 0);
    Vector2 cursorWorldPos;
    Vector2 spawnPos;

    AudioScript audioScript;
    GameObject sfxManager;
    AudioSource source;

    bool menuOpen;
    [SerializeField] float menuLerpSpeed = 8f;

    private void Start()
    {
        upgradeMenuRect = upgradeMenu.GetComponent<RectTransform>();
        upgradeMenuRect.anchoredPosition = startPos;
        sfxManager = GameObject.FindGameObjectWithTag("sfxManager");
        audioScript = sfxManager.GetComponent<AudioScript>();
        source = sfxManager.GetComponent<AudioSource>();
        menuOpen = false;
    }

    private void Update()
    {
        cursorWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPos = cursorWorldPos - new Vector2(0, 1f);

        Vector2 target = menuOpen ? destination : startPos;
        upgradeMenuRect.anchoredPosition = Vector2.Lerp(upgradeMenuRect.anchoredPosition, target, Time.deltaTime * menuLerpSpeed);

        if (Vector2.Distance(upgradeMenuRect.anchoredPosition, target) < 0.1f)
        {
            upgradeMenuRect.anchoredPosition = target;
        }
    }

    public void UpgradeButtonPressed()
    {
        audioScript.PlayButtonClick();
        audioScript.PlayMenuSwoosh();
        menuOpen = true;
        upgradeButton.SetActive(false);
    }

    public void PurchaseSacrificeCircle()
    {
        int price = sacrificeCirclePrefab.GetComponent<ObjectDragger>().buyingPrice;
        if (CurrencyHandler.baconAmount >= price)
        {
            GameObject obj = Instantiate(sacrificeCirclePrefab, spawnPos, Quaternion.identity);
            CurrencyHandler.baconAmount -= price;
            obj.GetComponent<ObjectDragger>().beenBought = true;
            DisableMenu();
        }
    }

    public void PurchaseCarrotDummy()
    {
        int price = carrotDummyPrefab.GetComponent<ObjectDragger>().buyingPrice;
        if (CurrencyHandler.baconAmount >= price)
        {
            GameObject obj = Instantiate(carrotDummyPrefab, spawnPos, Quaternion.identity);
            CurrencyHandler.baconAmount -= price;
            obj.GetComponent<ObjectDragger>().beenBought = true;
            DisableMenu();
        }
    }

    public void OpenDestroyMenu()
    {
        menuOpen = false;
        buildModePanel.SetActive(true);
    }

    public void CloseDestroyMenu()
    {
        menuOpen = true;
        buildModePanel.SetActive(false);
    }

    public void EnableDestroying()
    {
        BuildMode script = buildModePanel.GetComponent<BuildMode>();
        if (script.isMoving == false)
        {
            if (script.isDestroying == true) script.isDestroying = false;
            else script.isDestroying = true;
        }
        else
        {
            script.isMoving = false;
            script.isDestroying = true;
        }
    }

    public void EnableMoving()
    {
        BuildMode script = buildModePanel.GetComponent<BuildMode>();
        if (script.isDestroying == false)
        {
            if (script.isMoving == true) script.isMoving = false;
            else script.isMoving = true;
        }
        else
        {
            script.isMoving = true;
            script.isDestroying = false;
        }
    }

    public void DisableMenu()
    {
        audioScript.PlayButtonClick();
        audioScript.PlayMenuSwoosh();
        menuOpen = false;
        upgradeButton.SetActive(true);
    }
}
