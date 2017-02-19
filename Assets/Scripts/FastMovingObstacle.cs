using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class FastMovingObstacle : MonoBehaviour
{
    Rigidbody body;
    Planet home;

    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;

    float walkSpeed = 8f;
    public float dir = -1f;

    bool moving = true;

    void Start()
    {
        body = GetComponent<Rigidbody>();

        body.useGravity = false;
        body.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

        transform.GetChild(0).localRotation = (dir == -1f ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0));

        StartCoroutine(CatchBreath());
    }

    void FixedUpdate()
    {
        if (home == null)
            home = Common.Instance.GetClosestPlanet(transform);
        home.Attract(body);

        if (moving)
        {
            Vector3 moveDir = new Vector3(dir, -0.1f, 0).normalized;
            Vector3 targetMoveAmount = moveDir * walkSpeed;
            moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

            Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
            body.MovePosition(body.position + localMove);
        }
    }

    public void ChangeHome(Planet newHome)
    {
        home = newHome;
    }

    IEnumerator CatchBreath()
    {
        moving = true;
        yield return new WaitForSeconds(2f);
        EnableAnimation(false);
        moving = false;
        yield return new WaitForSeconds(2f);
        EnableAnimation(true);
        StartCoroutine(CatchBreath()); 
    }

    private void EnableAnimation(bool enabled)
    {
        var model = transform.GetChild(0).GetChild(0);
        var anim = model.GetComponent<BodyAnimator>();
        anim.enabled = enabled;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            SwitchDirection();
        }
    }

    private void SwitchDirection()
    {
        body.velocity = Vector3.zero;
        var model = transform.GetChild(0);

        if (dir > 0)
        {
            dir = -1f;
            model.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            dir = 1f;
            model.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}