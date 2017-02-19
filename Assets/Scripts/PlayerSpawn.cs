using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public bool Tutorial;
    public float direction = 1f;
    public bool IsClone;
    public int IndexOverride;

    public GameObject cubeBoy;

    void Start()
    {
        string path = "CubeBoys/cubeBoy" + (IndexOverride > 0 ? IndexOverride : (IsClone ? UserData.Instance.GetRandomCharacterIndex() : UserData.Instance.GetCharacterIndex()));
        var resource = Resources.Load(path) as GameObject;

        cubeBoy = Instantiate(resource);
        cubeBoy.transform.position = transform.position;

        var playerScript = cubeBoy.GetComponent<Player>();
        if (Tutorial && !UserData.Instance.HasFinishedLevel("1-1"))
        {
            playerScript.enabled = false;
            cubeBoy.AddComponent<Tutorial>();
        }
        playerScript.direction = direction;
    }
}