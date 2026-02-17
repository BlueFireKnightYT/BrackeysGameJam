using UnityEditor.Tilemaps;
using UnityEngine;

public class ObjectDragger : MonoBehaviour
{
    private BoxCollider2D coll;
    private CircleCollider2D circle;
    public bool beenBought;

    private void Start()
    {
        if (beenBought)
        {
            coll = GetComponent<BoxCollider2D>();
            circle = GetComponent<CircleCollider2D>();
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
        coll.enabled = false;
        circle.enabled = true;
        beenBought = false;
    }
}
