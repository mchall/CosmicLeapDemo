using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Rocket3 : MonoBehaviour
{
    public int UnlockIndex;

    void Start()
    {
        if (UserData.Instance.UnlockedShips.Contains(UnlockIndex))
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