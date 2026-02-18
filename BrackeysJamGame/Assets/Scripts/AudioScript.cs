using UnityEngine;

public class AudioScript : MonoBehaviour
{
    AudioSource source;
    [SerializeField] AudioClip BigPigCrash;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayBigPigCrash()
    {
        source.PlayOneShot(BigPigCrash);
    }
}
