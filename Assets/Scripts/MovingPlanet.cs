using UnityEngine;

public class MovingPlanet : MonoBehaviour
{
    public float lerpTime = 25f;
    private float currentLerpTime = 0;
    private float perc = 1;

    private Vector3 startPos;
    public Vector3 endPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        currentLerpTime += Time.deltaTime * 5f;
        perc = currentLerpTime / lerpTime;
        if (perc >= 1.2f)
        {
            Vector3 temp = startPos;
            startPos = endPos;
            endPos = temp;
            currentLerpTime = 0;
            return;
        }

        transform.position = Vector3.Lerp(startPos, endPos, perc);
    }
}