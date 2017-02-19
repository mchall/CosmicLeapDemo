using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour
{
    public Planet home;

    Rigidbody body;
    AudioHelper audioHelper;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        audioHelper = Camera.main.GetComponent<AudioHelper>();

        if (home != null)
            home.Position(transform, 0f);
    }

    public void TakeOff()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (!child.name.StartsWith("Rocket"))
                transform.GetChild(i).gameObject.SetActive(true);
        }
        audioHelper.BlastOff();

        body.AddForce(transform.up, ForceMode.Impulse);

        GetComponent<RocketMotion>().enabled = true;
    }

    public void FinalTakeOff()
    {
        TakeOff();
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(8f);
        Application.LoadLevel("Menu");
    }
}