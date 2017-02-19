using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class LevelSelect : MonoBehaviour
{
    int currentIndex;
    float keyboardTime;
    string focusedControl;

    void Start()
    {
        if (UserData.Instance.CurrentLevel != null)
        {
            var split = UserData.Instance.CurrentLevel.Split('-');
            if (split.Length > 0)
            {
                ChangeLevel(int.Parse(split[0]));
                FocusLevel(int.Parse(split[1]), split.Length > 2);
                UpdateFocusedControl();
            }
        }
        else
        {
            ChangeLevel(1);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("B Button") || Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Back Button"))
        {
            Cancel();
        }

        if (Input.GetButtonDown("A Button") || Input.GetButtonDown("Jump"))
        {
            var panel = transform.FindChild("LevelPanel");
            panel.FindChild(focusedControl).GetComponent<Button>().onClick.Invoke();
        }

        if (Input.GetButtonDown("LB Button"))
        {
            PreviousLevel();
        }

        if (Input.GetButtonDown("RB Button"))
        {
            NextLevel();
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            if ((Time.time - keyboardTime >= 0.2f))
            {
                HandleDown();
                UpdateFocusedControl();
                keyboardTime = Time.time;
            }
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            if ((Time.time - keyboardTime >= 0.2f))
            {
                HandleUp();
                UpdateFocusedControl();
                keyboardTime = Time.time;
            }
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            if ((Time.time - keyboardTime >= 0.2f))
            {
                HandleLeft();
                UpdateFocusedControl();
                keyboardTime = Time.time;
            }
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            if ((Time.time - keyboardTime >= 0.2f))
            {
                HandleRight();
                UpdateFocusedControl();
                keyboardTime = Time.time;
            }
        }
    }

    private void UpdateButtons()
    {
        var names = Input.GetJoystickNames();
        if (names.Length > 0 && !string.IsNullOrEmpty(names[0]))
        {
            transform.FindChild("LeftBtn").FindChild("LBBtn").gameObject.SetActive(true);
            transform.FindChild("RightBtn").FindChild("RBBtn").gameObject.SetActive(true);
            transform.FindChild("CancelBtn").FindChild("BBtn").gameObject.SetActive(true);

            transform.FindChild("LeftBtn").FindChild("Icon").gameObject.SetActive(false);
            transform.FindChild("RightBtn").FindChild("Icon").gameObject.SetActive(false);
            transform.FindChild("CancelBtn").FindChild("Image").gameObject.SetActive(false);
        }
        else
        {
            transform.FindChild("LeftBtn").FindChild("LBBtn").gameObject.SetActive(false);
            transform.FindChild("RightBtn").FindChild("RBBtn").gameObject.SetActive(false);
            transform.FindChild("CancelBtn").FindChild("BBtn").gameObject.SetActive(false);

            transform.FindChild("LeftBtn").FindChild("Icon").gameObject.SetActive(true);
            transform.FindChild("RightBtn").FindChild("Icon").gameObject.SetActive(true);
            transform.FindChild("CancelBtn").FindChild("Image").gameObject.SetActive(true);
        }
    }

    private void FocusLevel(int subLevel, bool isGlitch)
    {
        ClearFocus();

        switch (subLevel)
        {
            case 1:
                focusedControl = isGlitch ? "GlitchLevel1Button" : "Level1Button";
                break;
            case 2:
                focusedControl = isGlitch ? "GlitchLevel2Button" : "Level2Button";
                break;
            case 3:
                focusedControl = isGlitch ? "GlitchLevel3Button" : "Level3Button";
                break;
            case 4:
                focusedControl = isGlitch ? "GlitchLevel4Button" : "Level4Button";
                break;
            case 5:
                focusedControl = isGlitch ? "GlitchLevel5Button" : "Level5Button";
                break;
        }
    }

    private void HandleLeft()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        switch (focusedControl)
        {
            case "Level1Button":
                PreviousLevel();
                ClearFocus();
                focusedControl = "Level5Button";
                break;
            case "Level2Button":
                ClearFocus();
                focusedControl = "Level1Button";
                break;
            case "Level3Button":
                ClearFocus();
                focusedControl = "Level2Button";
                break;
            case "Level4Button":
                ClearFocus();
                focusedControl = "Level3Button";
                break;
            case "Level5Button":
                ClearFocus();
                focusedControl = "Level4Button";
                break;
            case "GlitchLevel1Button":
                PreviousLevel();
                ClearFocus();
                focusedControl = "GlitchLevel5Button";
                break;
            case "GlitchLevel2Button":
                ClearFocus();
                focusedControl = "GlitchLevel1Button";
                break;
            case "GlitchLevel3Button":
                ClearFocus();
                focusedControl = "GlitchLevel2Button";
                break;
            case "GlitchLevel4Button":
                ClearFocus();
                focusedControl = "GlitchLevel3Button";
                break;
            case "GlitchLevel5Button":
                ClearFocus();
                focusedControl = "GlitchLevel4Button";
                break;
        }
    }

    private void HandleRight()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        switch (focusedControl)
        {
            case "Level1Button":
                ClearFocus();
                focusedControl = "Level2Button";
                break;
            case "Level2Button":
                ClearFocus();
                focusedControl = "Level3Button";
                break;
            case "Level3Button":
                ClearFocus();
                focusedControl = "Level4Button";
                break;
            case "Level4Button":
                ClearFocus();
                focusedControl = "Level5Button";
                break;
            case "Level5Button":
                NextLevel();
                ClearFocus();
                focusedControl = "Level1Button";
                break;
            case "GlitchLevel1Button":
                ClearFocus();
                focusedControl = "GlitchLevel2Button";
                break;
            case "GlitchLevel2Button":
                ClearFocus();
                focusedControl = "GlitchLevel3Button";
                break;
            case "GlitchLevel3Button":
                ClearFocus();
                focusedControl = "GlitchLevel4Button";
                break;
            case "GlitchLevel4Button":
                ClearFocus();
                focusedControl = "GlitchLevel5Button";
                break;
            case "GlitchLevel5Button":
                NextLevel();
                ClearFocus();
                focusedControl = "GlitchLevel1Button";
                break;
        }
    }

    private void HandleDown()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        switch (focusedControl)
        {
            case "IntroButton":
                ClearFocus();
                focusedControl = "Level1Button";
                break;
            case "Level1Button":
                ClearFocus();
                focusedControl = "GlitchLevel1Button";
                break;
            case "Level2Button":
                ClearFocus();
                focusedControl = "GlitchLevel2Button";
                break;
            case "Level3Button":
                ClearFocus();
                focusedControl = "GlitchLevel3Button";
                break;
            case "Level4Button":
                ClearFocus();
                focusedControl = "GlitchLevel4Button";
                break;
            case "Level5Button":
                ClearFocus();
                focusedControl = "GlitchLevel5Button";
                break;
            case "GlitchLevel1Button":
            case "GlitchLevel2Button":
            case "GlitchLevel3Button":
            case "GlitchLevel4Button":
            case "GlitchLevel5Button":
                if (currentIndex == 10)
                {
                    ClearFocus();
                    focusedControl = "EpilogueButton";
                }
                break;
        }
    }

    private void HandleUp()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        switch (focusedControl)
        {
            case "Level1Button":
            case "Level2Button":
            case "Level3Button":
            case "Level4Button":
            case "Level5Button":
                ClearFocus();
                focusedControl = "IntroButton";
                break;
            case "GlitchLevel1Button":
                ClearFocus();
                focusedControl = "Level1Button";
                break;
            case "GlitchLevel2Button":
                ClearFocus();
                focusedControl = "Level2Button";
                break;
            case "GlitchLevel3Button":
                ClearFocus();
                focusedControl = "Level3Button";
                break;
            case "GlitchLevel4Button":
                ClearFocus();
                focusedControl = "Level4Button";
                break;
            case "GlitchLevel5Button":
                ClearFocus();
                focusedControl = "Level5Button";
                break;
            case "EpilogueButton":
                ClearFocus();
                focusedControl = "GlitchLevel1Button";
                break;
        }
    }

    private void ClearFocus()
    {
        var panel = transform.FindChild("LevelPanel");
        var control = panel.FindChild(focusedControl);
        control.localScale = new Vector3(1, 1, 1);
        control.FindChild("Controller").gameObject.SetActive(false);
        control.FindChild("Keyboard").gameObject.SetActive(false);
    }

    public void OnMouseHover()
    {
        PointerEventData pe = new PointerEventData(EventSystem.current);
        pe.position = Input.mousePosition;

        List<RaycastResult> hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pe, hits);
        foreach (RaycastResult h in hits)
        {
            if (h.gameObject != null && h.gameObject.GetComponent<Button>() != null)
            {
                if (GlobalPlayer.Instance != null)
                    GlobalPlayer.Instance.Play();
                ClearFocus();
                focusedControl = h.gameObject.name;
                UpdateFocusedControl();
                break;
            }
        }
    }

    private void UpdateFocusedControl()
    {
        UpdateButtons();
        var panel = transform.FindChild("LevelPanel");

        var names = Input.GetJoystickNames();
        var control = panel.FindChild(focusedControl);
        control.localScale = new Vector3(1.1f, 1.1f, 1);
        if (names.Length > 0 && !string.IsNullOrEmpty(names[0]))
        {
            control.FindChild("Controller").gameObject.SetActive(true);
        }
        else
        {
            control.FindChild("Keyboard").gameObject.SetActive(true);
        }

        EventSystem.current.SetSelectedGameObject(control.gameObject);
    }

    private string Special(float val)
    {
        return string.Format("{0}", val).ToString().Replace("0", "O");
    }

    public void NextLevel()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        var level = currentIndex + 1;
        if (level > Common.NumLevels)
            level = 1;

        ClearFocus();
        ChangeLevel(level);
    }

    public void PreviousLevel()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        var level = currentIndex - 1;
        if (level < 1)
            level = Common.NumLevels;

        ClearFocus();
        ChangeLevel(level);
    }

    public void ChangeLevel(int index)
    {
        currentIndex = index;
        focusedControl = "Level1Button";
        UpdateFocusedControl();

        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        transform.FindChild("LevelPanel").gameObject.SetActive(true);
        transform.FindChild("Name").GetComponent<Text>().text = string.Format("{0} {1} - {2}%", LevelString(), Special(index), Special(Mathf.Round(Common.Instance.LevelPercentage(index))));
        PopulateSubLevel(index);
    }

    private string LevelString()
    {
        if (UserData.Instance.IsSpanish)
            return "Nivel";
        return "Level";
    }

    public void LoadLevel(string level)
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        if (level == "Intro" && UserData.Instance.LevelUnlocked(currentIndex + "-1"))
        {
            Application.LoadLevel("Intro" + currentIndex);
            return;
        }

        if (level == "Epilogue" && UserData.Instance.LevelUnlocked("10-5"))
        {
            Application.LoadLevel("Epilogue1");
            return;
        }

        string name = string.Format("{0}-{1}", currentIndex, level);

        if (UserData.Instance.LevelUnlocked(name))
        {
            Application.LoadLevel(name);
        }
    }

    public void PopulateSubLevel(int level)
    {
        StopAllCoroutines();

        var panel = transform.FindChild("LevelPanel");

        var introBtn = panel.FindChild("IntroButton");
        var introUnlocked = UserData.Instance.LevelUnlocked(level + "-1");
        introBtn.FindChild("Locked").gameObject.SetActive(!introUnlocked);
        introBtn.FindChild("Disabled").gameObject.SetActive(!introUnlocked);
        introBtn.FindChild("Text").gameObject.SetActive(introUnlocked);

        var epilogueBtn = panel.FindChild("EpilogueButton");
        var epilogueUnlocked = UserData.Instance.HasFinishedLevel("10-5");
        epilogueBtn.FindChild("Locked").gameObject.SetActive(!epilogueUnlocked);
        epilogueBtn.FindChild("Disabled").gameObject.SetActive(!epilogueUnlocked);
        epilogueBtn.FindChild("Text").gameObject.SetActive(epilogueUnlocked);
        epilogueBtn.gameObject.SetActive(level == 10);

        for (int j = 1; j <= 5; j++)
        {
            var btn = panel.FindChild("Level" + j + "Button");
            if (btn != null)
            {
                btn.FindChild("Grade").gameObject.SetActive(false);
                btn.FindChild("Locked").gameObject.SetActive(true);
                btn.FindChild("Disabled").gameObject.SetActive(true);
                btn.FindChild("Text").gameObject.SetActive(false);

                var img = btn.GetComponent<Image>();
                img.color = new Color32(255, 150, 34, 255);

                var levelName = level + "-" + j;

                if (UserData.Instance.LevelUnlocked(levelName))
                {
                    btn.FindChild("Locked").gameObject.SetActive(false);
                    btn.FindChild("Disabled").gameObject.SetActive(false);
                    btn.FindChild("Text").gameObject.SetActive(true);

                    if (!UserData.Instance.HasVisitedLevel(levelName))
                    {
                        StartCoroutine(AnimateButton(img, new Color32(255, 150, 34, 255), new Color32(255, 187, 112, 255)));
                    }
                    else
                    {
                        if (UserData.Instance.HasFinishedLevel(levelName))
                        {
                            var grade = "C";
                            if (UserData.Instance.HasCoinChallengedLevel(levelName) && UserData.Instance.HasTimeChallengedLevel(levelName))
                            {
                                grade = "A";
                            }
                            else if (UserData.Instance.HasCoinChallengedLevel(levelName) || UserData.Instance.HasTimeChallengedLevel(levelName))
                            {
                                grade = "B";
                            }

                            btn.FindChild("Grade").gameObject.SetActive(true);
                            btn.FindChild("Grade").GetComponent<Text>().text = grade;
                        }
                    }
                }
            }

            var glitchBtn = panel.FindChild("GlitchLevel" + j + "Button");
            if (glitchBtn != null)
            {
                glitchBtn.FindChild("Grade").gameObject.SetActive(false);
                glitchBtn.FindChild("Locked").gameObject.SetActive(true);
                glitchBtn.FindChild("Disabled").gameObject.SetActive(true);
                glitchBtn.FindChild("Text").gameObject.SetActive(false);

                var img = glitchBtn.GetComponent<Image>();
                img.color = new Color32(174, 23, 23, 255);

                var levelName = level + "-" + j + "-G";

                if (UserData.Instance.LevelUnlocked(levelName))
                {
                    glitchBtn.FindChild("Locked").gameObject.SetActive(false);
                    glitchBtn.FindChild("Disabled").gameObject.SetActive(false);
                    glitchBtn.FindChild("Text").gameObject.SetActive(true);

                    if (!UserData.Instance.HasVisitedLevel(levelName))
                    {
                        StartCoroutine(AnimateButton(img, new Color32(174, 23, 23, 255), new Color32(174, 74, 74, 255)));
                    }
                    else
                    {
                        if (UserData.Instance.HasFinishedLevel(levelName))
                        {
                            var grade = "C";
                            if (UserData.Instance.HasCoinChallengedLevel(levelName) && UserData.Instance.HasTimeChallengedLevel(levelName))
                            {
                                grade = "A";
                            }
                            else if (UserData.Instance.HasCoinChallengedLevel(levelName) || UserData.Instance.HasTimeChallengedLevel(levelName))
                            {
                                grade = "B";
                            }

                            glitchBtn.FindChild("Grade").gameObject.SetActive(true);
                            glitchBtn.FindChild("Grade").GetComponent<Text>().text = grade;
                        }
                    }
                }
            }
        }
    }

    IEnumerator AnimateButton(Image image, Color32 left, Color32 right)
    {
        image.color = left;
        yield return new WaitForSeconds(0.5f);
        image.color = right;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(AnimateButton(image, left, right));
    }

    public void Cancel()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        Application.LoadLevel("Menu");
    }
}