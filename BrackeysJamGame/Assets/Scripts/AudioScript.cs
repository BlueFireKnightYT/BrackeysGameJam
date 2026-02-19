using System.Collections;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource source;
    public AudioSource sourceVariablePitch;
    public AudioClip BigPigCrash;
    public AudioClip ButtonClick;
    public AudioClip MenuSwoosh;
    public AudioClip Thud;
    public AudioClip TinyPigCrash;

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

    IEnumerator waitForEnd(AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length);
        sourceVariablePitch.pitch = 1;
    }
}
