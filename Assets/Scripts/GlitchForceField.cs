using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ForceField))]
public class GlitchForceField : MonoBehaviour
{
    ForceField forceField;

    public float onTime = 4f;
    public float offTime = 4f;

    void Start()
    {
        forceField = GetComponent<ForceField>();
        StartCoroutine(Glitch());
    }

    IEnumerator Glitch()
    {
        if (enabled)
        {
            yield return new WaitForSeconds(onTime);
            StartCoroutine(forceField.SwitchState());
            yield return new WaitForSeconds(offTime);
            StartCoroutine(forceField.SwitchState());
            StartCoroutine(Glitch());
        }
    }
}