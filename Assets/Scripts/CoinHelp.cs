using UnityEngine;

public class CoinHelp : MonoBehaviour
{
    void Start()
    {
        if (UserData.Instance.HasCoinChallengedLevel(UserData.Instance.LoadedLevel))
        {
            Destroy(gameObject);
            return;
        }
    }
}