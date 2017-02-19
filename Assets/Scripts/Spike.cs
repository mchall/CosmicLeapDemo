using UnityEngine;

public class Spike : MonoBehaviour
{
    public Planet home;

    void Start()
    {
        home.Position(transform, -0.5f);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            var movingObstacle = col.collider.GetComponent<MovingObstacle>();
            if (movingObstacle != null)
            {
                movingObstacle.Die();
            }
        }
    }
}