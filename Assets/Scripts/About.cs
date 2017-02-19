using UnityEngine;

public class About : MonoBehaviour
{
    void Start()
    {
        UpdateButtons();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Back Button"))
        {
            Exit();
        }
        else if (Input.GetButtonDown("B Button"))
        {
            Exit();
        }
    }

    void UpdateButtons()
    {
        var names = Input.GetJoystickNames();
        if (names.Length > 0 && !string.IsNullOrEmpty(names[0]))
        {
            transform.FindChild("CancelBtn").FindChild("BBtn").gameObject.SetActive(true);
            transform.FindChild("CancelBtn").FindChild("Image").gameObject.SetActive(false);
        }
        else
        {
            transform.FindChild("CancelBtn").FindChild("BBtn").gameObject.SetActive(false);
            transform.FindChild("CancelBtn").FindChild("Image").gameObject.SetActive(true);
        }
    }

    public void Exit()
    {
        Application.LoadLevel("Menu");
    }
}