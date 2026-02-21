using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasElementActions : MonoBehaviour
{
    [Header("Buttons")]
    public GameObject upgradeButton;
    public GameObject settingsButton;

    [Header("Prefabs")]
    public GameObject sacrificeCirclePrefab;
    public GameObject carrotDummyPrefab;
    public GameObject candlesUpgradePrefab;
    public GameObject fireUpgradePrefab;
    public GameObject pigRainerPrefab;

    [Header("Vectors")]
    Vector2 destination = new Vector2(0, 0);
    Vector2 startPos = new Vector2(-650, 0);
    Vector2 startPos2 = new Vector2(0, 1080);
    Vector2 cursorWorldPos;
    Vector2 spawnPos;


    [Header("Menu's")]

    [SerializeField] float menuLerpSpeed = 8f;

    public GameObject upgradeMenu;

    public BuildMode buildMode;
    public GameObject buildPanel;
    public GameObject QuitMenu;
    public RawImage buildPanelImage;

    public RectTransform upgradeMenuRect;
    public RectTransform destroyMenuRect;
    public RectTransform settingsMenuRect;

    bool menuOpen = false;
    bool destroyMenuOpen = false;
    bool settingsMenuOpen = false;

    [Header("Settings")]

    public Slider MusicVolumeSlider;

    [Header("Rest")]
    public LayerMask objectLayer;
    public static bool canDragPigs;
    public static bool canActivateMenu;

    public AudioScript audioScript;
    public GameObject sfxManager;



    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            buildPanelImage = buildPanel.GetComponent<RawImage>();
            upgradeMenuRect = upgradeMenu.GetComponent<RectTransform>();
            upgradeMenuRect.anchoredPosition = startPos;
            sfxManager = GameObject.FindGameObjectWithTag("sfxManager");
            audioScript = sfxManager.GetComponent<AudioScript>();
            menuOpen = false;
            canDragPigs = true;
            canActivateMenu = true;
        }
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Game")
        { 
            cursorWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPos = cursorWorldPos - new Vector2(0, 1f);

            Vector2 target = menuOpen ? destination : startPos;
            upgradeMenuRect.anchoredPosition = Vector2.Lerp(upgradeMenuRect.anchoredPosition, target, Time.deltaTime * menuLerpSpeed);

            if (Vector2.Distance(upgradeMenuRect.anchoredPosition, target) < 0.1f)
            {
                upgradeMenuRect.anchoredPosition = target;
            }
            //destroy menu
            Vector2 target2 = destroyMenuOpen ? destination : startPos2;
            destroyMenuRect.anchoredPosition = Vector2.Lerp(destroyMenuRect.anchoredPosition, target2, Time.deltaTime * menuLerpSpeed);

            if (Vector2.Distance(destroyMenuRect.anchoredPosition, target2) < 0.1f)
            {
                destroyMenuRect.anchoredPosition = target2;
            }
            //settings menu
            Vector2 target3 = settingsMenuOpen ? destination : startPos2;
            settingsMenuRect.anchoredPosition = Vector2.Lerp(settingsMenuRect.anchoredPosition, target3, Time.deltaTime * menuLerpSpeed);

            if (Vector2.Distance(settingsMenuRect.anchoredPosition, target3) < 0.1f)
            {
                settingsMenuRect.anchoredPosition = target3;
            }
        }
    }

    public void OpenMenu()
    {
        if (canActivateMenu)
        {
            audioScript.PlayButtonClick();
            audioScript.PlayMenuSwoosh();
            menuOpen = true;
            settingsMenuOpen = false;
            destroyMenuOpen = false;
            upgradeButton.SetActive(false);
        }
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
        settingsMenuOpen = false;
        destroyMenuOpen = true;

        audioScript.PlayButtonClick();
        audioScript.PlayMenuSwoosh();
        buildMode.enabled = true;
        canDragPigs = false;
    }

    public void CloseDestroyMenu()
    {
        menuOpen = true;
        destroyMenuOpen = false;
        settingsMenuOpen = false;

        audioScript.PlayButtonClick();
        audioScript.PlayMenuSwoosh();
        if (buildMode.isMoving || buildMode.isDestroying) DeHighlightObjects();
        buildMode.enabled = false;
        canDragPigs = true;
    }

    public void OpenSettingsMenu()
    {
        settingsMenuOpen = true;
        destroyMenuOpen = false;
        menuOpen = false;

        audioScript.PlayButtonClick();
        audioScript.PlayMenuSwoosh();

        settingsButton.SetActive(false);
        upgradeButton.SetActive(false);
    }

    public void CloseSettingsMenu()
    {
        settingsMenuOpen = false;
        settingsButton.SetActive(true);
        upgradeButton.SetActive(true);

        audioScript.PlayButtonClick();
        audioScript.PlayMenuSwoosh();

        if (buildMode.isMoving || buildMode.isDestroying) DeHighlightObjects();
        buildMode.enabled = false;
        canDragPigs = true;
    }

    public void ChangeMusicVolume()
    {
        audioScript.musicSource.volume = MusicVolumeSlider.value;
    }

    public void EnableDestroying()
    {
        if (buildMode.isMoving == false)
        {
            if (buildMode.isDestroying == true)
            {
                buildMode.isDestroying = false;
                DeHighlightObjects();
                NormalAlpha();
            }
            else
            {
                buildMode.isDestroying = true;
                HighlightDeleteObjects();
                LowerAlpha();
            }
        }
        else
        {
            HighlightDeleteObjects();
            buildMode.isMoving = false;
            buildMode.isDestroying = true;
        }
    }

    public void EnableMoving()
    {
        
        if (buildMode.isDestroying == false)
        {
            if (buildMode.isMoving == true)
            {
                buildMode.isMoving = false;
                DeHighlightObjects();
                NormalAlpha();
            }
            else
            {
                if(CurrencyHandler.baconAmount >= 5)
                {
                    buildMode.isMoving = true;
                    HighlightMoveObjects();
                    LowerAlpha();
                }
            }
        }
        else
        {
            buildMode.isMoving = true;
            HighlightMoveObjects();
            buildMode.isDestroying = false;
        }
    }

    public void NormalAlpha()
    {
        Color c = buildPanelImage.color;
        c.a = 0.10f;
        buildPanelImage.color = c;
    }
    public void LowerAlpha()
    {
        Color c = buildPanelImage.color;
        c.a = 0.01f;
        buildPanelImage.color = c;
    }

    public void HighlightMoveObjects()
    {
        GameObject[] all = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject go in all)
        {
            if (((1 << go.layer) & objectLayer) != 0)
            {
                Transform objTransform = go.GetComponent<Transform>();
                go.GetComponent<SpriteRenderer>().color = Color.green;
                if (buildMode.isDestroying == false)
                {
                    objTransform.localScale = new Vector2(objTransform.localScale.x * 1.2f, objTransform.localScale.y * 1.2f);
                }      
            }
        }
    }

    public void HighlightDeleteObjects()
    {
        GameObject[] all = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject go in all)
        {
            if (((1 << go.layer) & objectLayer) != 0)
            {
                Transform objTransform = go.GetComponent<Transform>();
                go.GetComponent<SpriteRenderer>().color = Color.red;
                if(buildMode.isMoving == false)
                {
                    objTransform.localScale = new Vector2(objTransform.localScale.x * 1.2f, objTransform.localScale.y * 1.2f);
                }       
            }
        }
    }

    public void DeHighlightObjects()
    {
        GameObject[] all = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject go in all)
        {
            if (((1 << go.layer) & objectLayer) != 0)
            {
                Transform objTransform = go.GetComponent<Transform>();
                go.GetComponent<SpriteRenderer>().color = Color.white;
                objTransform.localScale = new Vector2(objTransform.localScale.x / 1.2f, objTransform.localScale.y / 1.2f);
            }
        }
    }

    public void PurchaseObject(UpgradeStructureHolder script)
    {
        UpgradeStructure obj = script.structure;
        if (CurrencyHandler.baconAmount >= obj.realPrice && script.unlocked)
        {
            GameObject structure = Instantiate(obj.structurePrefab, spawnPos, Quaternion.identity);
            CurrencyHandler.baconAmount -= obj.realPrice;
            structure.GetComponent<ObjectDragger>().beenBought = true;
            script.UpdateText();
            canActivateMenu = false;
            DisableMenu();
        }
    }

    public void ConfirmQuitOpener()
    {
        QuitMenu.SetActive(true);
        audioScript.PlayButtonClick();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        audioScript.PlayButtonClick();
    }
    public void QuitGame()
    {
        audioScript.PlayButtonClick();
        Application.Quit();
    }

    public void CancelQuit()
    {
        audioScript.PlayButtonClick();
        QuitMenu.SetActive(false);
    }
}
