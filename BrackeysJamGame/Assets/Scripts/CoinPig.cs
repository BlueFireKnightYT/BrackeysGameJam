using System.Collections;
using UnityEngine;

public class CoinPig : MonoBehaviour
{
    private bool isWaiting;
    public GameObject baconPrefab;

    private void Update()
    {
        if (!isWaiting)
        {
            if (!GetComponent<Dragging>().isDragging)
            {
                StartCoroutine(DropBacon());
            } 
        }
    }
    public IEnumerator DropBacon()
    {
        isWaiting = true;
        float pauseTime = Random.Range(1, 2);
        yield return new WaitForSeconds(pauseTime);
        GameObject bacon = Instantiate(baconPrefab, transform.position, Quaternion.identity);
        Vector2 shootDir = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        bacon.GetComponent<Rigidbody2D>().AddRelativeForce(shootDir * 3, ForceMode2D.Impulse);
        isWaiting = false;
    }
}
