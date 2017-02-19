using UnityEngine;

public class TutorialJump : MonoBehaviour
{
    void Start()
    {
        if (UserData.Instance.HasFinishedLevel("1-1"))
        {
            Destroy(gameObject);
        }
    }
}