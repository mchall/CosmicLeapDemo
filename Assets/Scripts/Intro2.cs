using UnityEngine;

public class Intro2 : IntroBase
{
    public override void LoadLevel()
    {
        Application.LoadLevel("2-1");
    }

    protected override string IndexText()
    {
        if (UserData.Instance.IsSpanish)
        {
            switch (index)
            {
                case 0: return "No sé vosotros ...";
                case 1: return "¡Pero todo esto me está dando ganas de tomarme un buen vaso de Cosmic Cola!";
                case 2: return "¡Ya a la venta en el sector 22!";
                case 3: return "Haz que tu día sea perfecto. ¡Bebe Cosmic Cola!";
                default:
                    return "";
            }
        }
        else if (UserData.Instance.IsGerman)
        {
            switch (index)
            {
                case 0: return "Ich weiss nicht, wie es Ihnen geht ...";
                case 1: return "Aber dies alles macht mich durstig auf einen grossen Schluck Cosmic Cola!";
                case 2: return "Jetzt in Sektor 22 verfügbar!";
                case 3: return "Ihr Tag ist nicht perfekt ohne eine Cosmic Cola!";
                default:
                    return "";
            }
        }
        else
        {
            switch (index)
            {
                case 0: return "i don't know about you guys ...";
                case 1: return "but all this is making me thirsty for a big jug of cosmic cola!";
                case 2: return "now available in sector 22!";
                case 3: return "your day isn't complete without a drink of cosmic cola!";
                default:
                    return "";
            }
        }
    }
}