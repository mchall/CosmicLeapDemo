using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    GameObject settingsPanel;
    GameObject graphicsPanel;
    GameObject inputPanel;

    float keyboardTime;
    Transform focusedControl;
    int resolutionIndex;

    public TitlePlayerSpawn spawn1;
    public TitlePlayerSpawn spawn2;
    public TitlePlayerSpawn spawn3;

    void Start()
    {
        if (MusicPlayer.Instance != null)
            MusicPlayer.Instance.PlayMenuMusic();

        SpawnCharacters();

        settingsPanel = transform.FindChild("SettingsPanel").gameObject;
        graphicsPanel = transform.FindChild("GraphicsPanel").gameObject;
        inputPanel = transform.FindChild("InputPanel").gameObject;
        UpdateResolutionIndex();
        DisplaySoundVolume();
        DisplayMusicVolume();
        ToggleFullscreenButton(Screen.fullScreen);

        ToggleQualityButton();
        ToggleGlitchButton();
        ToggleVibrateButton();
        ToggleAccessibilityButton();
        UpdateNavigation();
        StartCoroutine(UpdateAspect());
    }

    IEnumerator UpdateAspect()
    {
        AspectTweak();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(UpdateAspect());
    }

    void AspectTweak()
    {
        if (Camera.main.aspect >= 1.7)
            Camera.main.orthographicSize = 9;
        else if (Camera.main.aspect >= 1.6)
            Camera.main.orthographicSize = 10;
        else if (Camera.main.aspect >= 1.5)
            Camera.main.orthographicSize = 11;
        else if (Camera.main.aspect >= 1.3)
            Camera.main.orthographicSize = 12;
        else
            Camera.main.orthographicSize = 13;
    }

    void SpawnCharacters()
    {
        var index = UserData.Instance.GetCharacterIndex();

        spawn3.Index = index;
        spawn3.gameObject.SetActive(true);

        List<int> items = new List<int>();
        items.AddRange(UserData.Instance.UnlockedCharacters);
        items.Remove(index);

        if (items.Count > 0)
        {
            var idx = Common.Instance.Random(0, items.Count);

            spawn1.Index = items[idx];
            spawn1.gameObject.SetActive(true);

            items.Remove(items[idx]);
        }

        if (items.Count > 0)
        {
            var idx = Common.Instance.Random(0, items.Count);

            spawn2.Index = items[idx];
            spawn2.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        transform.FindChild("ControllerInfo").gameObject.SetActive(!IsGamepadAttached());

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Back Button") || Input.GetButtonDown("B Button"))
        {
            if (graphicsPanel.activeInHierarchy)
            {
                HideGraphicsSettings();
            }
            else if (settingsPanel.activeInHierarchy)
            {
                HideSettings();
            }
        }
        else
        {
            if (Input.GetButtonDown("A Button") || Input.GetButtonDown("Jump"))
            {
                HandleNavigationSelected();
            }

            if (Input.GetAxis("Vertical") < 0)
            {
                if ((Time.time - keyboardTime >= 0.2f))
                {
                    ProcessNavigationMove(true);
                    keyboardTime = Time.time;
                }
            }
            else if (Input.GetAxis("Vertical") > 0)
            {
                if ((Time.time - keyboardTime >= 0.2f))
                {
                    ProcessNavigationMove(false);
                    keyboardTime = Time.time;
                }
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                if ((Time.time - keyboardTime >= 0.2f))
                {
                    ProcessHorizontalNavigationMove(true);
                    keyboardTime = Time.time;
                }
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                if ((Time.time - keyboardTime >= 0.2f))
                {
                    ProcessHorizontalNavigationMove(false);
                    keyboardTime = Time.time;
                }
            }
        }
    }

    private void HandleNavigationSelected()
    {
        if (focusedControl != null)
        {
            focusedControl.GetComponent<Button>().onClick.Invoke();
        }
    }

    private void ProcessHorizontalNavigationMove(bool left)
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        if (graphicsPanel.activeInHierarchy)
        {
            if (focusedControl.name == "ResolutionBtn")
            {
                if (left)
                    PreviousResolution();
                else
                    NextResolution();
            }
        }
        else if (settingsPanel.activeInHierarchy)
        {
            if (focusedControl.name == "SoundBtn")
            {
                if (left)
                    PreviousSoundVolume();
                else
                    NextSoundVolume();
            }
            else if (focusedControl.name == "MusicBtn")
            {
                if (left)
                    PreviousMusicVolume();
                else
                    NextMusicVolume();
            }
        }
    }

    private void ProcessNavigationMove(bool down)
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        if (settingsPanel.activeInHierarchy)
        {
            if (settingsPanel.transform.FindChild("GraphicsBtn").FindChild("Arrow").gameObject.activeInHierarchy)
            {
                FocusControl(settingsPanel.transform, down, "InputBtn", null, "BackBtn", null);
            }
            else if (settingsPanel.transform.FindChild("InputBtn").FindChild("Arrow").gameObject.activeInHierarchy)
            {
                FocusControl(settingsPanel.transform, down, "SoundBtn", null, "GraphicsBtn", null);
            }
            else if (settingsPanel.transform.FindChild("SoundBtn").FindChild("Arrow").gameObject.activeInHierarchy)
            {
                FocusControl(settingsPanel.transform, down, "MusicBtn", null, "InputBtn", null);
            }
            else if (settingsPanel.transform.FindChild("MusicBtn").FindChild("Arrow").gameObject.activeInHierarchy)
            {
                FocusControl(settingsPanel.transform, down, "BackBtn", null, "SoundBtn", null);
            }
            else if (settingsPanel.transform.FindChild("BackBtn").FindChild("Arrow").gameObject.activeInHierarchy)
            {
                FocusControl(settingsPanel.transform, down, "GraphicsBtn", null, "MusicBtn", null);
            }
        }
        else if (graphicsPanel.activeInHierarchy)
        {
            if (graphicsPanel.transform.FindChild("ResolutionBtn").FindChild("Arrow").gameObject.activeInHierarchy)
            {
                FocusControl(graphicsPanel.transform, down, "WindowOffBtn", "WindowOnBtn", "BackBtn", null);
            }
            else if (graphicsPanel.transform.FindChild("WindowOffBtn").FindChild("Arrow").gameObject.activeInHierarchy || graphicsPanel.transform.FindChild("WindowOnBtn").FindChild("Arrow").gameObject.activeInHierarchy)
            {
                FocusControl(graphicsPanel.transform, down, "GraphicsHighBtn", "GraphicsLowBtn", "ResolutionBtn", null);
            }
            else if (graphicsPanel.transform.FindChild("GraphicsHighBtn").FindChild("Arrow").gameObject.activeInHierarchy || graphicsPanel.transform.FindChild("GraphicsLowBtn").FindChild("Arrow").gameObject.activeInHierarchy)
            {
                FocusControl(graphicsPanel.transform, down, "GlitchOnBtn", "GlitchOffBtn", "WindowOffBtn", "WindowOnBtn");
            }
            else if (graphicsPanel.transform.FindChild("GlitchOnBtn").FindChild("Arrow").gameObject.activeInHierarchy || graphicsPanel.transform.FindChild("GlitchOffBtn").FindChild("Arrow").gameObject.activeInHierarchy)
            {
                FocusControl(graphicsPanel.transform, down, "BackBtn", null, "GraphicsHighBtn", "GraphicsLowBtn");
            }
            else if (graphicsPanel.transform.FindChild("BackBtn").FindChild("Arrow").gameObject.activeInHierarchy)
            {
                FocusControl(graphicsPanel.transform, down, "ResolutionBtn", null, "GlitchOnBtn", "GlitchOffBtn");
            }
        }
        else if (inputPanel.activeInHierarchy)
        {
            if (inputPanel.transform.FindChild("VibrateOnBtn").FindChild("Arrow").gameObject.activeInHierarchy || inputPanel.transform.FindChild("VibrateOffBtn").FindChild("Arrow").gameObject.activeInHierarchy)
            {
                FocusControl(inputPanel.transform, down, "AccessibilityOnBtn", "AccessibilityOffBtn", "BackBtn", null);
            }
            else if (inputPanel.transform.FindChild("AccessibilityOnBtn").FindChild("Arrow").gameObject.activeInHierarchy || inputPanel.transform.FindChild("AccessibilityOffBtn").FindChild("Arrow").gameObject.activeInHierarchy)
            {
                FocusControl(inputPanel.transform, down, "BackBtn", null, "VibrateOnBtn", "VibrateOffBtn");
            }
            else if (inputPanel.transform.FindChild("BackBtn").FindChild("Arrow").gameObject.activeInHierarchy)
            {
                FocusControl(inputPanel.transform, down, "VibrateOnBtn", "VibrateOffBtn", "AccessibilityOnBtn", "AccessibilityOffBtn");
            }
        }
        else
        {
            if (transform.FindChild("SelectBtn").FindChild("Arrow").gameObject.activeInHierarchy)
            {
                FocusControl(transform, down, "CharacterBtn", null, "ExitBtn", null);
            }
            else if (transform.FindChild("CharacterBtn").FindChild("Arrow").gameObject.activeInHierarchy)
            {
                FocusControl(transform, down, "ShipBtn", null, "SelectBtn", null);
            }
            else if (transform.FindChild("ShipBtn").FindChild("Arrow").gameObject.activeInHierarchy)
            {
                FocusControl(transform, down, "SettingsBtn", null, "CharacterBtn", null);
            }
            else if (transform.FindChild("SettingsBtn").FindChild("Arrow").gameObject.activeInHierarchy)
            {
                FocusControl(transform, down, "AboutBtn", null, "ShipBtn", null);
            }
            else if (transform.FindChild("AboutBtn").FindChild("Arrow").gameObject.activeInHierarchy)
            {
                FocusControl(transform, down, "ExitBtn", null, "SettingsBtn", null);
            }
            else if (transform.FindChild("ExitBtn").FindChild("Arrow").gameObject.activeInHierarchy)
            {
                FocusControl(transform, down, "SelectBtn", null, "AboutBtn", null);
            }
        }
    }

    private void FocusControl(Transform parent, bool down, string down1, string down2, string up1, string up2)
    {
        Transform next = down ? (parent.FindChild(down1).gameObject.activeInHierarchy ? parent.FindChild(down1) : parent.FindChild(down2)) :
                        (parent.FindChild(up1).gameObject.activeInHierarchy ? parent.FindChild(up1) : parent.FindChild(up2));
        FocusControl(next);
    }

    private void UpdateNavigation()
    {
        if (settingsPanel.activeInHierarchy)
        {
            FocusControl(settingsPanel.transform.FindChild("GraphicsBtn"));
        }
        else if (inputPanel.activeInHierarchy)
        {
            if(UserData.Instance.Vibrate)
                FocusControl(inputPanel.transform.FindChild("VibrateOnBtn"));
            else
                FocusControl(inputPanel.transform.FindChild("VibrateOffBtn"));
        }
        else if (graphicsPanel.activeInHierarchy)
        {
            FocusControl(graphicsPanel.transform.FindChild("ResolutionBtn"));
        }
        else
        {
            FocusControl(transform.FindChild("SelectBtn"));
        }
    }

    private void FocusControl(Transform control)
    {
        if (control.name == "Left" || control.name == "Right")
            return;

        if (focusedControl != null)
        {
            focusedControl.FindChild("Arrow").gameObject.SetActive(false);
            focusedControl.FindChild("Enter").gameObject.SetActive(false);
            focusedControl.FindChild("ImageC").gameObject.SetActive(false);
            focusedControl.transform.localScale = new Vector3(1, 1, 1);
        }

        focusedControl = control;
        EventSystem.current.SetSelectedGameObject(focusedControl.gameObject);

        control.FindChild("Arrow").gameObject.SetActive(true);
        control.transform.localScale = new Vector3(1.05f, 1, 1);
        if (IsGamepadAttached())
            control.FindChild("ImageC").gameObject.SetActive(true);
        else
            control.FindChild("Enter").gameObject.SetActive(true);
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
                FocusControl(h.gameObject.transform);
                break;
            }
        }
    }

    public void ShowGraphicsSettings()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        HideSettings();
        graphicsPanel.SetActive(true);

        ShowMainMenu(!graphicsPanel.activeInHierarchy);
        UpdateNavigation();
    }

    public void HideGraphicsSettings()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        graphicsPanel.SetActive(false);

        ShowSettings();
        UpdateNavigation();
    }

    public void ShowInputSettings()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        HideSettings();
        inputPanel.SetActive(true);

        ShowMainMenu(!inputPanel.activeInHierarchy);
        UpdateNavigation();
    }

    public void HideInputSettings()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        inputPanel.SetActive(false);

        ShowSettings();
        UpdateNavigation();
    }

    private bool IsGamepadAttached()
    {
        var names = Input.GetJoystickNames();
        return names.Length > 0 && !string.IsNullOrEmpty(names[0]);
    }

    public void ShowLevelSelect()
    {
        if (!UserData.Instance.LevelUnlocked("1-2"))
        {
            Application.LoadLevel("Intro1");
            return;
        }

        Application.LoadLevel("LevelSelect");
    }

    public void ShipSelect()
    {
        Application.LoadLevel("ShipSelect");
    }

    public void CharacterSelect()
    {
        Application.LoadLevel("CharacterSelect");
    }

    public void ShowAbout()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        Application.LoadLevel("About");
    }

    public void ShowSettings()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        settingsPanel.SetActive(true);

        ShowMainMenu(!settingsPanel.activeInHierarchy);
        UpdateNavigation();
    }

    public void HideSettings()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        settingsPanel.SetActive(false);

        ShowMainMenu(true);
        UpdateNavigation();
    }

    private void ShowMainMenu(bool visible)
    {
        transform.FindChild("SelectBtn").gameObject.SetActive(visible);
        transform.FindChild("CharacterBtn").gameObject.SetActive(visible);
        transform.FindChild("ShipBtn").gameObject.SetActive(visible);
        transform.FindChild("SettingsBtn").gameObject.SetActive(visible);
        transform.FindChild("AboutBtn").gameObject.SetActive(visible);
        transform.FindChild("ExitBtn").gameObject.SetActive(visible);
    }

    public void ToggleFullscreen(bool value)
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        Screen.fullScreen = value;
        ToggleFullscreenButton(value);
    }

    public void ToggleQuality(bool value)
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        UserData.Instance.Quality = value;
        ToggleQualityButton();
    }

    public void ToggleGlitchEffect(bool value)
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        UserData.Instance.GlitchEffect = value;
        ToggleGlitchButton();
    }

    public void ToggleVibrate(bool value)
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        UserData.Instance.Vibrate = value;
        ToggleVibrateButton();
    }

    public void ToggleAccessibility(bool value)
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        UserData.Instance.Accessibility = value;
        ToggleAccessibilityButton();
    }

    private void ToggleQualityButton()
    {
        if (UserData.Instance.Quality)
        {
            graphicsPanel.transform.FindChild("GraphicsHighBtn").gameObject.SetActive(true);
            graphicsPanel.transform.FindChild("GraphicsLowBtn").gameObject.SetActive(false);

            FocusControl(graphicsPanel.transform.FindChild("GraphicsHighBtn"));
        }
        else
        {
            graphicsPanel.transform.FindChild("GraphicsHighBtn").gameObject.SetActive(false);
            graphicsPanel.transform.FindChild("GraphicsLowBtn").gameObject.SetActive(true);

            FocusControl(graphicsPanel.transform.FindChild("GraphicsLowBtn"));
        }

        QualitySettings.SetQualityLevel(UserData.Instance.Quality ? 5 : 1);
        Camera.main.GetComponent<OLDTVTube>().enabled = UserData.Instance.Quality && UserData.Instance.GlitchEffect;
    }

    private void ToggleFullscreenButton(bool fullScreen)
    {
        if (fullScreen)
        {
            graphicsPanel.transform.FindChild("WindowOnBtn").gameObject.SetActive(false);
            graphicsPanel.transform.FindChild("WindowOffBtn").gameObject.SetActive(true);

            FocusControl(graphicsPanel.transform.FindChild("WindowOffBtn"));
        }
        else
        {
            graphicsPanel.transform.FindChild("WindowOnBtn").gameObject.SetActive(true);
            graphicsPanel.transform.FindChild("WindowOffBtn").gameObject.SetActive(false);

            FocusControl(graphicsPanel.transform.FindChild("WindowOnBtn"));
        }
    }

    private void ToggleGlitchButton()
    {
        if (UserData.Instance.GlitchEffect)
        {
            graphicsPanel.transform.FindChild("GlitchOnBtn").gameObject.SetActive(true);
            graphicsPanel.transform.FindChild("GlitchOffBtn").gameObject.SetActive(false);

            FocusControl(graphicsPanel.transform.FindChild("GlitchOnBtn"));
        }
        else
        {
            graphicsPanel.transform.FindChild("GlitchOnBtn").gameObject.SetActive(false);
            graphicsPanel.transform.FindChild("GlitchOffBtn").gameObject.SetActive(true);

            FocusControl(graphicsPanel.transform.FindChild("GlitchOffBtn"));
        }

        Camera.main.GetComponent<GlitchEffect>().enabled = UserData.Instance.GlitchEffect;
        Camera.main.GetComponent<OLDTVTube>().enabled = UserData.Instance.Quality && UserData.Instance.GlitchEffect;
    }

    private void ToggleVibrateButton()
    {
        if (UserData.Instance.Vibrate)
        {
            inputPanel.transform.FindChild("VibrateOnBtn").gameObject.SetActive(true);
            inputPanel.transform.FindChild("VibrateOffBtn").gameObject.SetActive(false);

            FocusControl(inputPanel.transform.FindChild("VibrateOnBtn"));
        }
        else
        {
            inputPanel.transform.FindChild("VibrateOnBtn").gameObject.SetActive(false);
            inputPanel.transform.FindChild("VibrateOffBtn").gameObject.SetActive(true);

            FocusControl(inputPanel.transform.FindChild("VibrateOffBtn"));
        }
    }

    private void ToggleAccessibilityButton()
    {
        if (UserData.Instance.Accessibility)
        {
            inputPanel.transform.FindChild("AccessibilityOnBtn").gameObject.SetActive(true);
            inputPanel.transform.FindChild("AccessibilityOffBtn").gameObject.SetActive(false);

            FocusControl(inputPanel.transform.FindChild("AccessibilityOnBtn"));
        }
        else
        {
            inputPanel.transform.FindChild("AccessibilityOnBtn").gameObject.SetActive(false);
            inputPanel.transform.FindChild("AccessibilityOffBtn").gameObject.SetActive(true);

            FocusControl(inputPanel.transform.FindChild("AccessibilityOffBtn"));
        }
    }

    public void PreviousSoundVolume()
    {
        UserData.Instance.SoundVolume -= 0.1f;
        if (UserData.Instance.SoundVolume < 0)
            UserData.Instance.SoundVolume = 0;

        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        DisplaySoundVolume();
    }

    public void NextSoundVolume()
    {
        UserData.Instance.SoundVolume += 0.1f;
        if (UserData.Instance.SoundVolume > 1)
            UserData.Instance.SoundVolume = 1;

        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        DisplaySoundVolume();
    }

    private void DisplaySoundVolume()
    {
        var volume = (int)Math.Round(UserData.Instance.SoundVolume * 100, 0, MidpointRounding.AwayFromZero);
        settingsPanel.transform.FindChild("SoundBtn").FindChild("Text").GetComponent<Text>().text = string.Format("{0}: {1}%", SoundString(), Special(volume));
    }

    private string SoundString()
    {
        if (UserData.Instance.IsSpanish)
            return "Sonido";
        return "Sound";
    }

    public void PreviousMusicVolume()
    {
        UserData.Instance.MusicVolume -= 0.1f;
        if (UserData.Instance.MusicVolume < 0)
            UserData.Instance.MusicVolume = 0;
        MusicPlayer.Instance.UpdateVolume();

        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        DisplayMusicVolume();
    }

    public void NextMusicVolume()
    {
        UserData.Instance.MusicVolume += 0.1f;
        if (UserData.Instance.MusicVolume > 1)
            UserData.Instance.MusicVolume = 1;
        MusicPlayer.Instance.UpdateVolume();

        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        DisplayMusicVolume();
    }

    private void DisplayMusicVolume()
    {
        var volume = (int)Math.Round(UserData.Instance.MusicVolume * 100, 0, MidpointRounding.AwayFromZero);
        settingsPanel.transform.FindChild("MusicBtn").FindChild("Text").GetComponent<Text>().text = string.Format("{0}: {1}%", MusicString(), Special(volume));
    }

    private string MusicString()
    {
        if (UserData.Instance.IsSpanish)
            return "Música";
        if (UserData.Instance.IsGerman)
            return "Musik";
        return "Music";
    }

    private void UpdateResolutionIndex()
    {
        int index = 0;
        foreach (var resolution in Screen.resolutions)
        {
            if (Screen.width == resolution.width && Screen.height == resolution.height)
            {
                resolutionIndex = index;
                break;
            }
            index++;
        }
        DisplayResolution();
    }

    public void PreviousResolution()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        resolutionIndex--;
        if (resolutionIndex < 0)
            resolutionIndex = Screen.resolutions.Length - 1;
        DisplayResolution();
    }

    public void NextResolution()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        resolutionIndex++;
        if (resolutionIndex >= Screen.resolutions.Length)
            resolutionIndex = 0;
        DisplayResolution();
    }

    private void DisplayResolution()
    {
        var resolution = Screen.resolutions[resolutionIndex];
        graphicsPanel.transform.FindChild("ResolutionBtn").FindChild("Text").GetComponent<Text>().text = string.Format("{0} x {1}", Special(resolution.width), Special(resolution.height));
    }

    public void SetResolution()
    {
        var resolution = Screen.resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private string Special(int val)
    {
        return string.Format("{0}", val).ToString().Replace("0", "O");
    }

    public void Exit()
    {
        Application.Quit();
    }
}