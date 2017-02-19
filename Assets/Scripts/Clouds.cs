using UnityEngine;

public class Clouds : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(0, 0.1f, 0);
    }
}