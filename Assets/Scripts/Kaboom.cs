using UnityEngine;
using System.Collections;

public class Kaboom : MonoBehaviour
{
    public GameObject explosion;

    GameObject exploder;

    void Start()
    {
        StartCoroutine(Expire());

        exploder = Instantiate(explosion);
        exploder.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Planet" && other.gameObject.tag != "Player")
        {
            var movingObstacle = other.gameObject.GetComponent<MovingObstacle>();
            if (movingObstacle != null)
            {
                movingObstacle.Die();
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }

    IEnumerator Expire()
    {
        if (enabled)
        {
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
        }
    }
}