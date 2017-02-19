using UnityEngine;

public class Intro10 : IntroBase
{
    public override void LoadLevel()
    {
        Application.LoadLevel("10-1");
    }

    protected override string IndexText()
    {
        if (UserData.Instance.IsSpanish)
        {
            switch (index)
            {
                case 0: return "Nos acercamos a la última fase";
                case 1: return "Los concursantes que hayan llegado hasta aquí deben mantenerse alerta";
                case 2: return "Si lo hacen lo suficientemente bien ...";
                case 3: return ".... ¡Quién sabe! ¡Quizás nuestro querido emperador les obsequie con una visita!";
                case 4: return "¡Ajá! ¡Pero no hablemos antes de tiempo!";
                case 5: return "Así que… ¡Vamos a ponernos cósmicos!";
                default:
                    return "";
            }
        }
        else if (UserData.Instance.IsGerman)
        {
            switch (index)
            {
                case 0: return "Wir nähern uns dem letzten Abschnitt";
                case 1: return "Alle Kandidaten, die es bis hierher geschafft haben, müssen wachsam bleiben";
                case 2: return "Wenn sie gut genug sind, wer weiss ...";
                case 3: return "vielleicht wird unser geschätzter Imperator sie mit einem Besuch ehren!";
                case 4: return "Haha! Aber lasst uns nicht zu früh drüber reden!";
                case 5: return "Lasst uns COSMIC werden!";
                default:
                    return "";
            }
        }
        else
        {
            switch (index)
            {
                case 0: return "we're approaching the final section";
                case 1: return "all contestants who made it this far will need to stay vigilant";
                case 2: return "if they perform well enough, who knows ...";
                case 3: return "... perhaps our treasured emperor will honor them with a visit!";
                case 4: return "ahah! but let's not talk too soon!";
                case 5: return "so let's get cosmic!";
                default:
                    return "";
            }
        }
    }
}