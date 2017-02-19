using UnityEngine;

public class Intro3 : IntroBase
{
    public override void LoadLevel()
    {
        Application.LoadLevel("3-1");
    }

    protected override string IndexText()
    {
        if (UserData.Instance.IsSpanish)
        {
            switch (index)
            {
                case 0: return "Les recordamos que el sector 22 ahora tiene unas restricciones menores";
                case 1: return "Los residentes del sector 22 deben quedarse en casa cuando llegue la noche";
                case 2: return "Además, deberán viajar siempre con un pase válido aprobado por el emperador";
                case 3: return "E intentar congregarse en grupos de dos, como máximo";
                case 4: return "Es por su seguridad, residentes del sector 22";
                default:
                    return "";
            }
        }
        else if (UserData.Instance.IsGerman)
        {
            switch (index)
            {
                case 0: return "Zur Erinnerung: Sektor 22 hat nun ein paar kleinere Einschränkungen";
                case 1: return "Einwohner von Sektor 22 müssen nach Einbruch der Dunkelheit zuhause bleiben";
                case 2: return "Reisen Sie immer mit einem gültigen, vom Imperator genehmigten Pass";
                case 3: return "und versuchen Sie in Gruppen von höchstens zwei zu bleiben";
                case 4: return "Dies dient nur Ihrer Sicherheit, Sektor 22";
                default:
                    return "";
            }
        }
        else
        {
            switch (index)
            {
                case 0: return "this is a reminder that sector 22 now has a few minor restrictions";
                case 1: return "residents of sector 22 must stay home after dark";
                case 2: return "always travel with a valid emperor approved pass";
                case 3: return "and try to congregate in groups of two at most";
                case 4: return "this is purely for your safety, sector 22";
                default:
                    return "";
            }
        }
    }
}