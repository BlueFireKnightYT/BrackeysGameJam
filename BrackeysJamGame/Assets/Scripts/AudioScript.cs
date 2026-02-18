using UnityEngine;

public class AudioScript : MonoBehaviour
{
    AudioSource source;
    public AudioClip BigPigCrash;
    public AudioClip ButtonClick;
    public AudioClip MenuSwoosh;
    public AudioClip Thud;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayBigPigCrash()
    {
        source.PlayOneShot(BigPigCrash);
    }
    public void PlayButtonClick()
    {
        source.PlayOneShot(ButtonClick);
    }
    public void PlayMenuSwoosh()
    {
        source.PlayOneShot(MenuSwoosh);
    }
    public void PlayBuildThud()
    {
        source.PlayOneShot(Thud);
    }
}
