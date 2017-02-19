using UnityEngine;

public class AnimalAnimator : MonoBehaviour
{
    bool ready;
    Transform leg1, leg2, leg3, leg4;
    bool leg1dir = true, leg3dir = true;
    bool leg2dir = false, leg4dir = false;

    float legspeed = 30f;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            if (child.name == "Leg1Parent")
                leg1 = child.transform.GetChild(0);
            else if (child.name == "Leg2Parent")
                leg2 = child.transform.GetChild(0);
            else if (child.name == "Leg3Parent")
                leg3 = child.transform.GetChild(0);
            else if (child.name == "Leg4Parent")
                leg4 = child.transform.GetChild(0);
        }
        ready = true;
    }

    void Update()
    {
        if (ready)
        {
            MoveX(leg1, -3, 3, legspeed, ref leg1dir);
            MoveX(leg2, -3, 3, legspeed, ref leg2dir);
            MoveX(leg3, -3, 3, legspeed, ref leg3dir);
            MoveX(leg4, -3, 3, legspeed, ref leg4dir);
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
}