using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public enum LevelState
{
    Locked = 0,
    Unlocked = 1,
    Visited = 2,
    Finished = 3,
    Coins = 4,
    Time = 5,
    AllDone = 6,
}

[Serializable]
public class GameData
{
    public List<int> UnlockedChars { get; set; }
    public List<int> UnlockedShips { get; set; }
    public int CharacterIndex { get; set; }
    public int ShipIndex { get; set; }
    public bool Force2d { get; set; }
    public float SoundVolume { get; set; }
    public float MusicVolume { get; set; }
    public bool Quality { get; set; }
    public bool BatterySaver { get; set; }
    public bool GlitchEffect { get; set; }
    public bool Vibrate { get; set; }
    public bool Accessibility { get; set; }
    public bool IgnoreControllerDetect { get; set; }

    public GameData()
    {
        UnlockedChars = new List<int>();
        UnlockedShips = new List<int>();
        UnlockedChars.Add(1);
        UnlockedShips.Add(1);
        CharacterIndex = 1;
        ShipIndex = 1;

        Force2d = false;
        SoundVolume = 1f;
        MusicVolume = 0.3f;
        Quality = true;
        BatterySaver = false;
        GlitchEffect = true;
        Vibrate = true;
        Accessibility = false;
        IgnoreControllerDetect = false;
    }
}

[Serializable]
public class LevelProgress
{
    public LevelState[] LevelData { get; set; }
    public float[] LevelTimes { get; set; }

    public LevelProgress()
    {
        LevelData = new LevelState[200];
        LevelTimes = new float[200];
        LevelData[0] = LevelState.Unlocked;
    }
}

public class UserData
{
    static UserData _instance;
    public static UserData Instance
    {
        get
        {
            if (_instance == null)
                _instance = new UserData();
            return _instance;
        }
    }

    private GameData _data;
    private LevelProgress _levelProgress;

    public UserData()
    {
        _data = new GameData();
        _levelProgress = new LevelProgress();

        LoadData();
        LoadLevelProgress();
    }

    public int CoinsCollected { get; set; }
    public float LevelTime { get; set; }
    public bool FinishedLevel { get; set; }
    public int RetryCount { get; set; }
    public int CurrentHighScore { get; set; }

    public string CurrentLevel { get; set; }

    public string LoadedLevel
    {
        get
        {
            return Application.loadedLevelName;
        }
    }

    public string CharUnlockLevel { get; set; }
    public int CharUnlockIndex { get; set; }
    public int CurrentRandomIndex { get; set; }

    public List<int> UnlockedCharacters
    {
        get { return _data.UnlockedChars ?? new List<int>() { 1 }; }
    }

    public List<int> UnlockedShips
    {
        get { return _data.UnlockedShips ?? new List<int>() { 1 }; }
    }

    public bool IsSpanish
    {
        get { return false; }
    }

    public bool IsGerman
    {
        get { return false; }
    }

    public bool Force2d
    {
        get { return _data.Force2d; }
        set
        {
            _data.Force2d = value;
            SaveData();
        }
    }

    public float SoundVolume
    {
        get { return _data.SoundVolume; }
        set
        {
            _data.SoundVolume = value;
            SaveData();
        }
    }

    public float MusicVolume
    {
        get { return _data.MusicVolume; }
        set
        {
            _data.MusicVolume = value;
            SaveData();
        }
    }

    public bool Quality
    {
        get { return _data.Quality; }
        set
        {
            _data.Quality = value;
            SaveData();
        }
    }

    public bool BatterySaver
    {
        get { return _data.BatterySaver; }
        set
        {
            _data.BatterySaver = value;
            SaveData();
        }
    }

    public bool GlitchEffect
    {
        get { return _data.GlitchEffect; }
        set
        {
            _data.GlitchEffect = value;
            SaveData();
        }
    }

    public bool Vibrate
    {
        get { return _data.Vibrate; }
        set
        {
            _data.Vibrate = value;
            SaveData();
        }
    }

