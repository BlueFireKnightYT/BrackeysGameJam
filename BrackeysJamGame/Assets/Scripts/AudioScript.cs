using System.Collections;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource source;
    public AudioSource sourceVariablePitch;

    public AudioClip Song;
    public AudioClip BigPigCrash;
    public AudioClip ButtonClick;
    public AudioClip MenuSwoosh;
    public AudioClip Thud;
    public AudioClip TinyPigCrash;
    public AudioClip UpgradeSFX;
    public AudioClip[] Oinks;

    private void Start()
    {
        StartCoroutine(PlaySongRepeating());
    }

    public void PlayBigPigCrash()
    {
        source.PlayOneShot(BigPigCrash);
    }
    public void PlayTinyPigCrash()
    {
        float pitchDifference = Random.Range(0.7f, 1.3f);
        sourceVariablePitch.pitch = pitchDifference;
        sourceVariablePitch.PlayOneShot(TinyPigCrash);
        StartCoroutine(waitForEnd(TinyPigCrash));
    }
    public void PlayTinyPigOink()
    {
        float pitchDifference = Random.Range(0.9f, 1.1f);

        sourceVariablePitch.pitch = pitchDifference;
        int randomOink = Random.Range(0, Oinks.Length);
        sourceVariablePitch.PlayOneShot(Oinks[randomOink]);

        StartCoroutine(waitForEnd(Oinks[randomOink]));
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
    public void PlayUpgradeSFX()
    {
        source.PlayOneShot(UpgradeSFX);
    }

    IEnumerator waitForEnd(AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length);
        sourceVariablePitch.pitch = 1;
    }
    IEnumerator PlaySongRepeating()
    {
        while (true)
        { 
            source.PlayOneShot(Song);
            yield return new WaitForSeconds(Song.length);
        }
    }
}
