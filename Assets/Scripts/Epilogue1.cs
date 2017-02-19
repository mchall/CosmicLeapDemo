using UnityEngine;

public class Epilogue1 : IntroBase
{
    float? totalProgress;

    public override void LoadLevel()
    {
        if (UnlockedCrewSize() > 30 && UserData.Instance.UnlockCharacter(39))
        {
            UserData.Instance.CharUnlockIndex = 39;
            UserData.Instance.CharUnlockLevel = "Menu";

            Application.LoadLevel("CharacterUnlock");
        }
        else if (totalProgress >= 75 && UserData.Instance.UnlockCharacter(40))
        {
            UserData.Instance.CharUnlockIndex = 40;
            UserData.Instance.CharUnlockLevel = "Menu";
            Application.LoadLevel("CharacterUnlock");
        }
        else if (totalProgress >= 75)
        {
            Application.LoadLevel("Epilogue2");
        }
        else
            Application.LoadLevel("Menu");
    }

    protected override string IndexText()
    {
        totalProgress = totalProgress ?? TotalProgress();

        if (UserData.Instance.IsSpanish)
        {
            switch (index)
            {
                case 0: return "Campeón de Cosmic Leap ... Estás en presencia del emperador";
                case 1: return "Pocos han sido capaces de conseguir lo que tú has conseguido";
                case 2: return "Pero no esperaba menos de alguien de mi estirpe";
                case 3: return "Sí .... Así es ...";
                case 4: return "Duke ... ¡Yo soy tu padre!";
                case 5: return "Ahora que estás aquí, veo todos los errores que he cometido ...";
                case 6: return "... Como padre, y como emperador.";
                case 7: return "Pero con tu ayuda, ¡podremos arreglarlo todo!";
                case 8: return "¡Podemos hacer del universo un lugar mejor! ¡Padre e hijo, codo con codo!";
                case 9: return "Pero antes… Veamos qué tal lo hiciste ...";
                case 10: return "Tienes una tripulación de " + UnlockedCharsString() + " miembros...";
                case 11: return Index11();
                case 12: return Index12();
                case 13: return Index13();
                case 14: return "Vamos a echar un vistazo a tus progresos ...";
                case 15: return "Has completado un " + TotalProgressString() + "% de Cosmic Leap ...";
                case 16: return Index16();
                case 17: return Index17();
                case 18: return Index18();
                case 19: return Index19();
                case 20: return Index20();
                default:
                    return "";
            }
        }
        else if (UserData.Instance.IsGerman)
        {
            switch (index)
            {
                case 0: return "Champion des Cosmic Leap ... du stehst vor deinem Imperator";
                case 1: return "Nicht viele haben geschafft, was du erreicht hast";
                case 2: return "Aber von meinem eigenen Fleisch und Blut habe ich auch nicht weniger erwartet";
                case 3: return "Ja ... es ist wahr ...";
                case 4: return "Duke ... ich bin dein Vater!";
                case 5: return "Wie du vor mir stehst kann ich all die Fehler sehen, die ich gemacht habe ...";
                case 6: return "... als Vater, und als Imperator.";
                case 7: return "Aber mit dir an meiner Seite können wir meine Fehler ausbügeln!";
                case 8: return "Wir können dieses Universum verbessern! Vater und Sohn, Seite an Seite!";
                case 9: return "Aber lass' uns zuerst sehen, wie gut du warst ...";
                case 10: return "Du hast eine Crew von " + UnlockedCharsString() + " ...";
                case 11: return Index11();
                case 12: return Index12();
                case 13: return Index13();
                case 14: return "Nun lass' uns deinen Fortschritt ansehen...";
                case 15: return "Du hast " + TotalProgressString() + "% des Cosmic Leap vollendet...";
                case 16: return Index16();
                case 17: return Index17();
                case 18: return Index18();
                case 19: return Index19();
                case 20: return Index20();
                default:
                    return "";
            }
        }
        else
        {
            switch (index)
            {
                case 0: return "champion of the cosmic leap ... you stand before your emperor";
                case 1: return "not many have succeeded in doing what you've accomplished";
                case 2: return "but i wouldn't have expected anything less from my own flesh and blood";
                case 3: return "yes ... it's true ...";
                case 4: return "duke ... i am your father!";
                case 5: return "with you before me i can see all the mistakes i've made ...";
                case 6: return "... as a father, and as an emperor.";
                case 7: return "but with you at my side, we can right my wrongs!";
                case 8: return "we can make this universe better! as father and son, side by side!";
                case 9: return "but, first let's see how well you did...";
                case 10: return "you have a crew of " + UnlockedCharsString() + " leapers...";
                case 11: return Index11();
                case 12: return Index12();
                case 13: return Index13();
                case 14: return "now let's look a little at your progress...";
                case 15: return "you completed " + TotalProgressString() + "% of the cosmic leap...";
                case 16: return Index16();
                case 17: return Index17();
                case 18: return Index18();
                case 19: return Index19();
                case 20: return Index20();
                default:
                    return "";
            }
        }
    }

