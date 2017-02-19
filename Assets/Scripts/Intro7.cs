using UnityEngine;

public class Intro7 : IntroBase
{
    public override void LoadLevel()
    {
        Application.LoadLevel("7-1");
    }

    protected override string IndexText()
    {
        if (UserData.Instance.IsSpanish)
        {
            switch (index)
            {
                case 0: return "¡La última vez dije algunas cosillas que no debería!";
                case 1: return "¡Curiosamente, tropecé y me hice daño por accidente! ¡Nada que ver con lo ocurrido!";
                case 2: return "¡Pero por suerte pude hacerme con el último disco de nuestro ministro de propaganda!";
                case 3: return "¡Me animó muchísimo!";
                case 4: return "¡Qué disco tan bueno! ¡Y qué precio tan justo!";
                case 5: return "¡Sin duda, solo un loco dejaría pasar una oportunidad como esta!";
                case 6: return "¡Sobre todo porque no comprarlo es delito!";
                default:
                    return "";
            }
        }
        else if (UserData.Instance.IsGerman)
        {
            switch (index)
            {
                case 0: return "Ihr alter Spielleiter hat beim letzten mal ein paar Dinge gesagt, die er nicht hätte sagen sollen!";
                case 1: return "Ich bin ausgerutscht und habe mich bei einem nicht zusammenhängenden Ereignis verletzt!";
                case 2: return "Aber zum Glück habe ich eine Kopie vom neuesten Album unseres Propagandaministers!";
                case 3: return "Junge! Das hat mich wieder aufgerichtet!";
                case 4: return "So ein tolles Album für einen so fairen Preis!";
                case 5: return "Sie wären ein Tor, wenn Sie an diesem Album vorbeilaufen!";
                case 6: return "Besonders, weil es Gesetz ist, eine Kopie zu besitzen!";
                default:
                    return "";
            }
        }
        else
        {
            switch (index)
            {
                case 0: return "your old host said a few things he shouldn't have said last time!";
                case 1: return "incidently i slipped and hurt myself in a completely unrelated incident!";
                case 2: return "but fortunately i got a copy of our propaganda minister's latest album!";
                case 3: return "boy! did that cheer me up!";
                case 4: return "such a great album for such a fair asking price!";
                case 5: return "surely you'd be a fool to walk past this album!";
                case 6: return "you know, especially since it's the law to own a copy!";
                default:
                    return "";
            }
        }
    }
}