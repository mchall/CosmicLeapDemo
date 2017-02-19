using UnityEngine;

public class Intro6 : IntroBase
{
    public override void LoadLevel()
    {
        Application.LoadLevel("6-1");
    }

    protected override string IndexText()
    {
        if (UserData.Instance.IsSpanish)
        {
            switch (index)
            {
                case 0: return "Algunos espectadores quizás me recuerden como el líder rebelde del sector 15";
                case 1: return "¡Pero eso fue hace mucho tiempo! ¡Mirad lo lejos que he llegado!";
                case 2: return "¡Los concursantes están a punto de entrar en el sinuoso mundo de los agujeros de gusano!";
                case 3: return "Te aseguro que si mi tripulación hubiera sabido controlarlos, ¡las cosas serían muy distintas!";
                case 4: return "¡Ajá! ¡Pero eso es cosa del pasado!";
                case 5: return "... ¡Que siga el espectáculo!";
                default:
                    return "";
            }
        }
        else if (UserData.Instance.IsGerman)
        {
            switch (index)
            {
                case 0: return "Einige unserer Zuschauer erkennen in mir vielleicht den ehemaligen Rebellen - Anführer aus Sektor 15";
                case 1: return "Aber das ist lange her!Ich meine, schauen Sie, wie weit ich gekommen bin!";
                case 2: return "Unsere Kandidaten werden jetzt die gewundene Welt der Wurmlöcher betreten";
                case 3: return "Ich sage Ihnen, wenn meine Crew damals hätte Wurmlöcher kontrollieren können, wären die Dinge komplett anders gelaufen!";
                case 4: return "Haha! Aber das ist jetzt Vergangenheit!";
                case 5: return "...weiter mit der Show!";
                default:
                    return "";
            }
        }
        else
        {
            switch (index)
            {
                case 0: return "some of our viewers may recognize me as the old rebel leader of sector 15";
                case 1: return "but that was a long time ago! i mean, look at how far i've come now!";
                case 2: return "our contestants are about to encounter the loopy world of wormholes!";
                case 3: return "i tell you, if my crew could control wormholes back then things might be very different!";
                case 4: return "ahah! but that's all in the past!";
                case 5: return "... on with the show!";
                default:
                    return "";
            }
        }
    }
}