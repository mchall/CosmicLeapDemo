using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Sun : MonoBehaviour
{
    public float burstTime = 3f;

    AudioHelper audioHelper;

    void Start()
    {
        audioHelper = Camera.main.GetComponent<AudioHelper>();
        StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(burstTime);
        audioHelper.Sun();
        StartCoroutine(PlaySound());
        StartCoroutine(Vibrate(0.5f, 1f));
    }

    IEnumerator Vibrate(float strength, float time)
    {
        if (UserData.Instance.Vibrate)
        {
            var names = Input.GetJoystickNames();
            if (!UserData.Instance.FinishedLevel && names.Length > 0 && !string.IsNullOrEmpty(names[0]))
            {
                GamePad.SetVibration(PlayerIndex.One, strength, strength);
                yield return new WaitForSeconds(time);
                GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
            }
        }
    }
}