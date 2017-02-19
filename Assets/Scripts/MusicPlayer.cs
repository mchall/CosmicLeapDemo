using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance = null;
    public static MusicPlayer Instance
    {
        get { return instance; }
    }

    AudioSource source;

    AudioClip menuClip;
    AudioClip game1;
    AudioClip game2;
    AudioClip game3;
    AudioClip game4;
    AudioClip game5;

    string menuName;
    string name1;
    string name2;
    string name3;
    string name4;
    string name5;

    void Start()
    {
        InitializeSource();
    }

    void InitializeSource()
    {
        if (source == null)
            source = GetComponent<AudioSource>();
    }

    public void Silence()
    {
        InitializeSource();
        source.Stop();
    }

    public void PlayMenuMusic()
    {
        InitializeSource();

        if (menuClip == null)
            menuClip = GetAudioClip("Playing_To_Win_full_mix_mp3");

        if (!source.isPlaying || source.clip != menuClip)
        {
            source.volume = UserData.Instance.MusicVolume;

            source.Stop();
            source.clip = menuClip;
            source.time = 0f;
            source.Play();
        }
    }

    public void UpdateVolume()
    {
        source.volume = UserData.Instance.MusicVolume;
    }

    public void PlayGameMusic()
    {
        InitializeSource();

        var clip = GameClip();
        if (clip != null)
        {
            if (!source.isPlaying || source.clip != clip)
            {
                source.volume = UserData.Instance.MusicVolume;

                source.Stop();
                source.clip = clip;
                source.time = ClipStartTime();
                source.Play();
            }
        }
        else
        {
            Silence();
        }
    }

    private AudioClip GameClip()
    {
        var levelName = UserData.Instance.LoadedLevel;

        if ((levelName == "Intro1") || (levelName == "Intro2") || (levelName.StartsWith("1-")) || (levelName.StartsWith("2-")))
        {
            if (game1 == null)
                game1 = GetAudioClip("Nanoids");
            return game1;
        }

        if ((levelName == "Intro3") || (levelName == "Intro4") || (levelName.StartsWith("3-")) || (levelName.StartsWith("4-")))
        {
            if (game2 == null)
                game2 = GetAudioClip("Enigmatic");
            return game2;
        }

        if ((levelName == "Intro5") || (levelName == "Intro6") || (levelName.StartsWith("5-")) || (levelName.StartsWith("6-")))
        {
            if (game3 == null)
                game3 = GetAudioClip("Leap_of_Faith");
            return game3;
        }

        if ((levelName == "Intro7") || (levelName == "Intro8") || (levelName.StartsWith("7-")) || (levelName.StartsWith("8-")))
        {
            if (game4 == null)
                game4 = GetAudioClip("Voyage");
            return game4;
        }

        if ((levelName == "Intro9") || (levelName == "Intro10") || (levelName.StartsWith("9-")) || (levelName.StartsWith("10-")))
        {
            if (game5 == null)
                game5 = GetAudioClip("MachinimaSound.com_-_Neuro_Rhythm");
            return game5;
        }

        if ((levelName == "Epilogue1") || (levelName == "Epilogue2"))
        {
            return null;
        }

        if (game1 == null)
            game1 = GetAudioClip("Nanoids");
        return game1;
    }

    private float ClipStartTime()
    {
        if (source.clip == game1)
            return 2.5f;
        if (source.clip == game2)
            return 19.95f;
        if (source.clip == game3)
            return 52.5f;
        if (source.clip == game4)
            return 0f;
        if (source.clip == game5)
            return 34.6f;
        return 0f;
    }

    void Update()
    {
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

    private AudioClip GetAudioClip(string name)
    {
        string path = "Music/" + name;
        return Resources.Load(path) as AudioClip;
    }
}