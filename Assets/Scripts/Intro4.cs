using UnityEngine;

public class Intro4 : IntroBase
{
    public override void LoadLevel()
    {
        Application.LoadLevel("4-1");
    }

    protected override string IndexText()
    {
        if (UserData.Instance.IsSpanish)
        {
            switch (index)
            {
                case 0: return "¡Chicas! Queréis tener un pelo increíble, manejable y brillante";
                case 1: return "¡Lo que necesitáis es la nueva Laca Cosmic!";
                case 2: return "¡Despierta pasión entre tus amigos y envidia entre tus enemigos!";
                case 3: return "Si fuera una chica, yo lo usaría, sin duda ...";
                case 4: return "... ¡Lo cierto es que la uso igual! ¡También pueden usarla los hombres!";
                case 5: return "Recuerda, no solamente es la mejor laca del mercado ...";
                case 6: return "... ¡Es también la única que aprueba nuestro querido emperador!";
                default:
                    return "";
            }
        }
        else if (UserData.Instance.IsGerman)
        {
            switch (index)
            {
                case 0: return "Meine Damen!Sind Sie auf der Suche nach schönem, tragbarem, üppigem und glänzendem Haar";
                case 1: return "Suchen Sie nicht weiter als bis zur nächsten Dose Cosmic - Haarspray!";
                case 2: return "Lassen Sie sich von jenen bewundern, die Sie mögen, und beneiden von allen anderen!";
                case 3: return "Wenn ich eine Dame wäre, ich würde es auf jeden Fall benutzen ...";
                case 4: return "... tatsächlich benutze ich es auch! Funktioniert auch bei den Herren!";
                case 5: return "Bedenken Sie: es ist nicht nur das beste Spray auf dem Markt!";
                case 6: return "Es ist auch das einzige vom Imperator genehmigte Spray!";
                default:
                    return "";
            }
        }
        else
        {
            switch (index)
            {
                case 0: return "ladies! are you looking for beautiful, manageable, luscious and lustrous hair";
                case 1: return "look no further than for a can of cosmic hair spray!";
                case 2: return "be the desire of those you fancy and the envy of those you don't!";
                case 3: return "if i were a lady i'd definitely use it ...";
                case 4: return "... in fact i use it anyway! it works for men too!";
                case 5: return "remember it's not only the best spray on the market!";
                case 6: return "it's also the only spray approved by our beloved emperor!";
                default:
                    return "";
            }
        }
    }
}