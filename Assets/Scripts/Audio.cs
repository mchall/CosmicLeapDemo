using UnityEngine;

public class Audio : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().volume = UserData.Instance.SoundVolume / 10;
    }
}