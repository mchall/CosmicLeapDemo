using UnityEngine;

public class Intro8 : IntroBase
{
    public override void LoadLevel()
    {
        Application.LoadLevel("8-1");
    }

    protected override string IndexText()
    {
        if (UserData.Instance.IsSpanish)
        {
            switch (index)
            {
                case 0: return "¡El trabajo en equipo es vital en esta fase!";
                case 1: return "¡Los concursantes deberán ayudarse mutuamente para salir con vida!";
                case 2: return "Como si fueran hormigas, abejas u otros miserables insectos.";
                case 3: return "Pero tú no eres así, espectador.Sabemos que eres diferente";
                case 4: return "¡Y que quieres ser el centro de atención de las fotos de grupo!";
                case 5: return "¡Quieres que el mundo vea que no eres como los demás!";
                case 6: return "¡Y por eso necesitas las nuevas zapatillas Cosmic! ¡Que todos sepan lo único que eres!";
                case 7: return "¡Zapatillas Cosmic! ¡Si no las tienes es que no vale la pena hablar contigo!";
                default:
                    return "";
            }
        }
        else if (UserData.Instance.IsGerman)
        {
            switch (index)
            {
                case 0: return "Im nächsten Abschnitt geht es um Zusammenarbeit!";
                case 1: return "Unsere Kandidaten müssen sich gemeinsam in Sicherheit bringen!";
                case 2: return "Wie Ameisen, oder Bienen, oder andere niedere Insekten";
                case 3: return "Aber von Ihnen zuhause wissen wir, dass Sie Individuen sind";
                case 4: return "Sie möchten im Gruppen-Selfie hervorstechen!";
                case 5: return "Sie möchten der Welt zeigen, dass Sie nicht wie jeder andere sind!";
                case 6: return "Darum wollen Sie Cosmic Sneaker! Zum ultimativen Vorzeigen Ihrer Individualität!";
                case 7: return "Cosmic Sneaker! Wenn Sie kein Paar besitzen, sind Sie es nicht Wert, drüber zu reden.";
                default:
                    return "";
            }
        }
        else
        {
            switch (index)
            {
                case 0: return "the next section is all about teamwork!";
                case 1: return "our contestants must all work as one to all reach safety!";
                case 2: return "kind of like ants, or bees, or other lowly insects";
                case 3: return "but for you at home, we know you're an individual";
                case 4: return "you want to stick out in that group selfie!";
                case 5: return "you want to show the world you're not like anyone else!";
                case 6: return "that's why you want cosmic sneakers! they're the ultimate statement of individuality!";
                case 7: return "cosmic sneakers! you're not worth talking to if you don't own a pair!";
                default:
                    return "";
            }
        }
    }
}