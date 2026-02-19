using UnityEngine;
using UnityEngine.UI;

public class CanvasElementActions : MonoBehaviour
{
    public GameObject upgradeMenu;
    public GameObject upgradeButton;
    public BuildMode buildMode;
    public GameObject buildPanel;
    public RawImage buildPanelImage;
    public GameObject sacrificeCirclePrefab;
    public GameObject carrotDummyPrefab;

    public RectTransform upgradeMenuRect;
    public RectTransform destroyMenuRect;

    Vector2 destination = new Vector2(0, 0);
    Vector2 startPos = new Vector2(-650, 0);
    Vector2 startPos2 = new Vector2(0, 1080);
    Vector2 cursorWorldPos;
    Vector2 spawnPos;

    AudioScript audioScript;
    GameObject sfxManager;
    AudioSource source;

    bool menuOpen = false;
    bool destroyMenuOpen = false;
    [SerializeField] float menuLerpSpeed = 8f;

    private void Start()
    {
        buildPanelImage = buildPanel.GetComponent<RawImage>();
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
        //2
        Vector2 target2 = destroyMenuOpen ? destination : startPos2;
        destroyMenuRect.anchoredPosition = Vector2.Lerp(destroyMenuRect.anchoredPosition, target2, Time.deltaTime * menuLerpSpeed);

        if (Vector2.Distance(destroyMenuRect.anchoredPosition, target2) < 0.1f)
        {
            destroyMenuRect.anchoredPosition = target2;
        }
    }

    public void OpenMenu()
    {
        audioScript.PlayButtonClick();
        audioScript.PlayMenuSwoosh();
        menuOpen = true;
        upgradeButton.SetActive(false);
    }

    public void DisableMenu()
    {
        audioScript.PlayButtonClick();
        audioScript.PlayMenuSwoosh();
        menuOpen = false;
        upgradeButton.SetActive(true);
    }

    public void OpenDestroyMenu()
    {
        menuOpen = false;
        destroyMenuOpen = true;
        audioScript.PlayButtonClick();
        audioScript.PlayMenuSwoosh();
        buildMode.enabled = true;
    }

    public void CloseDestroyMenu()
    {
        menuOpen = true;
        destroyMenuOpen = false;
        audioScript.PlayButtonClick();
        audioScript.PlayMenuSwoosh();
        buildMode.enabled = false;

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



    public void EnableDestroying()
    {
        LowerAlpha();
        if (buildMode.isMoving == false)
        {
            if (buildMode.isDestroying == true) buildMode.isDestroying = false;
            else buildMode.isDestroying = true;
        }
        else
        {
            buildMode.isMoving = false;
            buildMode.isDestroying = true;
        }
    }

    public void EnableMoving()
    {
        LowerAlpha();
        if (buildMode.isDestroying == false)
        {
            if (buildMode.isMoving == true) buildMode.isMoving = false;
            else buildMode.isMoving = true;
        }
        else
        {
            buildMode.isMoving = true;
            buildMode.isDestroying = false;
        }
    }

    public void NormalAlpha()
    {
        Color c = buildPanelImage.color;
        c.a = 0.18f;
        buildPanelImage.color = c;
    }
    public void LowerAlpha()
    {
        Color c = buildPanelImage.color;
        c.a = 0.01f;
        buildPanelImage.color = c;
    }


}
