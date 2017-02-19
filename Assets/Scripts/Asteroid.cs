using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.AddForce(transform.right * 7.5f, ForceMode.Impulse);

        StartCoroutine(Expire());
    }

    IEnumerator Expire()
    {
        if (enabled)
        {
            yield return new WaitForSeconds(10f);
            Destroy(gameObject);
        }
    }
}