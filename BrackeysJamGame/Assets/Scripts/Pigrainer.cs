using UnityEngine;

public class Pigrainer : MonoBehaviour
{
    public SpawnPig spawnScript;
    public float cooldownTimer;
    private bool onCooldown;
    private void OnMouseDown()
    {
        if (CanvasElementActions.canActivateMenu)
        {
            if(CurrencyHandler.pigsSacrificed >= 50 && !onCooldown)
            {
                print("clicked");
                onCooldown = true;
                spawnScript.maxPigs = 50;
                spawnScript.cooldown = 0.1f;
                Invoke("UndoRain", 5);
            }
        }
    }

    private void UndoRain()
    {
        Invoke("ResetCooldown", cooldownTimer);
        spawnScript.maxPigs = spawnScript.basePigs;
        spawnScript.cooldown = spawnScript.baseCooldown;
    }

    private void ResetCooldown()
    {
        onCooldown = false;
    }
}
