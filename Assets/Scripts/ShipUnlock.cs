using UnityEngine;

public class ShipUnlock : MonoBehaviour
{
    public ShipSelectSpawn spawn;
    public GameObject nameParent;

    bool okSelected = true;
    float keyboardTime;

    void Start()
    {
        AspectTweak();
        SwitchShip();
        UpdateButtons();
    }

    void AspectTweak()
    {
        if (Camera.main.aspect >= 1.6)
            Camera.main.orthographicSize = 10;
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
            SelectShip();
        }
        else if (Input.GetButtonDown("Jump"))
        {
            if (okSelected)
                Ok();
            else
                SelectShip();
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
        Application.LoadLevel(UserData.Instance.CharUnlockLevel);
    }

    public void SelectShip()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        UserData.Instance.SetShipIndex(UserData.Instance.CharUnlockIndex);
        Application.LoadLevel(UserData.Instance.CharUnlockLevel);
    }

    public void SwitchShip()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        spawn.GenerateShip(UserData.Instance.CharUnlockIndex);
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