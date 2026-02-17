using UnityEngine;

public class Dragging : MonoBehaviour
{
    [SerializeField] bool isDragging = false;
    private BoxCollider2D boxColl;

    private void Start()
    {
        boxColl = GetComponent<BoxCollider2D>();
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
        isDragging = true;
        boxColl.enabled = false;
    }
    private void OnMouseUp()
    {
        isDragging = false;
        boxColl.enabled = true;
    }
}
