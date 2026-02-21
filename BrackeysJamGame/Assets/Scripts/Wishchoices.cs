using UnityEngine;

public class Wishchoices : MonoBehaviour
{
    SpawnPig spawn;

    private void Start()
    {
        spawn = GameObject.FindGameObjectWithTag("spawner").GetComponent<SpawnPig>();
    }

    public void MorePigs()
    {
        spawn.maxPigs += 5;
        if(spawn.cooldown >= 2)
        {
            spawn.cooldown -= 1;
        }
        this.gameObject.SetActive(false);
    }

    public void MoreBacon()
    {
        CurrencyHandler.baconAmount += 100;
        this.gameObject.SetActive(false);
    }
}
