using UnityEngine;
using System.Collections;

public class GlitchEnemy : MonoBehaviour
{
    Vector3 pos;

    public float time = 1f;

    void Start()
    {
        pos = transform.position;
        StartCoroutine(Glitch());
    }

    IEnumerator Glitch()
    {
        if (enabled)
        {
            yield return new WaitForSeconds(time);
            transform.position = pos;
            StartCoroutine(ResetTrails(gameObject));
            StartCoroutine(Glitch());
        }
    }

    IEnumerator ResetTrails(GameObject obj)
    {
        var trail = obj.GetComponent<TrailRenderer>();

        if(trail == null && obj.transform.childCount > 0)
            trail = obj.transform.GetChild(0).GetComponent<TrailRenderer>();

        if (trail != null)
        {
            float temp = trail.time;
            trail.time = 0;
            yield return new WaitForEndOfFrame();
            trail.time = temp;
        }
    }
}