using UnityEngine;
using System.Collections;

public class MissileLauncher : MonoBehaviour
{
    public GameObject prefab;
    public GameObject target;
    public float speed = 10f;
    public float errorMargin = 2f;
    public bool oneShot;
    public bool disableRotate;
    public bool ignoreCheck;

    GameObject ufo;
    AudioHelper audioHelper;
    AudioSource audioSource;

    bool fired;

    void Start()
    {
        audioHelper = Camera.main.GetComponent<AudioHelper>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = UserData.Instance.SoundVolume / 4;
        audioSource.clip = audioHelper.fire;

        if (transform.childCount > 0)
        {
            ufo = transform.GetChild(0).gameObject;
            StartCoroutine(FireMissile());
        }
    }

    void FixedUpdate()
    {
        if (!disableRotate && ufo != null && target != null && target != ufo)
        {
            if (target.name == "PlayerSpawn")
            {
                target = target.GetComponent<PlayerSpawn>().cubeBoy;
            }

            var pos = target.transform.position - transform.position;
            var newRot = Quaternion.LookRotation(pos);
            ufo.transform.rotation = Quaternion.Lerp(ufo.transform.rotation, newRot, Time.deltaTime * 1f);
        }
    }

    IEnumerator FireMissile()
    {
        if (enabled)
        {
            yield return new WaitForSeconds(2f);

            if (target != null && ufo != null)
            {
                RaycastHit hit;
                var rayDirection = target.transform.position - transform.position;
                if (target == ufo || ignoreCheck || Physics.Raycast(transform.position, rayDirection, out hit) && hit.transform == target.transform)
                {
                    fired = true;
                    audioSource.Play();

                    var m = Instantiate(prefab);
                    var missile = m.GetComponent<Missile>();
                    if (missile != null && target != ufo)
                        missile.parent = ufo;
                    var body = m.GetComponent<Rigidbody>();
                    m.transform.position = transform.position;

                    Vector3 random = new Vector3(Random.Range(-errorMargin, errorMargin), Random.Range(-errorMargin, errorMargin), 0);
                    var firePos = target.transform.position + random;

                    Vector3 gravityUp = (body.position - firePos).normalized;
                    Vector3 localUp = body.transform.right;

                    body.rotation = Quaternion.FromToRotation(localUp, gravityUp) * body.rotation;
                    body.AddForce((firePos - m.transform.position).normalized * speed, ForceMode.Impulse);
                }
                if (oneShot && fired)
                { }
                else
                    StartCoroutine(FireMissile());
            }
        }
    }
}