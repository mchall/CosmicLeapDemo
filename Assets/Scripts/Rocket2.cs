using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Rocket2 : MonoBehaviour
{
    Rigidbody body;

    public int UnlockIndex;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.AddForce(transform.up * 3f, ForceMode.Impulse);

        if (UserData.Instance.UnlockedCharacters.Contains(UnlockIndex))
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(PlayWarning());
        }
    }

    IEnumerator PlayWarning()
    {
        var canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            canvas.transform.FindChild("Detected").gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            canvas.transform.FindChild("Detected").gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            canvas.transform.FindChild("Detected").gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            canvas.transform.FindChild("Detected").gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            canvas.transform.FindChild("Detected").gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            canvas.transform.FindChild("Detected").gameObject.SetActive(false);
        }
    }
}