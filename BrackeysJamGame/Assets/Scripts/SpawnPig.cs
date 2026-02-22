using System.Collections;
using UnityEngine;

public class SpawnPig : MonoBehaviour
{
    [SerializeField] GameObject[] pigs;
    [SerializeField] GameObject[] rarePigs;
    [SerializeField] GameObject shadow;



    public float cooldown = 10f;
    public float baseCooldown;
    public int rarePigChance;

    public int maxPigs = 5;
    public int currentPigs = 0;
    public GameObject[] currentPigsList;
    public int basePigs;

    bool isSpawning;


    private void Start()
    {
        baseCooldown = cooldown;
        basePigs = maxPigs;
    }
    private void Update()
    {
        if (!isSpawning && currentPigs < maxPigs)
        {
            StartCoroutine(SpawnPigs());
        }

        currentPigsList = GameObject.FindGameObjectsWithTag("piglet");
        currentPigs = currentPigsList.Length;
    }
    IEnumerator SpawnPigs()
    {
        isSpawning = true;
        while (currentPigs < maxPigs)
        {
            int rarityChance = Random.Range(0, rarePigChance);
            GameObject[] pigsToChoose;
            if (rarityChance == rarePigChance - 1) pigsToChoose = rarePigs;
            else pigsToChoose = pigs;
            int pigToSpawn = Random.Range(0, pigsToChoose.Length);
            yield return new WaitForSeconds(cooldown);

            Vector3 nextPos = new Vector3(Random.Range(-9f, 9f), Random.Range(-5f, 5f), 0f);
            Vector3 spawnPos = new Vector3(nextPos.x, nextPos.y + 15f, 0f);

            GameObject spawnedPig = Instantiate(pigsToChoose[pigToSpawn], spawnPos, Quaternion.identity);
            GameObject shadowObj = Instantiate(shadow, new Vector3(nextPos.x, nextPos.y - 0.85f, 0f), Quaternion.identity);
            if (pigsToChoose == rarePigs && pigToSpawn == 0)
            {
                shadowObj.transform.localScale = new Vector3(2, 2, 2);
            }
            //currentPigs++;
            PigletMove move = spawnedPig.GetComponent<PigletMove>();
            move.nextPos = nextPos;
        }
        isSpawning = false;
    }
}
