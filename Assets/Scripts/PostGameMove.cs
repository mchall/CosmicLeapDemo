using UnityEngine;

public class PostGameMove : MonoBehaviour
{
    public Vector3 pos;
    bool finished;

    void Start()
    {
    }

    void Update()
    {
        if (!finished && UserData.Instance.FinishedLevel)
        {
            finished = true;
            transform.localPosition = pos;
        }
    }
}