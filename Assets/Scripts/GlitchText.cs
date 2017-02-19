using UnityEngine;

public class GlitchText : MonoBehaviour
{
    float val;

    void Start()
    {
        val = 0.2f;
    }

    void FixedUpdate()
    {
        var z = transform.localRotation.eulerAngles.z;
        if (z > 180)
            z = z - 360;

        if (z < -3)
            val = 0.2f;
        if (z > 3)
            val = -0.2f;
        
        transform.Rotate(new Vector3(0, 0, 1), val);
    }
}