using UnityEngine;

public class SpaceDebris : MonoBehaviour
{
    void Start()
    {
    }

    void FixedUpdate()
    {
        transform.Rotate(0, 0.5f, 0);
    }
}