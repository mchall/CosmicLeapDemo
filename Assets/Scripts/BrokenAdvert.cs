using UnityEngine;
using System.Collections;

public class BrokenAdvert : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Change());
    }

    IEnumerator Change()
    {
        if (enabled)
        {
            yield return new WaitForSeconds(Random.Range(0f, 0.2f));

            var euler = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(euler.x, euler.y, euler.z + 180);

            StartCoroutine(Change());
        }
    }
}