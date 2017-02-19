using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingObstacle : MonoBehaviour
{
    Rigidbody body;
    Planet home;

    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;

    float walkSpeed = 3;
    public float dir = -1f;

    void Start()
    {
        body = GetComponent<Rigidbody>();

        body.useGravity = false;
        body.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

        transform.GetChild(1).localRotation = (dir == -1f ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0));
    }

    void FixedUpdate()
    {
        if (home == null)
            home = Common.Instance.GetClosestPlanet(transform);

        if (home != null)
            home.Attract(body);

        Vector3 moveDir = new Vector3(dir, -0.1f, 0).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
        body.MovePosition(body.position + localMove);
    }

    public void ChangeHome(Planet newHome)
    {
        home = newHome;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Obstacle" && col.gameObject.GetComponent<MovingObstacle>() != null)
        {
            SwitchDirection();
        }
    }

    public void Die()
    {
        var mat = transform.GetComponent<Renderer>().material;

        var exploder = Resources.Load("Exploder") as GameObject;
        exploder.GetComponent<DeathExploder>().SetMaterial(mat);
        Instantiate(exploder, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    private void SwitchDirection()
    {
        body.velocity = Vector3.zero;
        var model = transform.GetChild(1);

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