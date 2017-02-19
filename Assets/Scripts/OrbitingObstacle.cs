using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class OrbitingObstacle : MonoBehaviour
{
    public GameObject explosionSphere;
    public Planet home;

    Rigidbody body;
    AudioHelper audioHelper;
    AudioSource audioSource;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.useGravity = false;
        body.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

        audioHelper = Camera.main.GetComponent<AudioHelper>();
        audioSource = transform.GetChild(0).GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.volume = UserData.Instance.SoundVolume;
            audioSource.clip = audioHelper.swoosh;
            audioSource.Play();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.contacts.Length > 0)
        {
            var sphere = Instantiate(explosionSphere);
            sphere.transform.position = col.contacts[0].point;
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        home.Orbit(transform);
    }
}