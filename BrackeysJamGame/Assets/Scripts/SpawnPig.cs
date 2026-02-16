using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPig : MonoBehaviour
{
    [SerializeField] GameObject[] pigs;

    public float cooldown = 10f;

    public int maxPigs = 5;
    int currentPigs = 0;

    bool isSpawning;

    private void Update()
    {
        if (!isSpawning && currentPigs < maxPigs)
        {
            StartCoroutine(SpawnPigs());
        }
    }
    IEnumerator SpawnPigs()
    {
        isSpawning = true;
        while (currentPigs < maxPigs)
        {
            int pigToSpawn = Random.Range(0, pigs.Length);
            yield return new WaitForSeconds(cooldown);

            Vector3 nextPos = new Vector3(Random.Range(-9f, 9f), Random.Range(-5f, 5f), 0f);
            Vector3 spawnPos = new Vector3(nextPos.x, nextPos.y + 15f, 0f);

            GameObject spawnedPig = Instantiate(pigs[pigToSpawn], spawnPos, Quaternion.identity);
            currentPigs++;
            PigletMove move = spawnedPig.GetComponent<PigletMove>();

            move.nextPos = nextPos;
        }
        isSpawning = false;
    }
}
