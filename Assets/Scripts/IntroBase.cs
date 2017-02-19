using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class IntroBase : MonoBehaviour
{
    protected int index = -1;
    AudioHelper audioHelper;
    string[] currentWords;
    int currentWordIndex;

    protected abstract string IndexText();
    public abstract void LoadLevel();

    void Start()
    {
        if (MusicPlayer.Instance != null)
            MusicPlayer.Instance.PlayGameMusic();

        AspectTweak();
        audioHelper = Camera.main.GetComponent<AudioHelper>();
        UpdateText();
    }

    void AspectTweak()
    {
        if (Camera.main.aspect >= 1.6)
            Camera.main.orthographicSize = 8;
        else
            Camera.main.orthographicSize = 9;
    }

    void Update()
    {
        if (IsGamepadAttached())
        {
            transform.FindChild("ImageC").gameObject.SetActive(true);
            transform.FindChild("Enter").gameObject.SetActive(false);

            transform.FindChild("CancelBtn").FindChild("BBtn").gameObject.SetActive(true);
            transform.FindChild("CancelBtn").FindChild("Image").gameObject.SetActive(false);
        }
        else
        {
            transform.FindChild("ImageC").gameObject.SetActive(false);
            transform.FindChild("Enter").gameObject.SetActive(true);

            transform.FindChild("CancelBtn").FindChild("BBtn").gameObject.SetActive(false);
            transform.FindChild("CancelBtn").FindChild("Image").gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Back Button"))
        {
            LoadLevel();
        }
        else if (Input.GetButtonDown("B Button"))
        {
            LoadLevel();
        }
        else if (Input.GetButtonDown("A Button") || Input.GetButtonDown("Jump"))
        {
            UpdateText();
        }
        else
        {
            switch (InputHelper.Instance.CheckInput())
            {
                case InputType.Tap:
                    UpdateText();
                    break;
            }
        }
    }

    public void UpdateText()
    {
        if (currentWords != null && currentWordIndex <= currentWords.Length)
        {
            StopAllCoroutines();
            transform.FindChild("DialogText").GetComponent<Text>().text = IndexText();
            currentWordIndex = currentWords.Length + 1;
        }
        else
        {
            index++;
            if (IndexText() == "")
            {
                LoadLevel();
            }
            currentWords = IndexText().Split(new string[1] { " " }, StringSplitOptions.RemoveEmptyEntries);
            currentWordIndex = 1;
            StartCoroutine(Talk());
        }
    }

    private bool IsGamepadAttached()
    {
        var names = Input.GetJoystickNames();
        return names.Length > 0 && !string.IsNullOrEmpty(names[0]);
    }

    IEnumerator Talk()
    {
        if (currentWords != null && currentWordIndex <= currentWords.Length)
        {
            audioHelper.Talk(1);

            string currentText = "";
            for (int i = 0; i < currentWordIndex; i++)
            {
                currentText += currentWords[i] + " ";
            }

            var fullText = IndexText();
            for (int j = currentText.Length; j < fullText.Length; j++)
            {
                fullText = fullText.Remove(j, 1);
                fullText = fullText.Insert(j, " ");
            }
            transform.FindChild("DialogText").GetComponent<Text>().text = fullText;

            currentWordIndex++;

            var range = UnityEngine.Random.Range(0.1f, 0.2f);
            yield return new WaitForSeconds(range);
            StartCoroutine(Talk());
        }
    }
}