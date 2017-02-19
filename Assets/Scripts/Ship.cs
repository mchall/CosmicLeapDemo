using UnityEngine;

public class Ship : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(0.5f, 0, 0f);
    }
}