using UnityEngine;

public class Sign : MonoBehaviour
{
    public Planet home;

    void Start()
    {
        home.Position(transform, -0.1f);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0.3f);
    }
}