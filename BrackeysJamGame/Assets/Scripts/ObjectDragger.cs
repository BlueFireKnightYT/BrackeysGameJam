using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class ObjectDragger : MonoBehaviour
{
    private BoxCollider2D coll;
    private CircleCollider2D circle;
    public bool beenBought;
    public bool canPlace;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        circle = GetComponent<CircleCollider2D>();
        if (beenBought)
        {
            circle.enabled = false;
        }
    }

    void Update()
    {
        if (beenBought)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    private void OnMouseDown()
    {
        canPlace = true;
        RaycastHit2D[] hit = Physics2D.BoxCastAll(transform.position, coll.size, 0, Vector2.zero);

        foreach (RaycastHit2D hit2 in hit)
        {
            if(hit2.collider.gameObject != this.gameObject)
            {
                if (hit2.collider.gameObject.CompareTag("sacrifice"))
                {
                    canPlace = false;
                }
            } 
        }
        if (canPlace) 
        {
            coll.enabled = false;
            circle.enabled = true;
            beenBought = false;
        }
          
    }
}
