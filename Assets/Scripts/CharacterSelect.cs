using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CharacterSelect : MonoBehaviour
{
    public CharSelectSpawn spawn;
    public GameObject randomDice;
    public GameObject nameParent;

    int[] charOrder;
    float keyboardTime;
    int currentIndex;

    void Start()
    {
        AspectTweak();

        charOrder = new int[Common.NumChars + 1] { -1, 1, 2, 6, 5, 9, 10, 13, 12, 37, 3, 35, 14, 18, 33, 32, 21, 25, 23, 11, 15, 17, 30, 29, 7, 16, 28, 24, 8, 19, 36, 4, 20, 27, 34, 31, 22, 26, 38, 39, 40 };
        var tempIndex = UserData.Instance.GetExplicitCharacterIndex();
        for (int i = 0; i < charOrder.Length; i++)
        {
            if (tempIndex == charOrder[i])
            {
                currentIndex = i;
                break;
            }
        }

        SwitchCharacter(currentIndex);
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
            if (currentIndex == 0 || UserData.Instance.UnlockedCharacters.Contains(charOrder[currentIndex]))
                SelectCharacter();
            else
                Locked();
        }

        if (Input.GetButtonDown("LB Button"))
        {
            PreviousCharacter();
        }

        if (Input.GetButtonDown("RB Button"))
        {
            NextCharacter();
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            if ((Time.time - keyboardTime >= 0.2f))
            {
                PreviousCharacter();
                keyboardTime = Time.time;
            }
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            if ((Time.time - keyboardTime >= 0.2f))
            {
                NextCharacter();
                keyboardTime = Time.time;
            }
        }
    }

    public void SelectCharacter()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        UserData.Instance.SetCharacterIndex(charOrder[currentIndex]);
        Application.LoadLevel("Menu");
    }

    public void NextCharacter()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        currentIndex++;
        if (currentIndex > Common.NumChars)
            currentIndex = 0;
        SwitchCharacter(currentIndex);
    }

    public void PreviousCharacter()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        currentIndex--;
        if (currentIndex < 0)
            currentIndex = Common.NumChars;
        SwitchCharacter(currentIndex);
    }

    public void SwitchCharacter(int charIndex)
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        currentIndex = charIndex;

        if (charIndex == 0)
        {
            spawn.Destroy();
            randomDice.SetActive(true);
        }
        else
        {
            spawn.GenerateCubeBoy(charOrder[charIndex]);
            randomDice.SetActive(false);
        }

        for (int i = 0; i < nameParent.transform.childCount; i++)
        {
            nameParent.transform.GetChild(i).gameObject.SetActive(false);
        }
        nameParent.transform.FindChild(charOrder[charIndex].ToString()).gameObject.SetActive(true);

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

        if (currentIndex == 0 || UserData.Instance.UnlockedCharacters.Contains(charOrder[currentIndex]))
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