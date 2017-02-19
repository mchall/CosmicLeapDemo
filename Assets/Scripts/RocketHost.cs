using UnityEngine;

public class RocketHost : MonoBehaviour
{
    void Start()
    {
        var index = UserData.Instance.GetShipIndex();

        HideDefaultShip();
        transform.FindChild("Rocket" + index).gameObject.SetActive(true);
    }

    void HideDefaultShip()
    {
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }
}