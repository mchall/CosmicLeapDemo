using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Tutorial : MonoBehaviour
{
    Rigidbody body;
    Planet currentPlanet;
    AudioHelper audioHelper;

    LayerMask groundedMask;
    bool switchedPlanets;
    float switchLeewayTime;
    bool doubleJump;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;

    float walkSpeed = 6.5f;
    float jumpForce = 230;
    float dir = 1f;

    bool frozen;
    Vector3 oldVelocity;

    MeshRenderer jumpText;
    MeshRenderer doubleJumpText;
    bool tapped;
    bool finished;

    void Start()
    {
        SetCamera();

        audioHelper = Camera.main.GetComponent<AudioHelper>();
        jumpText = GameObject.Find("JumpText").GetComponent<MeshRenderer>();
        doubleJumpText = GameObject.Find("DoubleJumpText").GetComponent<MeshRenderer>();
        groundedMask = LayerMask.GetMask("Planet");

        body = GetComponent<Rigidbody>();
        body.useGravity = false;
        body.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "JumpSuggestionTrigger")
        {
            StartCoroutine(JumpText());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "JumpSuggestionTrigger")
        {
            jumpText.enabled = true;
        }
        if (other.gameObject.name == "DoubleJumpTrigger")
        {
            jumpText.enabled = false;
            doubleJumpText.enabled = true;
            StartCoroutine(DoubleJumpText());
        }

        if (finished)
            return;

        if (other.gameObject.name == "JumpTrigger")
        {
            SetCamera();

            StartCoroutine(JumpTutorial());
        }
        if (other.gameObject.name == "JumpTrigger2")
        {
            SetCamera();
            StartCoroutine(JumpTutorial());
        }
        if (other.gameObject.name == "EndTrigger")
        {
            SetCamera();

            finished = true;
            GetComponent<Player>().enabled = true;
            GetComponent<Tutorial>().enabled = false;
        }
    }

    private void SetCamera()
    {
        Camera.main.transform.position = new Vector3(transform.position.x + 8, transform.position.y + 2, -10);
    }

    IEnumerator JumpText()
    {
        yield return new WaitForSeconds(1f);
        jumpText.enabled = false;
    }

    IEnumerator DoubleJumpText()
    {
        yield return new WaitForSeconds(0.5f);
        doubleJumpText.enabled = false;
    }

    IEnumerator JumpTutorial()
    {
        tapped = false;
        yield return StartCoroutine(WaitForTap());
        audioHelper.Jump();
        body.AddForce(transform.up * jumpForce * 1.2f, ForceMode.Force);

        yield return new WaitForSeconds(0.5f);

        jumpText.enabled = false;
        doubleJumpText.enabled = true;
        tapped = false;
        yield return StartCoroutine(WaitForTap());
        doubleJumpText.enabled = false;

        body.velocity = Vector3.zero;
        audioHelper.BigJump();
        body.AddForce(transform.up * jumpForce * 1.2f, ForceMode.Force);
        doubleJump = true;

        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator WaitForTap()
    {
        Freeze();
        while (!tapped)
        {
            yield return new WaitForSeconds(0.1f);
        }
        Unfreeze();
    }

    private void Freeze()
    {
        oldVelocity = body.velocity;
        frozen = true;
        body.velocity = Vector3.zero;
        EnableAnimation(false);
    }

    private void Unfreeze()
    {
        body.velocity = oldVelocity;
        frozen = false;
        EnableAnimation(true);
    }

    private void EnableAnimation(bool enabled)
    {
        var model = transform.GetChild(0);

        var anim = model.GetComponent<BodyAnimator>();
        if (anim != null)
            anim.enabled = enabled;

        var anim2 = model.GetComponent<AnimalAnimator>();
        if (anim2 != null)
            anim2.enabled = enabled;
    }

    void Update()
    {
        if (InputHelper.Instance.CheckInput() == InputType.Tap)
        {
            tapped = true;
        }
    }

    void FixedUpdate()
    {
        if (frozen)
            return;

        Planet closestPlanet = Common.Instance.GetClosestPlanet(transform);
        if (closestPlanet != null)
        {
            if (currentPlanet == null)
                currentPlanet = closestPlanet;

            if ((Time.time - switchLeewayTime >= 0.5f) && doubleJump && currentPlanet != closestPlanet)
            {
                switchLeewayTime = Time.time;
                switchedPlanets = true;
                body.velocity = (closestPlanet.transform.position - body.position).normalized;
                currentPlanet = closestPlanet;
            }
            currentPlanet.Attract(body, switchedPlanets ? 3f : 1f);
        }

        if (currentPlanet == null)
            return;

        Vector3 moveDir = new Vector3(dir, dir == 0 ? 0f : -0.1f, 0).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

        Ray ray = new Ray(transform.localPosition, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.5f, groundedMask))
        {
            switchedPlanets = false;
            doubleJump = false;
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
    }
}