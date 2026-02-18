using UnityEngine;

public class CarrotDummy : MonoBehaviour
{
    private SpawnPig spawnScript;

    private void Start()
    {
        spawnScript = GameObject.FindGameObjectWithTag("spawner").GetComponent<SpawnPig>();
    }

    public void ActivateCarrotDummy()
    {
        spawnScript.rarePigChance -= 1;
    }

    public void DeactivateCarrotDummy()
    {
        spawnScript.rarePigChance += 1;
    }
}
