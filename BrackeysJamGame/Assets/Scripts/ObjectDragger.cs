using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class ObjectDragger : MonoBehaviour
{
    private BoxCollider2D coll;
    private CircleCollider2D circle;
    private SpriteRenderer sr;
    public CarrotDummy cDummy;
    public bool isUpgrade;
    public bool isCandlesUpgrade;
    public bool isFireUpgrade;
    CanvasElementActions actionsScript;
    public bool beenBought;
    public bool canPlace;
    public int buyingPrice;
    Color c;
    AudioScript audioScript;
    GameObject sfxManager;
    AudioSource source;
    GameObject lastObject;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        circle = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        cDummy = GetComponent<CarrotDummy>();

        actionsScript = GameObject.FindGameObjectWithTag("canvas").GetComponent<CanvasElementActions>();

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
            if(!isUpgrade)
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
            else if (isUpgrade)
            {
                canPlace = false;
                transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D[] hit = Physics2D.BoxCastAll(transform.position, coll.bounds.size, 0, Vector2.zero);

                foreach (RaycastHit2D hit2 in hit)
                {
                    if (hit2.collider.gameObject != this.gameObject)
                    {
                        if (hit2.collider.gameObject.GetComponent<ObjectDragger>() != null)
                        {
                            if(hit2.collider.gameObject.GetComponent<CountHandler>() != null)
                            {
                                if (isFireUpgrade)
                                {
                                    if (!hit2.collider.gameObject.GetComponent<CountHandler>().hasCandles && !hit2.collider.gameObject.GetComponent<CountHandler>().hasFire)
                                    {
                                        canPlace = true;
                                        lastObject = hit2.collider.gameObject;
                                        break;
                                    }
                                }
                                if (isCandlesUpgrade)
                                {
                                    if (!hit2.collider.gameObject.GetComponent<CountHandler>().hasCandles && !hit2.collider.gameObject.GetComponent<CountHandler>().hasFire)
                                    {
                                        canPlace = true;
                                        lastObject = hit2.collider.gameObject;
                                        break;
                                    }
                                }
                            }     
                        }
                    }
                }
            }
        }
    }
    private void OnMouseDown()
    { 
        if (canPlace) 
        {
            if (!isUpgrade)
            {
                coll.enabled = false;
                if (circle != null) circle.enabled = true;
                if (beenBought)
                {
                    audioScript.PlayBuildThud();
                    actionsScript.NormalAlpha();
                    if (cDummy != null) cDummy.ActivateCarrotDummy();
                    CanvasElementActions.canActivateMenu = true;
                }
                beenBought = false;
                c.a = 1f;
                sr.color = c;
            }
            else if (isUpgrade)
            {
                if (isCandlesUpgrade)
                {
                    lastObject.GetComponent<CountHandler>().hasCandles = true;
                    lastObject.GetComponent<CountHandler>().UpgradeSacrificeCircle();
                }
                if (isFireUpgrade)
                {
                    lastObject.GetComponent<CountHandler>().hasFire = true;
                    lastObject.GetComponent<CountHandler>().UpgradeSacrificeCircle();
                }
                CanvasElementActions.canActivateMenu = true;
                Destroy(this.gameObject);
            }
        }  
    }
}
