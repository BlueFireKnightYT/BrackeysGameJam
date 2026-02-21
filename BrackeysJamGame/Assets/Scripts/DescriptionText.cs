using TMPro;
using UnityEngine;

public class DescriptionText : MonoBehaviour
{
    public Vector2 offset;
    private void Update()
    {
        transform.position = (Vector2)Input.mousePosition - offset;
    }
}
