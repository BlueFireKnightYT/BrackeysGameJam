using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;

public class ObjectDragger : MonoBehaviour
{
    private BoxCollider2D coll;
    private CircleCollider2D circle;
    private SpriteRenderer sr;
    private CarrotDummy cDummy;
    public bool beenBought;
    public bool canPlace;
    Color c;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        circle = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        cDummy = GetComponent<CarrotDummy>();
        c = sr.color;
        if (beenBought && circle != null)
        {
            circle.enabled = false;
            c.a = 0.1f;
        }
    }

    void Update()
    {
        if (beenBought)
        {
            canPlace = true;
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hit = Physics2D.BoxCastAll(transform.position, coll.bounds.size, 0, Vector2.zero);

            foreach (RaycastHit2D hit2 in hit)
            {
                if (hit2.collider.gameObject != this.gameObject)
                {
                    if (hit2.collider.gameObject.GetComponent<ObjectDragger>() != null)
                    {
                        canPlace = false;
                        break;
                    }
                }
            }
        }
    }
    private void OnMouseDown()
    { 
        if (canPlace) 
        {
            coll.enabled = false;
            if(circle != null) circle.enabled = true;
            beenBought = false;
            c.a = 1f;
            if(cDummy != null) cDummy.ActivateCarrotDummy();
        }
          
    }
}
