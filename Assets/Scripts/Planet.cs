using UnityEngine;

public class Planet : MonoBehaviour
{
    float gravity = -9.8f;

    public void Attract(Rigidbody body, float magnitude = 1f)
    {
        Vector3 gravityUp = (body.position - transform.localPosition).normalized;
        Vector3 localUp = body.transform.up;

        body.AddForce(gravityUp * gravity * magnitude);
        body.rotation = Quaternion.FromToRotation(localUp, gravityUp) * body.rotation;
    }

    public void Orbit(Transform body, float magnitude = 60f)
    {
        body.RotateAround(transform.position, new Vector3(0, 0, 1).normalized, magnitude * Time.deltaTime);

        Vector3 gravityUp = (body.position - transform.localPosition).normalized;
        Vector3 localUp = body.transform.up;

        body.rotation = Quaternion.FromToRotation(localUp, gravityUp) * body.rotation;
    }

    public void Position(Transform body, float value)
    {
        Vector3 gravityUp = (body.position - transform.localPosition).normalized;
        Vector3 localUp = body.transform.up;

        body.rotation = Quaternion.FromToRotation(localUp, gravityUp) * body.rotation;

        Ray ray = new Ray(transform.position, gravityUp);
        body.position = ray.GetPoint(transform.localScale.x / 2 + value);
    }
}