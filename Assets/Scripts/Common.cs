using UnityEngine;
using System;

public class Common
{
    static Common _instance;
    public static Common Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Common();
            return _instance;
        }
    }

    public const int NumLevels = 1;
    public const int NumChars = 40;
    public const int NumShips = 9;

    private System.Random _random;
    private Texture2D _screenshot;

    private Common()
    {
        _random = new System.Random();
    }

    public Planet GetClosestPlanet(Transform body)
    {
        float shortestDistance = 9999f;
        Planet closestPlanet = null;

        foreach (Collider col in Physics.OverlapSphere(body.position, 3))
        {
            if (col.transform.tag == "Planet")
            {
                RaycastHit hit;
                if (Physics.Raycast(body.position, (col.transform.position - body.position), out hit))
                {
                    if (hit.distance < shortestDistance)
                    {
                        closestPlanet = col.GetComponentInParent<Planet>();
                        shortestDistance = hit.distance;
                    }
                }

            }
        }

        return closestPlanet;
    }

    public int Random(int min, int max)
    {
        return _random.Next(min, max);
    }

    public string GetNextLevelName()
    {
        return GetNextLevelName(UserData.Instance.LoadedLevel);
    }

    public string GetNextLevelName(string levelName)
    {
        var split = levelName.Split('-');
        if (split[1] == "5")
        {
            return String.Format("{0}-1{1}", int.Parse(split[0]) + 1, split.Length == 3 ? "-G" : "");
        }
        return String.Format("{0}-{1}{2}", split[0], int.Parse(split[1]) + 1, split.Length == 3 ? "-G" : "");
    }

    public string FriendlyLevelName()
    {
        if (UserData.Instance.LoadedLevel.EndsWith("-G"))
        {
            if (UserData.Instance.IsSpanish)
                return UserData.Instance.LoadedLevel.Replace("-G", " cósmico");
            return UserData.Instance.LoadedLevel.Replace("-G", " COSMIC");
        }
        return UserData.Instance.LoadedLevel;
    }

    public float LevelPercentage(int level)
    {
        float percentage = 0;
        for (int j = 1; j <= 5; j++)
        {
            var levelName = level + "-" + j;
            if (UserData.Instance.HasFinishedLevel(levelName))
            {
                percentage += 5f;

                if (UserData.Instance.HasCoinChallengedLevel(levelName))
                {
                    percentage += 2.5f;
                }
                if (UserData.Instance.HasTimeChallengedLevel(levelName))
                {
                    percentage += 2.5f;
                }
            }

            levelName = level + "-" + j + "-G";
            if (UserData.Instance.HasFinishedLevel(levelName))
            {
                percentage += 5f;

                if (UserData.Instance.HasCoinChallengedLevel(levelName))
                {
                    percentage += 2.5f;
                }
                if (UserData.Instance.HasTimeChallengedLevel(levelName))
                {
                    percentage += 2.5f;
                }
            }
        }
        return percentage;
    }
}