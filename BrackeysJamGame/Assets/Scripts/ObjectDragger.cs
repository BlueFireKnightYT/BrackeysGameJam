using UnityEngine;

public class ObjectDragger : MonoBehaviour
{
    private BoxCollider2D coll;
    private CircleCollider2D circle;
    private SpriteRenderer sr;
    private CarrotDummy cDummy;
    public bool beenBought;
    public bool canPlace;
    public int buyingPrice;
    Color c;
    AudioScript audioScript;
    GameObject sfxManager;
    AudioSource source;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        circle = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        cDummy = GetComponent<CarrotDummy>();

        sfxManager = GameObject.FindGameObjectWithTag("sfxManager");
        audioScript = sfxManager.GetComponent<AudioScript>();
        source = sfxManager.GetComponent<AudioSource>();

        c = sr.color;
        if (beenBought)
        {
            if(circle != null) circle.enabled = false;

            c.a = 0.5f;
            sr.color = c;
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
            if (beenBought)
                audioScript.PlayBuildThud();
            beenBought = false;
            c.a = 1f;
            sr.color = c;
            if (cDummy != null) cDummy.ActivateCarrotDummy();
        }
          
    }
}
