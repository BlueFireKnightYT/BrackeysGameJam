using UnityEngine;

public class CanvasElementActions : MonoBehaviour
{
    public GameObject upgradeMenu;
    public GameObject upgradeButton;
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
        if(source.isPlaying == audioScript.MenuSwoosh)
        {
            source.volume = 0.5f;
        }
        else
        {
            source.volume = 1f;
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
        if (CurrencyHandler.baconAmount >= 5)
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
        audioScript.PlayButtonClick();
        audioScript.PlayMenuSwoosh();
        menuOpen = false;
        upgradeButton.SetActive(true);
    }
}
