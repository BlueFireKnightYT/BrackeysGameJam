using Unity.VisualScripting;
using UnityEngine;

public class CountHandler : MonoBehaviour
{
    private CircleCollider2D circeColl;
    public bool isOccupied;

    private void Start()
    {
        circeColl = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (isOccupied)
        {
            if(circeColl != null)
            {
                if (!circeColl.IsTouchingLayers())
                {
                    isOccupied = false;
                }
            }
        }
    }
}
