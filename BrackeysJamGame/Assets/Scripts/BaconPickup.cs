using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaconPickup : MonoBehaviour
{
    public float moveSpeed;
    public float bufferAmount;
    private void OnMouseOver()
    {
        transform.position = Vector3.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) <= bufferAmount)
        {
            CurrencyHandler.baconAmount++;
            Destroy(this.gameObject);
        }
    }
}
