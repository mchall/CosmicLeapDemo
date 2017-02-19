using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    public GameObject explosionSphere;

    AudioHelper audioHelper;
    AudioSource audioSource;

    public GameObject parent;

    void Start()
    {
        audioHelper = Camera.main.GetComponent<AudioHelper>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = UserData.Instance.SoundVolume;
        audioSource.clip = audioHelper.ExplodeClip();

        var body = GetComponent<Rigidbody>();
        body.useGravity = false;
        body.constraints = RigidbodyConstraints.FreezeRotation;

        StartCoroutine(Expire());
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject != parent && col.contacts.Length > 0)
        {
            audioSource.Play();

            Destroy(transform.GetChild(0).gameObject);
            if (col.gameObject.tag != "Planet")
            {
                var immune = col.gameObject.GetComponent<MissileImmune>();
                var movingObstacle = col.gameObject.GetComponent<MovingObstacle>();
                if (movingObstacle != null)
                {
                    movingObstacle.Die();
                }
                else if (immune == null)
                {
                    Destroy(col.gameObject);
                }
            }

            var sphere = Instantiate(explosionSphere);
            sphere.transform.position = col.contacts[0].point;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Laser")
        {
            audioSource.Play();

            Destroy(transform.GetChild(0).gameObject);
            if (other.gameObject.tag != "Planet")
            {
                Destroy(other.gameObject);
            }

            var sphere = Instantiate(explosionSphere);
            sphere.transform.position = other.transform.position;
        }
    }

    IEnumerator Expire()
    {
        if (enabled)
        {
            yield return new WaitForSeconds(20f);
            Destroy(gameObject);
        }
    }
}