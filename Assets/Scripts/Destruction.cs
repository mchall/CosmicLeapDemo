using UnityEngine;
using System.Collections;

public class Destruction : MonoBehaviour
{
    public float timer = 2f;
    public GameObject explosion;

    AudioHelper audioHelper;
    GameObject exploder;

    void Start()
    {
        audioHelper = Camera.main.GetComponent<AudioHelper>();
        StartCoroutine(Kaboom());
    }

    IEnumerator Kaboom()
    {
        yield return new WaitForSeconds(timer);

        audioHelper.PlayExplosion();


        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var child = gameObject.transform.GetChild(i);
            var player = child.GetComponent<Player>();
            if (player != null)
            {
                player.Die();
            }
        }

        Destroy(gameObject);
        exploder = Instantiate(explosion);
        exploder.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
    }
}