using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    float elapsedTime = 0f;

    float ogShakeTime = 0.4f;
    float ogShakeAmount = 1;
    public float shakeTime = 0.4f;
    public float shakeAmount = 1f;

    public IEnumerator shake()
    {
        Vector3 startPos = transform.localPosition;
        while (shakeTime > elapsedTime)
        {
            yield return new WaitForEndOfFrame();
            transform.localPosition = new Vector3(startPos.x + Random.Range(-1, 1) * shakeAmount, startPos.y + Random.Range(-1, 1) * shakeAmount, transform.position.z);
            elapsedTime += Time.deltaTime;
        }
        transform.localPosition = startPos;
        elapsedTime = 0f;
        shakeTime = ogShakeTime;
        shakeAmount = ogShakeAmount;
        transform.position = new Vector3(0, 0, -10);
    }
}
