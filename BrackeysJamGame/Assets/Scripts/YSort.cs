using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YSort : MonoBehaviour
{
    void LateUpdate()
    {
        GetComponent<SpriteRenderer>().sortingOrder =
            Mathf.RoundToInt(-transform.position.y * 100);
    }
}
