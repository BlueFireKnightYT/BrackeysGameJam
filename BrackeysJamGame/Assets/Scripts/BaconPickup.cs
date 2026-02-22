using UnityEngine;

public class BaconPickup : MonoBehaviour
{
    public float moveSpeed;
    public float bufferAmount;
    AudioSource sfx;
    SpriteRenderer sr;
    CircleCollider2D coll;
    public AudioClip clip;

    private void Start()
    {
        sfx = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<CircleCollider2D>();
    }
    private void OnMouseOver()
    {
        transform.position = Vector3.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) <= bufferAmount)
        {
            float pitchDifference = Random.Range(0.8f, 1.2f);
            sfx.pitch = pitchDifference;
            CurrencyHandler.baconAmount++;
            sfx.PlayOneShot(clip);
            sr.enabled = false;
            coll.enabled = false;
            this.enabled = false;
            Destroy(this.gameObject, 2f);
        }
    }
}
