using UnityEngine;

public class Dragging : MonoBehaviour
{
    [SerializeField] bool isDragging = false;
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
    }
    private void OnMouseUp()
    {
        isDragging = false;
    }
}
