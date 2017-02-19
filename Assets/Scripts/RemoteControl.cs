using UnityEngine;
using System.Collections;

public class RemoteControl : MonoBehaviour
{
    AudioHelper audioHelper;
    bool triggered;

    public MissileLauncher launcher;
    public MissileLauncher launcher2;
    public MissileLauncher launcher3;

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
                launcher.enabled = true;

            if (launcher2 != null)
                launcher2.enabled = true;

            if (launcher3 != null)
                launcher3.enabled = true;

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