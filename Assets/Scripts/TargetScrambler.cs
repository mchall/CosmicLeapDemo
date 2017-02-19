using UnityEngine;
using System.Collections;

public class TargetScrambler : MonoBehaviour
{
    AudioHelper audioHelper;
    bool triggered;

    public MissileLauncher launcher;
    public GameObject newTarget;

    public MissileLauncher launcher2;
    public GameObject newTarget2;

    public MissileLauncher launcher3;
    public GameObject newTarget3;

    public MissileLauncher launcher4;
    public GameObject newTarget4;

    public MissileLauncher launcher5;
    public GameObject newTarget5;

    void Start()
    {
        audioHelper = Camera.main.GetComponent<AudioHelper>();
    }

    void Update()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, transform.localRotation * Quaternion.Euler(0, 10, 0), Time.deltaTime * 10f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.gameObject.tag == "Player")
        {
            var renderer = GetComponent<MeshRenderer>();
            renderer.enabled = false;

            if (launcher != null)
                launcher.target = newTarget;

            if (launcher2 != null)
                launcher2.target = newTarget2;

            if (launcher3 != null)
                launcher3.target = newTarget3;

            if (launcher4 != null)
                launcher4.target = newTarget4;

            if (launcher5 != null)
                launcher5.target = newTarget5;

            StartCoroutine(PlaySound());
            triggered = true;
        }
    }

    IEnumerator PlaySound()
    {
        audioHelper.Blip();
        yield return new WaitForSeconds(0.1f);
        audioHelper.Blip();
        yield return new WaitForSeconds(0.1f);
        audioHelper.Blip();
        yield return new WaitForSeconds(0.1f);
        audioHelper.Blip();
        yield return new WaitForSeconds(0.1f);
    }
}