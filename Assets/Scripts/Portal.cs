using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    Transform child;
    GameObject unaffected;
    AudioHelper audioHelper;

    public Portal end;
    public Planet home;

    void Start()
    {
        audioHelper = Camera.main.GetComponent<AudioHelper>();
        child = transform.GetChild(0);
    }

    void Update()
    {
        child.Rotate(0, 0, 45);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != unaffected)
        {
            Teleport(other.gameObject);
        }
    }

    public IEnumerator CoolDown(GameObject obj)
    {
        unaffected = obj;
        yield return new WaitForSeconds(0.05f);
        unaffected = null;
    }

    void Teleport(GameObject obj)
    {
        audioHelper.Teleport();

        var movingObstacle = obj.GetComponent<MovingObstacle>();
        if (movingObstacle != null)
        {
            movingObstacle.transform.FindChild("WildFire").gameObject.SetActive(false);
        }

        StartCoroutine(end.CoolDown(obj));
        obj.transform.position = end.transform.position;
        StartCoroutine(ResetTrails(obj));

        if (movingObstacle != null)
        {
            movingObstacle.ChangeHome(end.home);
            movingObstacle.transform.FindChild("WildFire").gameObject.SetActive(true);
        }

        var fastEnemy = obj.GetComponent<FastMovingObstacle>();
        if (fastEnemy != null)
        {
            fastEnemy.ChangeHome(end.home);
        }

        var player = obj.GetComponent<Player>();
        if (player != null)
        {
            player.Teleport(end.home);
        }
    }

    IEnumerator ResetTrails(GameObject obj)
    {
        var trail = obj.GetComponent<TrailRenderer>();
        if (trail != null)
        {
            float temp = trail.time;
            trail.time = 0;
            yield return new WaitForEndOfFrame();
            if (trail != null)
                trail.time = temp;
        }
    }
}