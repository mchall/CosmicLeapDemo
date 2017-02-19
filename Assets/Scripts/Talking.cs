using UnityEngine;
using System.Collections;

public class Talking : MonoBehaviour
{
    public float time = 0.15f;

    GameObject num1;
    GameObject num2;

    void Start()
    {
        num1 = transform.FindChild("default").gameObject;
        num2 = transform.FindChild("default2").gameObject;

        StartCoroutine(Talk());
    }

    IEnumerator Talk()
    {
        yield return new WaitForSeconds(time);
        if (num1.activeSelf)
        {
            num1.SetActive(false);
            num2.SetActive(true);
        }
        else
        {
            num1.SetActive(true);
            num2.SetActive(false);
        }
        StartCoroutine(Talk());
    }
}