using UnityEngine;

public class GlobalPlayer : MonoBehaviour
{
    private static GlobalPlayer instance = null;
    public static GlobalPlayer Instance
    {
        get { return instance; }
    }

    AudioSource source;
    public AudioClip clip;
    public AudioClip purchase;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play()
    {
        source.PlayOneShot(clip, UserData.Instance.SoundVolume);
    }

    public void PlayPurchase()
    {
        source.PlayOneShot(purchase, UserData.Instance.SoundVolume);
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}