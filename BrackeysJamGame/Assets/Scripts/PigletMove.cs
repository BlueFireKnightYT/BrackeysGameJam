using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PigletMove : MonoBehaviour
{
    public Vector3 nextPos;
    Rigidbody2D piggyRb;
    public Animator piggyAnimator;
    SpriteRenderer spriteRenderer;
    BoxCollider2D pigCollider2D;
    public bool isBiggaPigga;
    public float carrotSensingRange;
    public LayerMask carrotLayer;
    public bool carrotInRange;
    private GameObject closestCarrot;
    public float minWait = 3f;
    public float maxWait = 5f;

    GameObject cam;
    ScreenShake screenShake;
    PigletCollision collisionScript;

    GameObject sfxManager;
    AudioScript audioScript;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float movingSpeed;
    float requirement = 0.05f;

    bool isWaiting;

    void Start()
    {
        StartCoroutine(newNextPos());
        StartCoroutine(canPickUpWhenDropped());

        collisionScript = GetComponent<PigletCollision>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        screenShake = cam.GetComponent<ScreenShake>();

        sfxManager = GameObject.FindGameObjectWithTag("sfxManager");
        audioScript = sfxManager.GetComponent<AudioScript>();

        pigCollider2D = GetComponent<BoxCollider2D>();
        piggyRb = GetComponent<Rigidbody2D>();
        piggyAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isWaiting = false;

        InvokeRepeating("DetectCarrots", 0, 0.5f);
    }

    void FixedUpdate()
    {
        float dist = Vector2.Distance(piggyRb.position, new Vector2(nextPos.x, nextPos.y));
        if (dist < requirement)
        {
            piggyRb.linearVelocity = Vector2.zero;
            if (!isWaiting)
            {
                StartCoroutine(newNextPos());
            }

            return;
        }

        if(!isWaiting)
        { 
            Vector2 target = Vector2.MoveTowards(piggyRb.position, new Vector2(nextPos.x, nextPos.y), moveSpeed * Time.deltaTime);
            piggyRb.MovePosition(target);
        }
    }

    private void Update()
    {
        bool isWalking = !isWaiting && Vector2.Distance(piggyRb.position, new Vector2(nextPos.x, nextPos.y)) > requirement;
        if (!isBiggaPigga)
        {
            piggyAnimator.SetBool("moving", isWalking);
        }
        else
        {
            if (isWalking) piggyAnimator.speed = 1f;
            else piggyAnimator.speed = 0f;

        }
            float deltaX = nextPos.x - piggyRb.position.x;
        if (Mathf.Abs(deltaX) > 0.01f)
        {
            spriteRenderer.flipX = deltaX > 0f;
        }
        
        
    }

    public IEnumerator newNextPos()
    {
        isWaiting = true;
        float pauseTime = Random.Range(minWait, maxWait);
        yield return new WaitForSeconds(pauseTime);
        if (!carrotInRange)
        {
            nextPos = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f), transform.position.z);
        }
        else
        {
            Vector3 carrotPos = closestCarrot.transform.position;
            nextPos = new Vector3(Random.Range(carrotPos.x - 4, carrotPos.x + 4), Random.Range(carrotPos.y - 2, carrotPos.y + 2), transform.position.z);
        }
            isWaiting = false;
    }  
    public IEnumerator canPickUpWhenDropped()
    {
        yield return new WaitForSeconds(3);
        pigCollider2D.enabled = true;
        moveSpeed = movingSpeed;
        if (isBiggaPigga && SceneManager.GetActiveScene().name == "Game")
        { 
            screenShake.StartCoroutine(screenShake.shake());
            audioScript.PlayBigPigCrash();
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            audioScript.PlayTinyPigCrash();
        }

    }

    void DetectCarrots()
    {
        carrotInRange = false;
        closestCarrot = null;
        float minDistance = Mathf.Infinity;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, carrotSensingRange, carrotLayer);
        foreach (Collider2D hit in hits)
        {
            float distance = Vector2.Distance(transform.position, hit.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                carrotInRange = true;
                closestCarrot = hit.gameObject;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, carrotSensingRange);
    }
}
