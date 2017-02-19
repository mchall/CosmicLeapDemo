using UnityEngine;
using System.Collections;

public class DeathExploder : MonoBehaviour
{
    public void SetMaterial(Material mat)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Renderer>().material = mat;
        }
    }

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var body = transform.GetChild(i).GetComponent<Rigidbody>();
            body.AddExplosionForce(Random.Range(25, 50), transform.localPosition, 10f, Random.Range(-5f, 5f));
        }

        StartCoroutine(Expire());
    }

    IEnumerator Expire()
    {
        if (enabled)
        {
            yield return new WaitForSeconds(0.8f);
            Destroy(gameObject);
        }
    }
}