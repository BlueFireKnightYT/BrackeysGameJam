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
                hit.gameObject.GetComponent<ObjectDragger>().canPlace = true;
                if (hit.gameObject.GetComponent<ObjectDragger>().cDummy != null)
                {
                    hit.gameObject.GetComponent<ObjectDragger>().cDummy.DeactivateCarrotDummy();
                }
                actionsScript.DeHighlightObjects();
                CurrencyHandler.baconAmount -= 5;
                isMoving = false;
                if (hit.gameObject.GetComponent<CircleCollider2D>() != null) hit.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                if (hit.gameObject.GetComponent<BoxCollider2D>() != null) hit.gameObject.GetComponent<BoxCollider2D>().enabled = true;
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
                if(hit.gameObject.GetComponent<ObjectDragger>().cDummy != null)
                {
                    hit.gameObject.GetComponent<ObjectDragger>().cDummy.DeactivateCarrotDummy();
                }
                actionsScript.DeHighlightObjects();
                isDestroying = false;
            }
            actionsScript.NormalAlpha();
        }
    }
}
