using UnityEngine;

public class TutorialEnemy : MonoBehaviour
{
    void Start()
    {
        if (UserData.Instance.HasFinishedLevel("1-3"))
        {
            Destroy(gameObject);
        }
    }
}