using Unity.VisualScripting;
using UnityEngine;

public class CountHandler : MonoBehaviour
{
    private CircleCollider2D circeColl;
    public bool hasCandles;
    public bool hasFire;
    public bool isOccupied;
    public float sacrificeTimer;
    public float fireTimer;
    public int baconMultiplier;
    public int candlesMultiplier;
    Animator anim;
    private void Start()
    {
        circeColl = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
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

    public void UpgradeSacrificeCircle()
    {
        if (hasFire)
        {
            sacrificeTimer = fireTimer;
            anim.SetTrigger("FireUpgrade");
        }
        if (hasCandles)
        {
            baconMultiplier = candlesMultiplier;
            anim.SetTrigger("CandleUpgrade");
        }
    }
}
