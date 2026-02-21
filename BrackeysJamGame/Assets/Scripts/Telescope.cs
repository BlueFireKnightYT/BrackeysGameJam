using System.Runtime.ExceptionServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Telescope : MonoBehaviour
{
    public GameObject starPig;
    public GameObject wishPanel;
    public GameObject waitPanel;
    GameObject wishChoicePanel;
    public TextMeshProUGUI waitText;
    public float cooldown;
    float baseCooldown;
    public bool isWaiting;
    bool hasClicked;
    AudioSource sfx;
    public AudioClip clip;

    private void Start()
    {
        baseCooldown = cooldown;
        sfx = GetComponent<AudioSource>();
        wishChoicePanel = GameObject.FindGameObjectWithTag("wishchoice");
    }

   
    private void OnMouseDown()
    {
        if (!isWaiting)
        {
            wishPanel.SetActive(true);
        }
        else if (!hasClicked)
        {
            waitPanel.SetActive(true);
            hasClicked = true;
            Invoke("WaitPanelDissapear", 2f);
        }
    }

    public void LookForStars()
    {
        float chance = Random.Range(0, 5);
        isWaiting = true;
        wishPanel.SetActive(false);
        if (chance == 3)
        {
            sfx.PlayOneShot(clip);
            GameObject obj = Instantiate(starPig, new Vector2(-10, 6), Quaternion.identity);
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(10, -3), ForceMode2D.Impulse);
            obj.GetComponent<Rigidbody2D>().AddTorque(-5, ForceMode2D.Impulse);
            Destroy(obj, 8f);
            Invoke("WishChoiceAppear", 2.5f);
        }
        else
        {
            print("no");
        }
    }

    public void ExitPanel()
    {
        wishPanel.SetActive (false);
    }

    private void WaitPanelDissapear()
    {
        waitPanel.SetActive (false);
        hasClicked = false;
    }

    private void WishChoiceAppear()
    {
        wishChoicePanel.SetActive (true);
    }

    private void Update()
    {
        if (isWaiting)
        {
            baseCooldown -= Time.deltaTime;
            waitPanel.SetActive(true);
            waitText.enabled = true;
        }
        if(baseCooldown <= 0)
        {
            isWaiting = false;
            baseCooldown = cooldown;
            waitPanel.SetActive (false);
        }
        waitText.text = Mathf.RoundToInt(baseCooldown).ToString();
    }
}
