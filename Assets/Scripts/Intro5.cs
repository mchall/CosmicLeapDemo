using UnityEngine;

public class Intro5 : IntroBase
{
    public override void LoadLevel()
    {
        Application.LoadLevel("5-1");
    }

    protected override string IndexText()
    {
        if (UserData.Instance.IsSpanish)
        {
            switch (index)
            {
                case 0: return "¡Esto empieza a caldearse!";
                default:
                    return "";
            }
        }
        else if (UserData.Instance.IsGerman)
        {
            switch (index)
            {
                case 0: return "Da draussen wird's immer heisser!";
                default:
                    return "";
            }
        }
        else
        {
            switch (index)
            {
                case 0: return "things are about to 'heat up' out there!";
                default:
                    return "";
            }
        }
    }
}