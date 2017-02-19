using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    Rigidbody body;
    Planet currentPlanet;
    AudioHelper audioHelper;

    LayerMask groundedMask;
    bool switchedPlanets;
    float switchLeewayTime;
    bool grounded;
    bool doubleJump;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;

    bool floating;
    float floatingTime;

    public float direction = 1f;
    float walkSpeed = 6.5f;
    float jumpForce = 230;
    float prevDir = -5;
    float time = 0f;

    float forwardY;
    float backY;
    float keyboardTime;
    float stepTime;

    void Start()
    {
        if (UserData.Instance.GetLevelCharacter() == 40)
        {
            walkSpeed = 8.5f;
        }

        UserData.Instance.FinishedLevel = false;
        UserData.Instance.CoinsCollected = 0;
        time = Time.time;

        var model = transform.GetChild(0);
        forwardY = model.rotation.eulerAngles.y;
        backY = model.rotation.eulerAngles.y - 180;

        if (direction < 0)
            model.localRotation = Quaternion.Euler(0, backY, 0);

        Camera.main.transform.position = new Vector3(transform.position.x + 4, transform.position.y + 3, -10);

        audioHelper = Camera.main.GetComponent<AudioHelper>();
        groundedMask = LayerMask.GetMask("Planet");

        body = GetComponent<Rigidbody>();
        body.useGravity = false;
        body.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

        EnableAnimation(true);
    }

    void Update()
    {
        var zRot = transform.eulerAngles.z;
        switch (InputHelper.Instance.CheckInput())
        {
            case InputType.Tap:
                {
                    if (direction == 0)
                    {
                        direction = prevDir == 0f ? 1f : prevDir;
                        EnableAnimation(true);
                    }
                    else if (grounded)
                    {
                        audioHelper.Jump();
                        body.AddForce(transform.up * jumpForce, ForceMode.Force);
                    }
                    else if (!doubleJump)
                    {
                        audioHelper.BigJump();
                        doubleJump = true;
                        body.velocity = Vector3.zero;
                        body.AddForce(transform.up * jumpForce * 1.2f, ForceMode.Force);
                    }
                }
                break;
            case InputType.RightTap:
                {
                    if (direction != 0f)
                        Stop();
                    else if (direction == 0)
                    {
                        direction = prevDir == 0f ? 1f : prevDir;
                        SwitchDirection();
                        EnableAnimation(true);
                    }
                }
                break;
            case InputType.Left:
                if ((Time.time - keyboardTime >= 0.2f))
                {
                    ProcessLeft(zRot);
                    keyboardTime = Time.time;
                }
                break;
            case InputType.UpLeft:
                if ((Time.time - keyboardTime >= 0.2f))
                {
                    ProcessUpLeft(zRot);
                    keyboardTime = Time.time;
                }
                break;

            case InputType.DownLeft:
                if ((Time.time - keyboardTime >= 0.2f))
                {
                    ProcessDownLeft(zRot);
                    keyboardTime = Time.time;
                }
                break;
            case InputType.Right:
                if ((Time.time - keyboardTime >= 0.2f))
                {
                    ProcessRight(zRot);
                    keyboardTime = Time.time;
                }
                break;
            case InputType.UpRight:
                if ((Time.time - keyboardTime >= 0.2f))
                {
                    ProcessUpRight(zRot);
                    keyboardTime = Time.time;
                }
                break;
            case InputType.DownRight:
                if ((Time.time - keyboardTime >= 0.2f))
                {
                    ProcessDownRight(zRot);
                    keyboardTime = Time.time;
                }
                break;
            case InputType.Up:
                if ((Time.time - keyboardTime >= 0.2f))
                {
                    ProcessUp(zRot);
                    keyboardTime = Time.time;
                }
                break;
            case InputType.Down:
                if ((Time.time - keyboardTime >= 0.2f))
                {
                    ProcessDown(zRot);
                    keyboardTime = Time.time;
                }
                break;
        }
    }

    private void ProcessLeft(float zRot)
    {
        if (direction != 0f)
            Stop();
        else if (direction == 0f)
        {
            direction = 1f; //left
            SwitchDirection();
            EnableAnimation(true);
        }
    }

    private void ProcessUpLeft(float zRot)
    {
        if (direction != 0f)
            Stop();
        else if (direction == 0f)
        {
            if (zRot <= 45 || zRot >= 225)
                direction = 1f; //left
            else
                direction = -1f; //right
            SwitchDirection();
            EnableAnimation(true);
        }
    }

    private void ProcessDownLeft(float zRot)
    {
        if (direction != 0f)
            Stop();
        else if (direction == 0f)
        {
            if (zRot <= 135 || zRot >= 315)
                direction = 1f; //left
            else
                direction = -1f; //right
            SwitchDirection();
            EnableAnimation(true);
        }
    }

    private void ProcessRight(float zRot)
    {
        if (direction != 0f)
            Stop();
        else if (direction == 0f)
        {
            direction = -1f; //right
            SwitchDirection();
            EnableAnimation(true);
        }
    }

    private void ProcessUpRight(float zRot)
    {
        if (direction != 0f)
            Stop();
        else if (direction == 0f)
        {
            if (zRot <= 135 || zRot >= 315)
                direction = -1f; //right
            else
                direction = 1f; //left
            SwitchDirection();
            EnableAnimation(true);
        }
    }

    private void ProcessDownRight(float zRot)
    {
        if (direction != 0f)
            Stop();
        else if (direction == 0f)
        {
            if (zRot <= 45 || zRot >= 225)
                direction = -1f; //right
            else
                direction = 1f; //left
            SwitchDirection();
            EnableAnimation(true);
        }
    }

    private void ProcessUp(float zRot)
    {
        if (direction != 0f)
            Stop();
        else if (direction == 0f)
        {
            if (zRot <= 180)
                direction = -1f; //right
            else
                direction = 1f; //left
            SwitchDirection();
            EnableAnimation(true);
        }
    }

    private void ProcessDown(float zRot)
    {
        if (direction != 0f)
            Stop();
        else if (direction == 0f)
        {
            if (zRot > 180)
                direction = -1f; //right
            else
                direction = 1f; //left
            SwitchDirection();
            EnableAnimation(true);
        }
    }

    public void Teleport(Planet home)
    {
        body.velocity = Vector3.zero;
        doubleJump = false;
        switchedPlanets = true;
        currentPlanet = home;
        Camera.main.transform.position = new Vector3(transform.position.x + 4, transform.position.y + 3, -10);
    }

    public Planet CurrentPlanet()
    {
        return currentPlanet;
    }

    public bool IsGrounded()
    {
        return grounded;
    }

    public void SwitchPlanets(Planet planet)
    {
        //switchedPlanets = true;
        currentPlanet = planet;
    }

    void FixedUpdate()
    {
        Planet closestPlanet = Common.Instance.GetClosestPlanet(transform);
        if (closestPlanet != null)
        {
            floating = false;
            if (currentPlanet == null)
                currentPlanet = closestPlanet;

            if ((Time.time - switchLeewayTime >= 0.5f) && doubleJump && currentPlanet != closestPlanet)
            {
                switchLeewayTime = Time.time;
                switchedPlanets = true;
                body.velocity = (closestPlanet.transform.position - body.position).normalized;
                currentPlanet = closestPlanet;
            }

            transform.SetParent(currentPlanet.transform);
            currentPlanet.Attract(body, switchedPlanets ? 3f : 1f);
        }
        else if (floating)
        {
            if (Time.time - floatingTime > 2f)
                Die();
        }
        else
        {
            floatingTime = Time.time;
            floating = true;
        }

        if (currentPlanet == null)
            return;

        Vector3 moveDir = new Vector3(direction, direction == 0 ? 0f : -0.1f, 0).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.5f, groundedMask))
        {
            switchedPlanets = false;
            grounded = true;
            doubleJump = false;

            if (direction != 0 && (Time.time - stepTime >= 0.3f))
            {
                audioHelper.Step();
                stepTime = Time.time;
            }
        }
        else
        {
            grounded = false;
        }

        if (!switchedPlanets)
        {
            Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
            body.MovePosition(body.position + localMove);
        }

        if (body.velocity.magnitude > walkSpeed)
        {
            body.velocity = body.velocity.normalized * walkSpeed;
        }

        MoveCamera();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Obstacle" || col.gameObject.tag == "Player" || col.gameObject.tag == "Fire")
        {
            Die();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Obstacle")
        {
            Die();
        }
    }

    public void Die()
    {
        GameFinished();

        var mat = transform.GetChild(0).FindChild("Leg1Parent").GetChild(0).GetComponent<Renderer>().material;

        var exploder = Resources.Load("Exploder") as GameObject;
        exploder.GetComponent<DeathExploder>().SetMaterial(mat);
        Instantiate(exploder, transform.position, transform.rotation);

        FindObjectOfType<Canvas>().GetComponent<Scene>().LoseMenu();

        audioHelper.Die();
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Laser")
        {
            if (other.gameObject.GetComponent<LaserBeam>().enabled)
            {
                Die();
            }
        }
        else if (other.gameObject.tag == "Pellet")
        {
            var p = other.gameObject.GetComponent<Pellet>();
            if (!p.DestroyedOnStart)
            {
                UserData.Instance.CoinsCollected++;
                if (audioHelper != null)
                    audioHelper.Pellet();
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.tag == "RestStop")
        {
            Stop();
        }
        else if (other.gameObject.tag == "Explosion")
        {
            Die();
        }
        else if (other.gameObject.tag == "Rocket")
        {
            Destroy(gameObject);

            var rocket2 = other.gameObject.GetComponent<Rocket2>();
            if (rocket2 != null)
            {
                FindObjectOfType<Canvas>().GetComponent<Scene>().UnlockMenu(rocket2.UnlockIndex);
                other.gameObject.GetComponent<Rocket>().TakeOff();
            }
            else
            {
                var rocket3 = other.gameObject.GetComponent<Rocket3>();
                if (rocket3 != null)
                {
                    FindObjectOfType<Canvas>().GetComponent<Scene>().UnlockShipMenu(rocket3.UnlockIndex);
                    other.gameObject.GetComponent<Rocket>().TakeOff();
                }
                else
                {
                    if (UserData.Instance.LoadedLevel == "Epilogue2")
                    {
                        other.gameObject.GetComponent<Rocket>().FinalTakeOff();
                    }
                    else
                    {
                        if (FindObjectOfType<Canvas>().GetComponent<Scene>().WinMenu(time))
                        {
                            other.gameObject.GetComponent<Rocket>().TakeOff();

                            GameFinished();
                            Camera.main.transform.position = new Vector3(other.transform.position.x + 4, other.transform.position.y + 3, -10);
                        }
                    }
                }
            }
        }
    }

    private void GameFinished()
    {
        Camera.main.GetComponent<CameraScript>().GameFinished = true;
    }

    public void Stop()
    {
        EnableAnimation(false);

        if (prevDir != 0f)
            prevDir = direction;
        direction = 0f;
        body.velocity = Vector3.zero;

        currentPlanet.Attract(body);
    }

    private void EnableAnimation(bool enabled)
    {
        if (enabled)
            GetComponent<TrailRenderer>().enabled = true;
        else
            StartCoroutine(StopTrailRenderer());

        var model = transform.GetChild(0);

        var euler = model.localRotation.eulerAngles;
        model.localRotation = Quaternion.Euler(euler.x, euler.y, enabled ? - 10f : 0f); 

        var anim = model.GetComponent<BodyAnimator>();
        if (anim != null)
            anim.enabled = enabled;

        var anim2 = model.GetComponent<AnimalAnimator>();
        if (anim2 != null)
            anim2.enabled = enabled;
    }

    IEnumerator StopTrailRenderer()
    {
        yield return new WaitForSeconds(1f);
        if (direction == 0)
            GetComponent<TrailRenderer>().enabled = false;
    }

    private void SwitchDirection()
    {
        body.velocity = Vector3.zero;
        var model = transform.GetChild(0);

        if (direction > 0)
        {
            direction = -1f;
            model.localRotation = Quaternion.Euler(0, backY, 0);
        }
        else
        {
            direction = 1f;
            model.localRotation = Quaternion.Euler(0, forwardY, 0);
        }
    }

    private void MoveCamera()
    {
        if (currentPlanet != null)
        {
            Vector3 newPos = new Vector3(currentPlanet.transform.position.x + 4, currentPlanet.transform.position.y + 3, -10);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, newPos, Time.deltaTime * 0.8f);
        }
    }

    void OnDrawGizmos()
    {
        if (currentPlanet != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(currentPlanet.transform.localPosition, currentPlanet.transform.localScale.x);
        }
    }
}