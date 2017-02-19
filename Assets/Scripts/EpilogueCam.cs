using UnityEngine;
using System.Collections;

public class EpilogueCam : MonoBehaviour
{
    public float magnitude = 0.02f;

    float rotate;
    bool pause;

    void Start()
    {
        rotate = magnitude;
    }

    void FixedUpdate()
    {
        if (pause)
            return;

        if (Camera.main.transform.localRotation.eulerAngles.y > 15)
        {
            if (!pause)
                StartCoroutine(Pause());
            rotate = -magnitude;
        }

        if (Camera.main.transform.localRotation.eulerAngles.y < 1)
        {
            if (!pause)
                StartCoroutine(Pause());
            rotate = magnitude;
        }

        transform.Rotate(0, rotate, 0);
    }

    IEnumerator Pause()
    {
        pause = true;
        yield return new WaitForSeconds(0.5f);
        pause = false;
    }
}