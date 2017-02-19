using UnityEngine;

public class Pellet : MonoBehaviour
{
    public Planet home;
    public bool DestroyedOnStart;

    void Start()
    {
        if (UserData.Instance.HasCoinChallengedLevel(UserData.Instance.LoadedLevel))
        {
            DestroyedOnStart = true;
            Destroy(gameObject);
            return;
        }

        if (home != null)
            home.Position(transform, 0.5f);
    }

    void Update()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, transform.localRotation * Quaternion.Euler(0, 10, 0), Time.deltaTime * 10f);
    }
}