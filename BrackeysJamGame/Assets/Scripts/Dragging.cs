using System.Linq;
using UnityEngine;

public class Dragging : MonoBehaviour
{
    public bool isDragging = false;
    private BoxCollider2D boxColl;
    private PigletCollision pigColl;
    private PigletMove moveScript;
    SpriteRenderer sr;
    AudioSource sfx;

    public AudioClip[] oinkClips;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sfx = GetComponent<AudioSource>();
        boxColl = GetComponent<BoxCollider2D>();
        pigColl = GetComponent<PigletCollision>();
        moveScript = GetComponent<PigletMove>();
    }
    void Update()
    {
        if  (isDragging)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseDown()
    {
        if (!pigColl.isSacrificed && CanvasElementActions.canDragPigs)
        {
            isDragging = true;
            boxColl.enabled = false;
            pigColl.enabled = false;
            float pitchDifference = Random.Range(0.9f, 1.1f);
            sfx.pitch = pitchDifference;
            sfx.PlayOneShot(oinkClips[Random.Range(0, oinkClips.Length)]);

            Color c = sr.color;
            c.a = 0.5f;
            sr.color = c;

        } 
    }
    private void OnMouseUp()
    {
        isDragging = false;
        boxColl.enabled = true;
        pigColl.enabled = true;
        moveScript.StartCoroutine(moveScript.newNextPos());

        Color c = sr.color;
        c.a = 1f;
        sr.color = c;
    }
}
