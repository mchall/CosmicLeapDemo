using UnityEngine;

public class Intro9 : IntroBase
{
    public override void LoadLevel()
    {
        Application.LoadLevel("9-1");
    }

    protected override string IndexText()
    {
        if (UserData.Instance.IsSpanish)
        {
            switch (index)
            {
                case 0: return "Me gustaría desmentir ciertos rumores que hemos recibido.";
                case 1: return "Susurros sobre una rebelión en el sector 25.";
                case 2: return "Nada más lejos de la realidad";
                case 3: return "El sector 25 siempre ha sido uno de los lugares favoritos de nuestro querido emperador ...";
                case 4: return "... Y sus habitantes siempre han sido de lo más devotos hacia su alteza.";
                case 5: return "Y aunque es cierto que se ha restringido el acceso al sector 25 ...";
                case 6: return "... ¡Solo es una medida preventiva para reducir las emisiones de combustible espacial!";
                case 7: return "Así que piensa antes de actuar. ¡No des cuerda a rumores infundados!";
                default:
                    return "";
            }
        }
        else if (UserData.Instance.IsGerman)
        {
            switch (index)
            {
                case 0: return "Ich möchte einige Gerüchte ansprechen, die wir gehört haben";
                case 1: return "Ich habe Geflüster über eine Rebellion in Sektor 25 gehört";
                case 2: return "Das ist schlicht und ergreifend Unsinn";
                case 3: return "Sektor 25 ist schon immer einer der Lieblings-Sektoren unseres geliebten Imperators gewesen ...";
                case 4: return "... und im Gegenzug haben ihn die Menschen in Sektor 25 auch immer geliebt";
                case 5: return "Nun, es ist wahr, dass der Zutritt zu Sektor 25 eingeschränkt wurde";
                case 6: return "Aber dies ist nur eine Massnahme zur Reduzierung von Raketenabgasen in diesem Bereich";
                case 7: return "Seien Sie schlau und verbreiten Sie keine haltlosen Gerüchte!";
                default:
                    return "";
            }
        }
        else
        {
            switch (index)
            {
                case 0: return "i want to address some rumors we've been hearing";
                case 1: return "i've heard whispers about a rebellion in sector 25";
                case 2: return "this is simply nonsense";
                case 3: return "sector 25 has always been one of our beloved emperor's favourite sectors ...";
                case 4: return "... and in turn the people of sector 25 have always loved him back";
                case 5: return "now, it's true access to sector 25 has been restricted";
                case 6: return "but this is purely a measure to reduce rocketfuel emissions in the area!";
                case 7: return "so be smart. don't spread unfounded rumors!";
                default:
                    return "";
            }
        }
    }
}