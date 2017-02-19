using UnityEngine;
using UnityEngine.UI;

public class German : MonoBehaviour
{
    public string Translation;
    public string Line2;
    public string Line3;
    public string Line4;
    public string Line5;

    public bool Ignore;

    void Start()
    {
        if (Ignore)
            return;

        if (UserData.Instance.IsGerman)
        {
            var translation = Translation;
            if (!string.IsNullOrEmpty(Line2))
            {
                translation += "\n" + Line2;
            }
            if (!string.IsNullOrEmpty(Line3))
            {
                translation += "\n" + Line3;
            }
            if (!string.IsNullOrEmpty(Line4))
            {
                translation += "\n" + Line4;
            }
            if (!string.IsNullOrEmpty(Line5))
            {
                translation += "\n" + Line5;
            }

            var uiText = GetComponent<Text>();
            if (uiText != null)
            {
                uiText.text = translation;
            }

            var meshText = GetComponent<TextMesh>();
            if (meshText != null)
            {
                meshText.text = translation;
            }
        }
    }
}