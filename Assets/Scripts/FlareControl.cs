using UnityEngine;
using System.Collections;

public class FlareControl : MonoBehaviour
{
    public LensFlare left;
    public LensFlare right;

    bool state;

    void Start()
    {
        state = Random.value > 0.5;
        StartCoroutine(Flip());
    }

    IEnumerator Flip()
    {
        if (enabled)
        {
            if (state)
            {
                left.brightness = 0f;
                right.brightness = 0.25f;
            }
            else
            {
                left.brightness = 0.25f;
                right.brightness = 0f;
            }

            state = !state;

            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Flip());
        }
    }
}