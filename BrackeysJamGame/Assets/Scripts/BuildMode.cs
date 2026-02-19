using UnityEngine;

public class BuildMode : MonoBehaviour
{
    public bool isDestroying;
    public bool isMoving;
    public LayerMask objectLayer;
    public CanvasElementActions actionsScript;
    void Update()
    {
        if (isMoving && Input.GetMouseButtonDown(0))
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(worldPos, objectLayer);

            if (hit != null && CurrencyHandler.baconAmount >= 5)
            {
                hit.gameObject.GetComponent<ObjectDragger>().beenBought = true;
                actionsScript.NormalAlpha();
                isMoving = false;
            }
        }
        if(isDestroying && Input.GetMouseButtonDown(0))
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(worldPos, objectLayer);

            if (hit != null)
            {
                Destroy(hit.gameObject);
                int price = hit.gameObject.GetComponent<ObjectDragger>().buyingPrice;
                CurrencyHandler.baconAmount += price / 2;
                actionsScript.NormalAlpha();
                isDestroying = false;
            }
        }
    }
}
