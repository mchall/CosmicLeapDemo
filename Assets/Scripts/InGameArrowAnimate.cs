using UnityEngine;

public class InGameArrowAnimate : MonoBehaviour
{
    bool dir = false;
    float speed = 0.5f;
    float min, max;

    void Start()
    {
        min = transform.localPosition.y - 0.3f;
        max = transform.localPosition.y;
    }

    void FixedUpdate()
    {
        MoveY(min, max, speed, ref dir);
    }

    private void MoveY(float min, float max, float speed, ref bool dir)
    {
        var y = transform.localPosition.y;
        if (y >= max)
            dir = true;
        else if (y <= min)
            dir = false;

        var dest = new Vector3(transform.localPosition.x, dir ? min : max, transform.localPosition.z);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, dest, Time.deltaTime * speed);
    }
}