    public bool Accessibility
    {
        get { return _data.Accessibility; }
        set
        {
            _data.Accessibility = value;
            SaveData();
        }
    }

    public bool IgnoreControllerDetect
    {
        get { return _data.IgnoreControllerDetect; }
        set
        {
            _data.IgnoreControllerDetect = value;
            SaveData();
        }
    }

    public bool UnlockCharacter(int index)
    {
        _data.UnlockedChars = _data.UnlockedChars ?? new List<int>();

        if (!_data.UnlockedChars.Contains(index))
        {
            _data.UnlockedChars.Add(index);
            SaveData();
            return true;
        }
        return false;
    }

    public bool UnlockShip(int index)
    {
        _data.UnlockedShips = _data.UnlockedShips ?? new List<int>();

        if (!_data.UnlockedShips.Contains(index))
        {
            _data.UnlockedShips.Add(index);
            SaveData();
            return true;
        }
        return false;
    }

    private void LevelDataCheck()
    {
        if (_levelProgress.LevelData == null)
        {
            _levelProgress.LevelData = new LevelState[200];
            _levelProgress.LevelData[0] = LevelState.Unlocked;
        }
        if (_levelProgress.LevelTimes == null)
        {
            _levelProgress.LevelTimes = new float[200];
        }
    }

    public float GetBestLevelTime(string levelName)
    {
        LevelDataCheck();

        var index = LevelIndexFromName(levelName);
        return _levelProgress.LevelTimes[index];
    }

    public bool RecordBestLevelTime(string levelName, float levelTime)
    {
        LevelDataCheck();

        var index = LevelIndexFromName(levelName);
        var time = _levelProgress.LevelTimes[index];

        if (time == 0f || levelTime < time)
        {
            _levelProgress.LevelTimes[index] = levelTime;
            return true;
        }
        return false;
    }

    private int LevelIndexFromName(string name)
    {
        var split = name.Split('-');

        var level = int.Parse(split[0]);
        var subLevel = int.Parse(split[1]);
        var isglitch = name.EndsWith("-G");

        var index = (level - 1) * 10;
        index += subLevel - 1;
        if (isglitch)
            index += 5;

        return index;
    }

    public bool UnlockLevel(string levelName)
    {
        LevelDataCheck();

        var index = LevelIndexFromName(levelName);
        var state = _levelProgress.LevelData[index];

        if (state == LevelState.Locked)
        {
            _levelProgress.LevelData[index] = LevelState.Unlocked;
            return true;
        }
        return false;
    }

    public bool LevelFinished(string levelName)
    {
        LevelDataCheck();

        var index = LevelIndexFromName(levelName);
        var state = _levelProgress.LevelData[index];

        if (state != LevelState.Coins && state != LevelState.Time && state != LevelState.AllDone)
        {
            _levelProgress.LevelData[index] = LevelState.Finished;
            return true;
        }
        return false;
    }

    public bool LevelVisited(string levelName)
    {
        LevelDataCheck();

        var index = LevelIndexFromName(levelName);
        var state = _levelProgress.LevelData[index];

        if (state == LevelState.Unlocked)
        {
            _levelProgress.LevelData[index] = LevelState.Visited;
            return true;
        }
        return false;
    }

    public void TimeChallengeCompleted(string levelName)
    {
        LevelDataCheck();

        var index = LevelIndexFromName(levelName);
        var state = _levelProgress.LevelData[index];

        if (state == LevelState.AllDone)
            return;
        else if (state == LevelState.Coins)
            _levelProgress.LevelData[index] = LevelState.AllDone;
        else
            _levelProgress.LevelData[index] = LevelState.Time;
    }

    public void CoinChallengeCompleted(string levelName)
    {
        LevelDataCheck();

        var index = LevelIndexFromName(levelName);
        var state = _levelProgress.LevelData[index];

        if (state == LevelState.AllDone)
            return;
        else if (state == LevelState.Time)
            _levelProgress.LevelData[index] = LevelState.AllDone;
        else
            _levelProgress.LevelData[index] = LevelState.Coins;
    }