    private string Index11()
    {
        if (UnlockedCrewSize() > 30)
        {
            if (UserData.Instance.IsSpanish)
                return "¡Sin duda tienes madera de líder!";
            if(UserData.Instance.IsGerman)
                return "Du hast offensichtlich einiges an Führungstalent!";
            return "you obviously have some talent as a leader!";
        }
        if (UserData.Instance.IsSpanish)
            return "... ¡Vuelve cuando tengas una tripulación de al menos 3o!";
        if (UserData.Instance.IsGerman)
            return "... komme wieder, wenn du eine Crew von mindestens 3o hast!";
        return "... come back when you've got yourself a crew of at least 3o!";
    }

    private string Index12()
    {
        if (UnlockedCrewSize() > 30)
        {
            if (UserData.Instance.IsSpanish)
                return "Liberaré al antiguo líder rebelde del sector 15.";
            if (UserData.Instance.IsGerman)
                return "Ich gewähre dem alten Rebellenanführer aus Sektor 15 seine Freiheit.";
            return "I'll grant the old sector 15 rebel leader his freedom.";
        }
        index++;
        return Index13();
    }

    private string Index13()
    {
        if (UnlockedCrewSize() > 30)
        {
            if (UserData.Instance.IsSpanish)
                return "Para que pueda unirse a tu tripulación.";
            if (UserData.Instance.IsGerman)
                return "Ihm ist freigestellt, deiner Crew beizutreten und dir zu dienen.";
            return "He's free to join your crew and serve you.";
        }
        index++;
        if (UserData.Instance.IsSpanish)
            return "Vamos a echar un vistazo a tus progresos ...";
        if (UserData.Instance.IsGerman)
            return "Nun lass' uns deinen Fortschritt ansehen...";
        return "now let's look a little at your progress...";
    }

    private string Index16()
    {
        if (totalProgress >= 75)
        {
            if (UserData.Instance.IsSpanish)
                return "¡Lo has hecho mejor de lo que esperaba!";
            if (UserData.Instance.IsGerman)
                return "Du hast meine Erwartungen übertroffen!";
            return "You've exceeded my expectations!";
        }
        if (UserData.Instance.IsSpanish)
            return "... ¡Vuelve cuando hayas completado un 75% del juego!";
        if (UserData.Instance.IsGerman)
            return "... komme wieder, wenn du die 75%-Marke überschritten hast!";
        return "... come back later when you've passed the 75% mark!";
    }

    private string Index17()
    {
        if (totalProgress >= 75)
        {
            if (UserData.Instance.IsSpanish)
                return "¡No puedo imaginar mayor honor que ser tu mano derecha!";
            if (UserData.Instance.IsGerman)
                return "Ich kann an keine grössere Ehre denken, als dir als Stellvertreter zu dienen.";
            return "I can think of no greater honor than serving as your second-in-command!";
        }
        return "";
    }

    private string Index18()
    {
        if (totalProgress >= 75)
        {
            if (UserData.Instance.IsSpanish)
                return "¡Ahora, estos sectores te pertenecen! ¡Haz con ellos lo que te plazca!";
            if (UserData.Instance.IsGerman)
                return "Du bist der neue Imperator dieser Sektoren! Mache mit ihnen, was du möchtest!";
            return "You're the new emperor of these sectors! Do with them as you wish!";
        }
        return "";
    }

    private string Index19()
    {
        if (totalProgress >= 75)
        {
            if (UserData.Instance.IsSpanish)
                return "¡Felicidades, y gracias por jugar, campeón!";
            if (UserData.Instance.IsGerman)
                return "Glückwünsche, und danke für's Spielen, Champion!";
            return "Congratulations and thank you for playing, champion!";
        }
        return "";
    }

    private string Index20()
    {
        if (totalProgress >= 75)
            return "...";
        return "";
    }

    private float TotalProgress()
    {
        float total = 0;
        for (int i = 1; i <= Common.NumLevels; i++)
        {
            total += Mathf.Round(Common.Instance.LevelPercentage(i));
        }
        return Mathf.Round(total / Common.NumLevels);
    }

    private int UnlockedCrewSize()
    {
        return UserData.Instance.UnlockedCharacters.Count - 1;
    }

    private string TotalProgressString()
    {
        return string.Format("{0}", totalProgress.Value).ToString().Replace("0", "O");
    }

    private string UnlockedCharsString()
    {
        return string.Format("{0}", UnlockedCrewSize()).ToString().Replace("0", "O");
    }
}