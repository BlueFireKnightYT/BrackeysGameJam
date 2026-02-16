using UnityEngine;

public class PigletCollision : MonoBehaviour
{
    private CircleCollider2D circleColl;
    private PigletMove pMove;
    private Rigidbody2D rb;
    private GameObject sacrificeCircle;
    public bool isSacrificed;
    private SpawnPig spawnScript;
    void Start()
    {
        circleColl = GetComponent<CircleCollider2D>();
        pMove = GetComponent<PigletMove>();
        rb = GetComponent<Rigidbody2D>();
        spawnScript = GameObject.FindGameObjectWithTag("spawner").GetComponent<SpawnPig>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("sacrifice") == true)
        {
            sacrificeCircle = collision.gameObject;
            if(collision.GetComponent<CountHandler>().isOccupied == false)
            {
                pMove.enabled = false;
                transform.position = new Vector2(0f, 0.1f);
                Destroy(gameObject, 1f);
                collision.GetComponent<CountHandler>().isOccupied = true;
                Invoke("UndoOccupy", 0.99f);
                isSacrificed = true;
            }     
        }
    }

    private void UndoOccupy()
    {
        sacrificeCircle.GetComponent<CountHandler>().isOccupied = false;
        CurrencyHandler.baconAmount++;
        spawnScript.currentPigs--;
    }
}
