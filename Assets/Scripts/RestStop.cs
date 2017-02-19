using UnityEngine;

public class RestStop : MonoBehaviour
{
    Transform child;
    bool dir = true;

    void Start()
    {
        child = transform.GetChild(0);

        var home = Common.Instance.GetClosestPlanet(transform);
        if (home != null)
            home.Position(transform, 0f);
    }

    void Update()
    {
        MoveY(child, 0, 0.5f, 3f, ref dir);
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