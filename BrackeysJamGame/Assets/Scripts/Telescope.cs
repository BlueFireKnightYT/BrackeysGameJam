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
    public GameObject noText;
    GameObject wishChoicePanel;
    GameObject canvas;
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
        canvas = GameObject.FindGameObjectWithTag("canvas");
        wishChoicePanel = canvas.transform.Find("Wishchoice Panel").gameObject;
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
        float chance = Random.Range(0, 2);
        hasClicked = true;
        isWaiting = true;
        wishPanel.SetActive(false);
        if (chance == 1)
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
            noText.SetActive(true);
            Invoke("WaitPanelDissapear", 2f);
        }
    }

    public void ExitPanel()
    {
        wishPanel.SetActive (false);
    }

    private void WaitPanelDissapear()
    {
        waitPanel.SetActive (false);
        noText.SetActive (false);
        hasClicked = false;
    }

    private void WishChoiceAppear()
    {
        wishChoicePanel.SetActive (true);
        hasClicked = false;
    }

    private void Update()
    {
        if (isWaiting)
        {
            baseCooldown -= Time.deltaTime;
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
