using UnityEngine;

public class Pigrainer : MonoBehaviour
{
    private SpawnPig spawnScript;
    public float cooldownTimer;
    private bool onCooldown;
    private Canvas canvas;
    Animator anim;

    private void Start()
    {
        spawnScript = GameObject.FindGameObjectWithTag("spawner").GetComponent<SpawnPig>();
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
        anim = GetComponent<Animator>();
    }
    private void OnMouseDown()
    {
        if (CanvasElementActions.canActivateMenu)
        {
            canvas.enabled = true; 
        }
    }

    public void StartPigRain()
    {
        if (!onCooldown)
        {
            print("clicked");
            anim.SetBool("isPressed", true);
            onCooldown = true;
            spawnScript.maxPigs += 50;
            spawnScript.cooldown *= 0.1f;
            Invoke("UndoRain", 5);
            canvas.enabled = false;
        }
    }

    public void NoOption()
    {
        canvas.enabled=false;
    }

    private void UndoRain()
    {
        Invoke("ResetCooldown", cooldownTimer);
        spawnScript.maxPigs -= 50;
        spawnScript.cooldown /= 0.1f;
    }

    private void ResetCooldown()
    {
        onCooldown = false;
        anim.SetBool("isPressed", false);
    }
}
