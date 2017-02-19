using UnityEngine;

public class Character : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(0, 0.5f, 0);
    }
}