using UnityEngine;

public class ShipSelectSpawn : MonoBehaviour
{
    GameObject ship;
    public bool GenerateOnStart = true;
    public float scale = 0.25f;

    void Start()
    {
        if (GenerateOnStart)
            GenerateShip(UserData.Instance.GetCharacterIndex());
    }

    public void GenerateShip(int index)
    {
        if (ship != null)
            Destroy(ship);

        string path = "Rockets/Rocket" + index;
        var resource = Resources.Load(path) as GameObject;
        ship = Instantiate(resource);
        ship.transform.position = transform.position;
        ship.transform.localRotation = transform.localRotation;
        ship.transform.localScale = new Vector3(scale, scale, scale);

        ship.AddComponent<Ship>();
    }

    public void Destroy()
    {
        if (ship != null)
            Destroy(ship);
    }
}