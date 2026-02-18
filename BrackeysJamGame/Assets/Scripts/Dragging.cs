using UnityEngine;

public class Dragging : MonoBehaviour
{
    [SerializeField] bool isDragging = false;
    private BoxCollider2D boxColl;
    private PigletCollision pigColl;
    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        boxColl = GetComponent<BoxCollider2D>();
        pigColl = GetComponent<PigletCollision>();
    }
    void Update()
    {
        if  (isDragging)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseDown()
    {
        if (!pigColl.isSacrificed)
        {
            isDragging = true;
            boxColl.enabled = false;
            pigColl.enabled = false;

            Color c = sr.color;
            c.a = 0.5f;
            sr.color = c;
        } 
    }
    private void OnMouseUp()
    {
        isDragging = false;
        boxColl.enabled = true;
        pigColl.enabled = true;

        Color c = sr.color;
        c.a = 1f;
        sr.color = c;
    }
}
