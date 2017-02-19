using UnityEngine;

public class BodyAnimator : MonoBehaviour
{
    public bool AnimateLegs = true;

    bool ready;
    Transform arm1, arm2, leg1, leg2;
    bool leg1dir = true, arm2dir = true;
    bool leg2dir = false, arm1dir = false;

    float armspeed = 10f, legspeed = 30f;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            if (child.name == "Arm1Parent")
                arm1 = child.transform.GetChild(0);
            else if (child.name == "Arm2Parent")
                arm2 = child.transform.GetChild(0);
            else if (child.name == "Leg1Parent")
                leg1 = child.transform.GetChild(0);
            else if (child.name == "Leg2Parent")
                leg2 = child.transform.GetChild(0);
        }
        ready = true;
    }

    void Update()
    {
        if (ready)
        {
            MoveY(arm1, -2, 2, armspeed, ref arm1dir);
            MoveY(arm2, -2, 2, armspeed, ref arm2dir);

            if (AnimateLegs)
            {
                MoveX(leg1, -3, 3, legspeed, ref leg1dir);
                MoveX(leg2, -3, 3, legspeed, ref leg2dir);
            }
        }
    }

    private void MoveX(Transform obj, float min, float max, float speed, ref bool dir)
    {
        if (obj != null && obj.transform != null)
        {
            var x = obj.localPosition.x;
            if (x >= max)
                dir = true;
            else if (x <= min)
                dir = false;

            var dest = new Vector3(dir ? min : max, obj.localPosition.y, obj.localPosition.z);
            obj.localPosition = Vector3.MoveTowards(obj.localPosition, dest, Time.deltaTime * speed);
        }
    }

    private void MoveY(Transform obj, float min, float max, float speed, ref bool dir)
    {
        if (obj != null && obj.transform != null)
        {
            var x = obj.localPosition.y;
            if (x >= max)
                dir = true;
            else if (x <= min)
                dir = false;

            var dest = new Vector3(obj.localPosition.x, dir ? min : max, obj.localPosition.z);
            obj.localPosition = Vector3.MoveTowards(obj.localPosition, dest, Time.deltaTime * speed);
        }
    }
}