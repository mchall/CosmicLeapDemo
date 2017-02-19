using UnityEngine;

public class ArrowAnimate : MonoBehaviour
{
    bool dir = false;
    float speed = 30f;
    float min, max;

    void Start()
    {
        min = transform.localPosition.x - 5;
        max = transform.localPosition.x + 5;
    }

    void FixedUpdate()
    {
        MoveX(min, max, speed, ref dir);
    }

    private void MoveX(float min, float max, float speed, ref bool dir)
    {
        var x = transform.localPosition.x;
        if (x >= max)
            dir = true;
        else if (x <= min)
            dir = false;

        var dest = new Vector3(dir ? min : max, 0, 0);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, dest, Time.deltaTime * speed);
    }
}