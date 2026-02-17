using System.Collections;
using UnityEngine;

public class PigletMove : MonoBehaviour
{
    public Vector3 nextPos;
    Rigidbody2D piggyRb;
    public Animator piggyAnimator;
    SpriteRenderer spriteRenderer;
    BoxCollider2D pigCollider2D;

    [SerializeField] float moveSpeed = 5f;
    float requirement = 0.05f;

    bool isWaiting;

    void Start()
    {
        StartCoroutine(newNextPos());

        pigCollider2D = GetComponent<BoxCollider2D>();
        piggyRb = GetComponent<Rigidbody2D>();
        piggyAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isWaiting = false;
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
        piggyAnimator.SetBool("moving", isWalking);

        float deltaX = nextPos.x - piggyRb.position.x;
        if (Mathf.Abs(deltaX) > 0.01f)
        {
            spriteRenderer.flipX = deltaX > 0f;
        }
    }

    public IEnumerator newNextPos()
    {
        isWaiting = true;
        float pauseTime = Random.Range(3f, 5f);
        yield return new WaitForSeconds(pauseTime);
        nextPos = new Vector3(Random.Range(-9f, 9f), Random.Range(-5f, 5f), transform.position.z);
        isWaiting = false;

        if (!pigCollider2D.enabled)
        {
            pigCollider2D.enabled = true;
            moveSpeed = 3f;
        }
    }       
}