    public bool LevelUnlocked(string levelName)
    {
        LevelDataCheck();

        var index = LevelIndexFromName(levelName);
        var state = _levelProgress.LevelData[index];

        return state != LevelState.Locked;
    }

    public bool HasFinishedLevel(string levelName)
    {
        LevelDataCheck();

        var index = LevelIndexFromName(levelName);
        var state = _levelProgress.LevelData[index];

        return state == LevelState.AllDone ||
               state == LevelState.Coins ||
               state == LevelState.Finished ||
               state == LevelState.Time;
    }

    public bool HasVisitedLevel(string levelName)
    {
        LevelDataCheck();

        var index = LevelIndexFromName(levelName);
        var state = _levelProgress.LevelData[index];

        if (state == LevelState.Locked || state == LevelState.Unlocked)
            return false;
        return true;
    }

    public bool HasTimeChallengedLevel(string levelName)
    {
        LevelDataCheck();

        var index = LevelIndexFromName(levelName);
        var state = _levelProgress.LevelData[index];

        return state == LevelState.Time || state == LevelState.AllDone;
    }

    public bool HasCoinChallengedLevel(string levelName)
    {
        LevelDataCheck();

        var index = LevelIndexFromName(levelName);
        var state = _levelProgress.LevelData[index];

        return state == LevelState.Coins || state == LevelState.AllDone;
    }

    public bool HasAGrade()
    {
        LevelDataCheck();

        foreach (var item in _levelProgress.LevelData)
        {
            if (item == LevelState.AllDone)
                return true;
        }
        return false;
    }

    public void SetCharacterIndex(int index)
    {
        _data.CharacterIndex = index;
        SaveData();
    }

    public int GetCharacterIndex()
    {
        if (_data.CharacterIndex == -1)
        {
            var index = Common.Instance.Random(0, UnlockedCharacters.Count);
            CurrentRandomIndex = UnlockedCharacters[index];
            return CurrentRandomIndex;
        }
        return _data.CharacterIndex;
    }

    public int GetRandomCharacterIndex()
    {
        var index = Common.Instance.Random(0, UnlockedCharacters.Count);
        return UnlockedCharacters[index];
    }

    public int GetLevelCharacter()
    {
        if (_data.CharacterIndex == -1)
        {
            if (CurrentRandomIndex > 0)
                return CurrentRandomIndex;
            return 1;
        }
        return _data.CharacterIndex;
    }

    public int GetExplicitCharacterIndex()
    {
        return _data.CharacterIndex;
    }

    public void SetShipIndex(int index)
    {
        _data.ShipIndex = index;
        SaveData();
    }

    public int GetShipIndex()
    {
        return _data.ShipIndex;
    }

    private string DataFile
    {
        get { return Application.dataPath + "/userdata.bin"; }
    }

    private string LevelProgressFile
    {
        get { return Application.dataPath + "/levels.bin"; }
    }

    private void LoadData()
    {
        if (File.Exists(DataFile))
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream file = File.Open(DataFile, FileMode.Open))
            {
                try
                {
                    _data = (GameData)bf.Deserialize(file);
                }
                catch
                {
                    _data = new GameData();
                }
            }
        }
    }

    private void LoadLevelProgress()
    {
        if (File.Exists(LevelProgressFile))
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream file = File.Open(LevelProgressFile, FileMode.Open))
            {
                try
                {
                    _levelProgress = (LevelProgress)bf.Deserialize(file);
                }
                catch
                {
                    _levelProgress = new LevelProgress();
                }
            }
        }
    }

    public void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream file = File.Create(DataFile))
        {
            bf.Serialize(file, _data);
        }
    }

    public void SaveLevelProgress()
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream file = File.Create(LevelProgressFile))
        {
            bf.Serialize(file, _levelProgress);
        }
    }
}