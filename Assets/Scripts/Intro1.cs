using UnityEngine;

public class Intro1 : IntroBase
{
    public override void LoadLevel()
    {
        Application.LoadLevel("1-1");
    }

    protected override string IndexText()
    {
        if (UserData.Instance.IsSpanish)
        {
            switch (index)
            {
                case 0: return "¡Bienvenidos a una nueva temporada de Cosmic Leap!";
                case 1: return "¡Este ha sido otro glorioso año para nuestro querido emperador!";
                case 2: return "¡Nos han concedido nuevos concursantes para esta nueva edición!";
                case 3: return "¡Provenientes del sector 22, recién liberado!";
                case 4: return "¡Así que si nos estás viendo desde el sector 22, bienvenido a la civilización!";
                case 5: return "¡Y disculpa que hayamos destruido vuestro querido sol!";
                case 6: return "¡Pero ya está bien de política!";
                case 7: return "Puede que nuestros concursantes sean novatos ... ";
                case 8: return "Pero como antiguos miembros de la rebelión, deben saber bastante ...";
                case 9: return "... ¡Sobre la derrota!";
                case 10: return "¡Ajá! ¡Pero en serio, repasemos las normas!";
                case 11: return "Para ganar, los concursantes deben llegar a su nave.";
                case 12: return "Para obtener una mejor puntuación, deberán conseguir todas las monedas";
                case 13: return "... O completar la fase dentro del tiempo objetivo";
                case 14: return "Si lo consiguen, podrán acceder a los niveles cósmicos";
                case 15: return "¡Y si son lo bastante buenos, quizás llamen la atención de nuestro querido emperador!";
                case 16: return "¡Se acabó la cháchara!";
                case 17: return "¡Vamos a ponernos CÓSMICOS!";
                default:
                    return "";
            }
        }
        else if (UserData.Instance.IsGerman)
        {
            switch (index)
            {
                case 0: return "Willkommen zu einer weiteren Cosmic Leap-Spielzeit!";
                case 1: return "Es war ein weiteres ruhmreiches Jahr für unseren hochverehrten Imperator!";
                case 2: return "Dieses Jahr werden wir mit einigen neuen Kandidaten verwöhnt!";
                case 3: return "Sie kommen den ganzen Weg vom kürzlich befreiten Sektor 22!";
                case 4: return "Wenn Sie von Sektor 22 zuschauen: Willkommen in der Zivilisation!";
                case 5: return "Und Entschuldigung dafür, Ihre lebensspendende Sonne zerstört zu haben!";
                case 6: return "Nun aber genug der Politik!";
                case 7: return "Unsere Kandidaten mögen vielleicht neue Gesichter sein ...";
                case 8: return "aber als ehemalige Kämpfer der Rebellion wissen sie das eine oder andere...";
                case 9: return "... über's Verlieren!";
                case 10: return "Haha! Aber jetzt im ernst, jetzt zu den Regeln.";
                case 11: return "Unsere Kandidaten müssen ihr Raumschiff erreichen um zu gewinnen";
                case 12: return "Für bessere Noten werden sie alle Münzen einsammeln müssen";
                case 13: return "oder innerhalb der Level-Zeit bleiben";
                case 14: return "Wenn sie dies schaffen, entsperren sie die Cosmic-Level";
                case 15: return "Vielleicht erhaschen sie die Aufmerksamkeit unseres hochverehrten Imperators, wenn sie gut genug sind!";
                case 16: return "Genug geredet!";
                case 17: return "Lasst uns COSMIC werden!";
                default:
                    return "";
            }
        }
        else
        {
            switch (index)
            {
                case 0: return "welcome to another season of cosmic leap!";
                case 1: return "it's been another glorious year for our esteemed emperor!";
                case 2: return "this year we're spoiled with some new contestants!";
                case 3: return "all the way from the newly liberated sector 22!";
                case 4: return "so if you're watching from sector 22, welcome to civilization!";
                case 5: return "and sorry about destroying your life giving sun!";
                case 6: return "but enough about politics!";
                case 7: return "our contestants may be fresh faces ...";
                case 8: return "but as former members of the rebellion they know a thing or two...";
                case 9: return "... about losing!";
                case 10: return "ahah! but seriously, let's go over the basics!";
                case 11: return "our contestants will need to get to their rocketship to win";
                case 12: return "for a better grade they'll need to collect all the coins";
                case 13: return "or finish within the level time";
                case 14: return "if they do that they'll unlock the cosmic levels";
                case 15: return "if they're good enough maybe they'll catch the eye of our esteemed emperor!";
                case 16: return "but enough talking!";
                case 17: return "Let's get COSMIC!";
                default:
                    return "";
            }
        }
    }
}