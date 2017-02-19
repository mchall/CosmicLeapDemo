using UnityEngine;

public class CharacterUnlock : MonoBehaviour
{
    public CharSelectSpawn spawn;
    public GameObject nameParent;

    bool okSelected = true;
    float keyboardTime;

    void Start()
    {
        AspectTweak();
        SwitchCharacter();
        UpdateButtons();
    }

    void AspectTweak()
    {
        if (Camera.main.aspect > 1.6)
            Camera.main.orthographicSize = 9;
        else
            Camera.main.orthographicSize = 11;
    }

    void Update()
    {
        if (Input.GetButtonDown("B Button") || Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Back Button"))
        {
            Ok();
        }
        else if (Input.GetButtonDown("A Button"))
        {
            Ok();
        }
        else if (Input.GetButtonDown("X Button"))
        {
            SelectCharacter();
        }
        else if (Input.GetButtonDown("Jump"))
        {
            if (okSelected)
                Ok();
            else
                SelectCharacter();
        }
        else if (Input.GetAxis("Vertical") < 0 || Input.GetAxis("Vertical") > 0)
        {
            if ((Time.time - keyboardTime >= 0.2f))
            {
                okSelected = !okSelected;
                UpdateButtons();
                keyboardTime = Time.time;
            }
        }
    }

    public void Ok()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        if (UserData.Instance.CharUnlockLevel == "Epilogue2" && TotalProgress() >= 75 && UserData.Instance.UnlockCharacter(40))
        {
            UserData.Instance.CharUnlockIndex = 40;
            UserData.Instance.CharUnlockLevel = "Epilogue2";
            Application.LoadLevel("CharacterUnlock");
        }
        else
            Application.LoadLevel(UserData.Instance.CharUnlockLevel);
    }

    private float TotalProgress()
    {
        float total = 0;
        for (int i = 1; i <= Common.NumLevels; i++)
        {
            total += Mathf.Round(Common.Instance.LevelPercentage(i));
        }
        return Mathf.Round(total / Common.NumLevels);
    }

    public void SelectCharacter()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        UserData.Instance.SetCharacterIndex(UserData.Instance.CharUnlockIndex);
        Application.LoadLevel(UserData.Instance.CharUnlockLevel);
    }

    public void SwitchCharacter()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        spawn.GenerateCubeBoy(UserData.Instance.CharUnlockIndex);
        nameParent.transform.FindChild(UserData.Instance.CharUnlockIndex.ToString()).gameObject.SetActive(true);

        UpdateButtons();
    }

    public void Locked()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
    }

    void UpdateButtons()
    {
        var names = Input.GetJoystickNames();
        if (names.Length > 0 && !string.IsNullOrEmpty(names[0]))
        {
            transform.FindChild("UnlockSetCurrentBtn").FindChild("ImageC").gameObject.SetActive(true);
            transform.FindChild("UnlockOkBtn").FindChild("ImageC").gameObject.SetActive(true);

            transform.FindChild("UnlockSetCurrentBtn").FindChild("Enter").gameObject.SetActive(false);
            transform.FindChild("UnlockOkBtn").FindChild("Enter").gameObject.SetActive(false);
        }
        else
        {
            transform.FindChild("UnlockSetCurrentBtn").FindChild("ImageC").gameObject.SetActive(false);
            transform.FindChild("UnlockOkBtn").FindChild("ImageC").gameObject.SetActive(false);

            transform.FindChild("UnlockSetCurrentBtn").FindChild("Enter").gameObject.SetActive(!okSelected);
            transform.FindChild("UnlockOkBtn").FindChild("Enter").gameObject.SetActive(okSelected);
        }
    }
}