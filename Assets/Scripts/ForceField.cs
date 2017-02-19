using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(SphereCollider))]
public class ForceField : MonoBehaviour
{
    bool state;
    MeshRenderer mesh;
    SphereCollider sphere;
    AudioHelper audioHelper;

    void Start()
    {
        audioHelper = Camera.main.GetComponent<AudioHelper>();
        state = true;
        mesh = GetComponent<MeshRenderer>();
        sphere = GetComponent<SphereCollider>();
    }

    public IEnumerator SwitchState()
    {
        state = !state;

        if (state)
            audioHelper.ShieldOn();
        else
            audioHelper.ShieldOff();

        sphere.enabled = state;

        mesh.enabled = state;
        yield return new WaitForSeconds(0.2f);
        mesh.enabled = !state;
        yield return new WaitForSeconds(0.2f);
        mesh.enabled = state;
    }

    void OnTriggerEnter(Collider other)
    {
        if (state && other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().Die();
        }
    }
}