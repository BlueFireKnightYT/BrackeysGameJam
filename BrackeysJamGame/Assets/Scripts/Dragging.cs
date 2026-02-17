using UnityEngine;

public class Dragging : MonoBehaviour
{
    [SerializeField] bool isDragging = false;
    private BoxCollider2D boxColl;
    private PigletCollision pigColl;

    private void Start()
    {
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
        } 
    }
    private void OnMouseUp()
    {
        isDragging = false;
        boxColl.enabled = true;
        pigColl.enabled = true;
    }
}
