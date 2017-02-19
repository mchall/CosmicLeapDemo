using UnityEngine;

public class OrbitingPlanet : MonoBehaviour
{
    public Planet other;
    public float speed = 20f;

    void FixedUpdate()
    {
        other.Orbit(transform, speed);
    }
}