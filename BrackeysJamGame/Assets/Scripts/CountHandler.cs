using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class CountHandler : MonoBehaviour
{
    private CircleCollider2D circeColl;
    public bool hasCandles;
    public bool hasFire;
    public bool isOccupied;
    public float sacrificeTimer;
    public float fireTimer;
    public int baconMultiplier;
    public int candlesMultiplier;
    Animator anim;
    GameObject cam;
    ScreenShake screenShake;

    GameObject sfxManager;
    AudioScript audioScript;


    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        screenShake = cam.GetComponent<ScreenShake>();

        sfxManager = GameObject.FindGameObjectWithTag("sfxManager");
        audioScript = sfxManager.GetComponent<AudioScript>();

        circeColl = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isOccupied)
        {
            if(circeColl != null)
            {
                Invoke("UnOccupy", 3f);
            }
        }
    }

    private void UnOccupy()
    {
        isOccupied = false;
    }

    public void UpgradeSacrificeCircle()
    {
        screenShake.shakeTime = 1.4f;
        screenShake.shakeAmount = .1f;

        screenShake.StartCoroutine(screenShake.shake());

        audioScript.PlayUpgradeSFX();

        if (hasFire)
        {
            sacrificeTimer = fireTimer;
            anim.SetTrigger("FireUpgrade");
        }
        if (hasCandles)
        {
            baconMultiplier = candlesMultiplier;
            anim.SetTrigger("CandleUpgrade");
        }
    }

    
}
