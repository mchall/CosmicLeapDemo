using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using XInputDotNetPure;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Scene : MonoBehaviour
{
    Transform gameMenu;
    bool inMenu;
    int numCompleted;
    Transform focusedControl;

    public float targetTime;
    public int numPlayers = 1;
    float keyboardTime;

    void Start()
    {
        if (MusicPlayer.Instance != null)
            MusicPlayer.Instance.PlayGameMusic();

        UserData.Instance.CurrentLevel = UserData.Instance.LoadedLevel;

        gameMenu = transform.FindChild("GameMenu");
        AspectTweak();
    }

    void AspectTweak()
    {
        if (Camera.main.aspect >= 1.6)
            Camera.main.orthographicSize = 8;
        else 
            Camera.main.orthographicSize = 9;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Back Button"))
        {
            if (!inMenu)
                Quit();
            else
                Home();
        }
        else
        {
            if (inMenu)
            {
                if (gameMenu.gameObject.activeInHierarchy)
                {
                    if (Input.GetButtonDown("A Button"))
                    {
                        if (gameMenu.FindChild("NextBtn").gameObject.activeInHierarchy)
                            NextScene();
                    }
                    if (Input.GetButtonDown("Y Button") || Input.GetButtonDown("Replay"))
                    {
                        SceneReset();
                    }
                    if (Input.GetButtonDown("B Button"))
                    {
                        Home();
                    }
                    if (Input.GetButtonDown("X Button"))
                    {
                        if(gameMenu.FindChild("NightmareBtn").gameObject.activeInHierarchy)
                            NightmareScene();
                    }

                    if (Input.GetButtonDown("Jump"))
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

    private void ProcessNavigationMove(bool down)
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        if (gameMenu.FindChild("HomeBtn").FindChild("Arrow").gameObject.activeInHierarchy)
        {
            bool nightmareAvailable = gameMenu.FindChild("NightmareBtn").gameObject.activeInHierarchy;
            bool nextAvailable = gameMenu.FindChild("NextBtn").gameObject.activeInHierarchy;

            Transform next = down ? gameMenu.FindChild("PrevBtn") : (nightmareAvailable ? gameMenu.FindChild("NightmareBtn") : (nextAvailable ? gameMenu.FindChild("NextBtn") : gameMenu.FindChild("PrevBtn")));
            FocusControl(next);
        }
        else if (gameMenu.FindChild("PrevBtn").FindChild("Arrow").gameObject.activeInHierarchy)
        {
            bool nextAvailable = gameMenu.FindChild("NextBtn").gameObject.activeInHierarchy;

            Transform next = down ? (nextAvailable ? gameMenu.FindChild("NextBtn") : gameMenu.FindChild("HomeBtn")) : gameMenu.FindChild("HomeBtn");
            FocusControl(next);
        }
        else if (gameMenu.FindChild("NextBtn").FindChild("Arrow").gameObject.activeInHierarchy)
        {
            bool nightmareAvailable = gameMenu.FindChild("NightmareBtn").gameObject.activeInHierarchy;

            Transform next = down ? (nightmareAvailable ? gameMenu.FindChild("NightmareBtn") : gameMenu.FindChild("HomeBtn")) : gameMenu.FindChild("PrevBtn");
            FocusControl(next);
        }
        else if (gameMenu.FindChild("NightmareBtn").FindChild("Arrow").gameObject.activeInHierarchy)
        {
            Transform next = down ? gameMenu.FindChild("HomeBtn") : gameMenu.FindChild("NextBtn");
            FocusControl(next);
        }
    }

    private void FocusControl(Transform control)
    {
        if (IsGamepadAttached())
            return;

        if (focusedControl != null)
        {
            focusedControl.FindChild("Arrow").gameObject.SetActive(false);
            focusedControl.FindChild("Enter").gameObject.SetActive(false);
            focusedControl.transform.localScale = new Vector3(1, 1, 1);
        }

        focusedControl = control;
        EventSystem.current.SetSelectedGameObject(focusedControl.gameObject);

        control.FindChild("Arrow").gameObject.SetActive(true);
        control.FindChild("Enter").gameObject.SetActive(true);
        control.transform.localScale = new Vector3(1.05f, 1, 1);
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

    public void SceneReset()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        UserData.Instance.RetryCount++;
        GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
        Application.LoadLevel(UserData.Instance.LoadedLevel);
    }

    public void Home()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();
        UserData.Instance.RetryCount = 0;
        GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
        Application.LoadLevel("Menu");
    }

    public void NextScene()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        UserData.Instance.RetryCount = 0;
        GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
        var name = Common.Instance.GetNextLevelName();
        if (UserData.Instance.LevelUnlocked(name))
        {
            int level = int.Parse(name.Split('-')[0]);
            if (level > Common.NumLevels)
                Application.LoadLevel("Menu");
            else
                Application.LoadLevel(name);
        }
        else if (name.EndsWith("-G"))
        {
            name = name.Replace("-G", "");

            int level = int.Parse(name.Split('-')[0]);
            if (level > Common.NumLevels)
                Application.LoadLevel("Menu");
            else
                Application.LoadLevel(name);
        }
        else
        {
            Application.LoadLevel("Menu");
        }
    }

    public void NightmareScene()
    {
        if (GlobalPlayer.Instance != null)
            GlobalPlayer.Instance.Play();

        GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
        if (UserData.Instance.LoadedLevel.EndsWith("-G"))
        {
            Application.LoadLevel(UserData.Instance.LoadedLevel.Replace("-G", ""));
        }
        else
        {
            if (UserData.Instance.LevelUnlocked(UserData.Instance.LoadedLevel + "-G"))
            {
                Application.LoadLevel(UserData.Instance.LoadedLevel + "-G");
            }
        }
    }

    private bool UnlockNextLevel()
    {
        var name = Common.Instance.GetNextLevelName();
        if (UserData.Instance.UnlockLevel(name))
        {
            return true;
        }
        return false;
    }

    public void Quit()
    {
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            var p = player.GetComponent<Player>();
            if (p != null)
                p.Die();
        }
        Camera.main.GetComponent<CameraScript>().GameFinished = true;
        LoseMenu();
    }

    public bool WinMenu(float time)
    {
        numCompleted++;
        if (numCompleted < numPlayers)
            return false;

        if (inMenu)
            return false;
        inMenu = true;

        StartCoroutine(Vibrate(0.2f, 5f));

        UserData.Instance.FinishedLevel = true;
        UserData.Instance.LevelTime = Time.time - time;

        inMenu = true;
        bool progressSave = false;

        StartCoroutine(DoShowCanvas(3f));

        if (!UserData.Instance.LoadedLevel.EndsWith("-G"))
        {
            progressSave = UnlockNextLevel();
        }

        if (UserData.Instance.LevelFinished(UserData.Instance.LoadedLevel))
            progressSave = true;

        bool timeCompleted = false;
        bool coinsCompleted = false;

        if (UserData.Instance.RecordBestLevelTime(UserData.Instance.LoadedLevel, UserData.Instance.LevelTime))
            progressSave = true;

        if (UserData.Instance.HasTimeChallengedLevel(UserData.Instance.LoadedLevel))
        {
            timeCompleted = true;
        }
        else
        {
            if (UserData.Instance.LevelTime <= targetTime)
            {
                progressSave = true;
                UserData.Instance.TimeChallengeCompleted(UserData.Instance.LoadedLevel);
                timeCompleted = true;
            }
        }

        if (UserData.Instance.HasCoinChallengedLevel(UserData.Instance.LoadedLevel))
        {
            coinsCompleted = true;
        }
        else
        {
            if (UserData.Instance.CoinsCollected >= 5)
            {
                progressSave = true;
                UserData.Instance.CoinChallengeCompleted(UserData.Instance.LoadedLevel);
                coinsCompleted = true;
            }
        }

        if (!UserData.Instance.LoadedLevel.EndsWith("-G") && timeCompleted && coinsCompleted)
        {
            if (!UserData.Instance.LevelUnlocked(UserData.Instance.LoadedLevel + "-G"))
            {
                progressSave = true;
                UserData.Instance.UnlockLevel(UserData.Instance.LoadedLevel + "-G");

                gameMenu.FindChild("Results").FindChild("NightmareUnlocked").gameObject.SetActive(true);                
            }
        }

        UpdateReport();
        UpdateNavigation();

        if (progressSave)
            UserData.Instance.SaveLevelProgress();

        return true;
    }

    public bool UnlockMenu(int unlockIndex)
    {
        if (inMenu)
            return false;
        inMenu = true;

        StartCoroutine(Vibrate(0.2f, 5f));

        inMenu = true;

        UserData.Instance.UnlockCharacter(unlockIndex);
        StartCoroutine(DoShowUnlock(5f, unlockIndex));

        return true;
    }

    public bool UnlockShipMenu(int unlockIndex)
    {
        if (inMenu)
            return false;
        inMenu = true;

        StartCoroutine(Vibrate(0.2f, 5f));

        inMenu = true;

        UserData.Instance.UnlockShip(unlockIndex);
        StartCoroutine(DoShowShipUnlock(5f, unlockIndex));

        return true;
    }

    private void UpdateReport()
    {
        var reportPanel = gameMenu.FindChild("Results");
        var timeDetails = reportPanel.FindChild("TimeDetails");
        var bestTimeDetails = reportPanel.FindChild("BestDetails");
        var targetTimeDetails = reportPanel.FindChild("TargetDetails");
        var coinDetails = reportPanel.FindChild("CoinDetails");
        var score = reportPanel.FindChild("Score");

        reportPanel.FindChild("Heading2").GetComponent<Text>().text = String.Format("{0} {1}", LevelString(), Common.Instance.FriendlyLevelName().Replace("0", "o"));

        var minutes = Mathf.Floor(targetTime / 60);
        var seconds = Mathf.Floor(targetTime % 60);
        var fraction = (targetTime * 100) % 100;

        targetTimeDetails.GetComponent<Text>().text = String.Format("{0} {1}:{2}:{3}", TargetString(), Special(minutes), Special(seconds), Special(fraction));

        if (UserData.Instance.HasTimeChallengedLevel(UserData.Instance.LoadedLevel))
        {
            reportPanel.FindChild("TimeCheck").gameObject.SetActive(true);
        }

        if (UserData.Instance.FinishedLevel)
        {
            minutes = Mathf.Floor(UserData.Instance.LevelTime / 60);
            seconds = Mathf.Floor(UserData.Instance.LevelTime % 60);
            fraction = (UserData.Instance.LevelTime * 100) % 100;

            timeDetails.GetComponent<Text>().text = String.Format("{0} {1}:{2}:{3}", FinishedInString(), Special(minutes), Special(seconds), Special(fraction));
        }
        else
        {
            timeDetails.GetComponent<Text>().text = DidNotFinishString();
        }

        var bestTime = UserData.Instance.GetBestLevelTime(UserData.Instance.LoadedLevel);
        if (bestTime > 0f)
        {
            minutes = Mathf.Floor(bestTime / 60);
            seconds = Mathf.Floor(bestTime % 60);
            fraction = (bestTime * 100) % 100;

            bestTimeDetails.GetComponent<Text>().text = String.Format("{0} {1}:{2}:{3}", BestString(), Special(minutes), Special(seconds), Special(fraction));
        }
        else
        {
            bestTimeDetails.GetComponent<Text>().text = "";
        }

        if (UserData.Instance.HasCoinChallengedLevel(UserData.Instance.LoadedLevel))
        {
            coinDetails.GetComponent<Text>().text = AllCoinsString();
            reportPanel.FindChild("CoinCheck").gameObject.SetActive(true);
        }
        else
        {
            coinDetails.GetComponent<Text>().text = String.Format(CoinsString(), Special(UserData.Instance.CoinsCollected));
        }

        if (UserData.Instance.HasFinishedLevel(UserData.Instance.LoadedLevel))
        {
            if (UserData.Instance.HasTimeChallengedLevel(UserData.Instance.LoadedLevel) && UserData.Instance.HasCoinChallengedLevel(UserData.Instance.LoadedLevel))
            {
                score.GetComponent<Text>().text = "A";
            }
            else if (UserData.Instance.HasTimeChallengedLevel(UserData.Instance.LoadedLevel) || UserData.Instance.HasCoinChallengedLevel(UserData.Instance.LoadedLevel))
            {
                score.GetComponent<Text>().text = "B";
            }
            else
            {
                score.GetComponent<Text>().text = "C";
            }
        }
        else
        {
            score.GetComponent<Text>().text = "F";
        }
    }

    private string CoinsString()
    {
        if (UserData.Instance.IsSpanish)
            return "{0} / 5\nMonedas Conseguidas";
        if (UserData.Instance.IsGerman)
            return "{0} / 5 Münzen\neingesammelt";
        return "COLLECTED {0} / 5 COINS";
    }

    private string AllCoinsString()
    {
        if (UserData.Instance.IsSpanish)
            return "Todas las monedas conseguidas";
        if (UserData.Instance.IsGerman)
            return "Alle Münzen\neingesammelt";
        return "All Coins Collected";
    }

    private string BestString()
    {
        if (UserData.Instance.IsSpanish)
            return "Mejor Tiempo";
        if (UserData.Instance.IsGerman)
            return "Bester:";
        return "Best";
    }

    private string DidNotFinishString()
    {
        if (UserData.Instance.IsSpanish)
            return "No completado";
        if (UserData.Instance.IsGerman)
            return "Nicht beendet";
        return "Did not Finish";
    }

    private string FinishedInString()
    {
        if (UserData.Instance.IsSpanish)
            return "Completado en\n";
        if (UserData.Instance.IsGerman)
            return "Beendet in";
        return "Finished in";
    }

    private string TargetString()
    {
        if (UserData.Instance.IsSpanish)
            return "Objetivo";
        if (UserData.Instance.IsGerman)
            return "Ziel:";
        return "Target";
    }

    private string Special(int val)
    {
        return val.ToString().Replace("0", "O");
    }

    private string Special2(int val)
    {
        return String.Format("{0:00}", val).Replace("0", "O");
    }

    private string Special(float val)
    {
        return String.Format("{0:00}", val).ToString().Replace("0", "O");
    }

    public void LoseMenu()
    {
        if (inMenu)
            return;
        inMenu = true;

        StartCoroutine(Vibrate(0.5f, 0.5f));

        if (UserData.Instance.LevelVisited(UserData.Instance.LoadedLevel))
        {
            UserData.Instance.SaveLevelProgress();
        }

        UpdateNavigation();
        UpdateReport();
        StartCoroutine(DoShowCanvas(1f));
    }

    IEnumerator Vibrate(float strength, float time)
    {
        if (UserData.Instance.Vibrate)
        {
            var names = Input.GetJoystickNames();
            if (names.Length > 0 && !string.IsNullOrEmpty(names[0]))
            {
                GamePad.SetVibration(PlayerIndex.One, strength, strength);
                yield return new WaitForSeconds(time);
                GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
            }
        }
    }

    IEnumerator DoShowCanvas(float time)
    {
        yield return new WaitForSeconds(time);
        gameMenu.gameObject.SetActive(true);
    }

    IEnumerator DoShowUnlock(float time, int unlockIndex)
    {
        yield return new WaitForSeconds(time);
        UserData.Instance.CharUnlockIndex = unlockIndex;
        UserData.Instance.CharUnlockLevel = UserData.Instance.LoadedLevel;
        Application.LoadLevel("CharacterUnlock");
    }

    IEnumerator DoShowShipUnlock(float time, int unlockIndex)
    {
        yield return new WaitForSeconds(time);
        UserData.Instance.CharUnlockIndex = unlockIndex;
        UserData.Instance.CharUnlockLevel = UserData.Instance.LoadedLevel;
        Application.LoadLevel("ShipUnlock");
    }

    private bool IsGamepadAttached()
    {
        var names = Input.GetJoystickNames();
        return names.Length > 0 && !string.IsNullOrEmpty(names[0]);
    }

    private void UpdateNavigation()
    {
        if (IsGamepadAttached())
        {
            gameMenu.FindChild("HomeBtn").FindChild("ImageC").gameObject.SetActive(true);
            gameMenu.FindChild("PrevBtn").FindChild("ImageC").gameObject.SetActive(true);
            gameMenu.FindChild("NextBtn").FindChild("ImageC").gameObject.SetActive(true);
            gameMenu.FindChild("NightmareBtn").FindChild("ImageC").gameObject.SetActive(true);
        }
        else
        {
            FocusControl(gameMenu.FindChild(UserData.Instance.FinishedLevel ? "NextBtn" : "PrevBtn"));
        }

        var name = Common.Instance.GetNextLevelName();
        if (UserData.Instance.LevelUnlocked(name))
        {
            gameMenu.FindChild("NextBtn").gameObject.SetActive(true);
        }
        else if (UserData.Instance.LoadedLevel.EndsWith("-G"))
        {
            gameMenu.FindChild("NextBtn").gameObject.SetActive(true);
        }

        if (!UserData.Instance.LoadedLevel.EndsWith("-G"))
        {
            if (UserData.Instance.LevelUnlocked(UserData.Instance.LoadedLevel + "-G"))
            {
                gameMenu.FindChild("NightmareBtn").gameObject.SetActive(true);
            }
        }
        else
        {
            gameMenu.FindChild("NightmareBtn").FindChild("Text").GetComponent<German>().Ignore = true;
            gameMenu.FindChild("NightmareBtn").FindChild("Text").GetComponent<Spanish>().Ignore = true;

            gameMenu.FindChild("NightmareBtn").gameObject.SetActive(true);
            gameMenu.FindChild("NightmareBtn").FindChild("Cross").gameObject.SetActive(true);
            gameMenu.FindChild("NightmareBtn").FindChild("Text").GetComponent<Text>().text = NormalModeString();
        }
    }

    private string NormalModeString()
    {
        if (UserData.Instance.IsSpanish)
            return "Modo normal";
        if (UserData.Instance.IsGerman)
            return "Normaler Modus";
        return "Normal Mode";
    }

    private string LevelString()
    {
        if (UserData.Instance.IsSpanish)
            return "Nivel";
        return "Level";
    }
}