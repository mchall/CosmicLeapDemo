using UnityEngine;

public class Diamond : MonoBehaviour
{
    void Update()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, transform.localRotation * Quaternion.Euler(0, 10, 0), Time.deltaTime * 10f);
    }
}
