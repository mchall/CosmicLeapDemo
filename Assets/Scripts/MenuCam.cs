using UnityEngine;
using System.Collections;

public class MenuCam : MonoBehaviour
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

        if (Camera.main.transform.localRotation.eulerAngles.y > 30)
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
        yield return new WaitForSeconds(1f);
        pause = false;
    }
}