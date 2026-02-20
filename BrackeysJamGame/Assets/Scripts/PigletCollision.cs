using UnityEngine;

public class PigletCollision : MonoBehaviour
{
    private PigletMove pMove;
    private BoxCollider2D coll;
    private GameObject sacrificeCircle;
    public int baconObtained;
    public bool isSacrificed;
    private SpawnPig spawnScript;
    void Start()
    {
        pMove = GetComponent<PigletMove>();
        coll = GetComponent<BoxCollider2D>();
        spawnScript = GameObject.FindGameObjectWithTag("spawner").GetComponent<SpawnPig>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("sacrifice") == true)
        {
            sacrificeCircle = collision.gameObject;
            if(collision.GetComponent<CircleCollider2D>().enabled == true)
            {
                if (collision.GetComponent<CountHandler>().isOccupied == false)
                {
                    pMove.piggyAnimator.SetBool("moving", false);
                    if(pMove.isBiggaPigga) pMove.piggyAnimator.speed = 0f;
                    pMove.enabled = false;
                    transform.position = collision.gameObject.transform.position;
                    Destroy(gameObject, collision.gameObject.GetComponent<CountHandler>().sacrificeTimer);
                    collision.GetComponent<CountHandler>().isOccupied = true;
                    Invoke("UndoOccupy", collision.gameObject.GetComponent<CountHandler>().sacrificeTimer - 0.01f);
                    isSacrificed = true;
                }
            }
        }
    }

    private void Update()
    {
        if (!coll.IsTouchingLayers())
        {
            sacrificeCircle = null;
        }
    }

    private void UndoOccupy()
    {
        sacrificeCircle.GetComponent<CountHandler>().isOccupied = false;
        CurrencyHandler.baconAmount += baconObtained * sacrificeCircle.GetComponent<CountHandler>().baconMultiplier;
        spawnScript.currentPigs--;
    }
}
