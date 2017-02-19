using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class ShipSelect : MonoBehaviour
{
    public ShipSelectSpawn spawn;
    public GameObject nameParent;

    int[] shipOrder;
    float keyboardTime;
    int currentIndex;

    void Start()
    {
        AspectTweak();

        shipOrder = new int[Common.NumShips] { 1, 2, 4,8, 5, 9, 3, 6, 7 };
        var tempIndex = UserData.Instance.GetShipIndex();
        for (int i = 0; i < shipOrder.Length; i++)
        {
            if (tempIndex == shipOrder[i])
            {
                currentIndex = i;
                break;
            }
        }

        SwitchShip(currentIndex);
        UpdateButtons();
    }

    void AspectTweak()
    {
        if (Camera.main.aspect >= 1.6)
            Camera.main.orthographicSize = 9;
        else
            Camera.main.orthographicSize = 11;
    }

    void Update()
    {
        if (Input.GetButtonDown("B Button") || Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Back Button"))
        {
            Cancel();
        }

        if (Input.GetButtonDown("A Button") || Input.GetButtonDown("Jump"))
        {
            if (currentIndex == 0 || UserData.Instance.UnlockedShips.Contains(shipOrder[currentIndex]))
                SelectShip();
            else
                Locked();
        }

        if (Input.GetButtonDown("LB Button"))
        {
            PreviousShip();
        }

        if (Input.GetButtonDown("RB Button"))
        {
            NextShip();
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            if ((Time.time - keyboardTime >= 0.2f))
            {
                PreviousShip();
                keyboardTime = Time.time;
            }
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            if ((Time.time - keyboardTime >= 0.2f))
            {
                NextShip();
                keyboardTime = Time.time;
            }
        }
    }

    public void SelectShip()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        UserData.Instance.SetShipIndex(shipOrder[currentIndex]);
        Application.LoadLevel("Menu");
    }

    public void NextShip()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        currentIndex++;
        if (currentIndex > Common.NumShips - 1)
            currentIndex = 0;
        SwitchShip(currentIndex);
    }

    public void PreviousShip()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        currentIndex--;
        if (currentIndex < 0)
            currentIndex = Common.NumShips - 1;
        SwitchShip(currentIndex);
    }

    public void SwitchShip(int shipIndex)
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        currentIndex = shipIndex;

        spawn.GenerateShip(shipOrder[shipIndex]);

        for (int i = 0; i < nameParent.transform.childCount; i++)
        {
            nameParent.transform.GetChild(i).gameObject.SetActive(false);
        }
        nameParent.transform.FindChild(shipOrder[shipIndex].ToString()).gameObject.SetActive(true);

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
            transform.FindChild("CancelBtn").FindChild("BBtn").gameObject.SetActive(true);
            transform.FindChild("SelectBtn").FindChild("ImageC").gameObject.SetActive(true);
            transform.FindChild("LockedBtn").FindChild("ImageC").gameObject.SetActive(true);
            transform.FindChild("CancelBtn").FindChild("Image").gameObject.SetActive(false);
        }
        else
        {
            transform.FindChild("CancelBtn").FindChild("BBtn").gameObject.SetActive(false);
            transform.FindChild("SelectBtn").FindChild("ImageC").gameObject.SetActive(false);
            transform.FindChild("LockedBtn").FindChild("ImageC").gameObject.SetActive(false);
            transform.FindChild("CancelBtn").FindChild("Image").gameObject.SetActive(true);
        }

        if (currentIndex == 0 || UserData.Instance.UnlockedShips.Contains(shipOrder[currentIndex]))
        {
            transform.FindChild("SelectBtn").gameObject.SetActive(true);
            transform.FindChild("LockedBtn").gameObject.SetActive(false);

            Camera.main.GetComponent<Grayscale>().enabled = false;
        }
        else
        {
            transform.FindChild("SelectBtn").gameObject.SetActive(false);
            transform.FindChild("LockedBtn").gameObject.SetActive(true);

            Camera.main.GetComponent<Grayscale>().enabled = true;
        }
    }

    public void Cancel()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        Application.LoadLevel("Menu");
    }
}