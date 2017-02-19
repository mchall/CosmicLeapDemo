using UnityEngine;

public class CharSelectSpawn : MonoBehaviour
{
    GameObject cubeBoy;
    public bool GenerateOnStart = true;
    public float scale = 4f;

    void Start()
    {
        if (GenerateOnStart)
            GenerateCubeBoy(UserData.Instance.GetCharacterIndex());
    }

    public void GenerateCubeBoy(int index)
    {
        if (cubeBoy != null)
            Destroy(cubeBoy);

        string path = "CubeBoys/cubeBoy" + index;
        var resource = Resources.Load(path) as GameObject;
        cubeBoy = Instantiate(resource);
        cubeBoy.transform.position = transform.position;
        cubeBoy.transform.localRotation = transform.localRotation;
        cubeBoy.transform.localScale = new Vector3(scale, scale, scale);

        if (index == 28)
        {
            cubeBoy.transform.localRotation = Quaternion.Euler(0, 340, 0);
        }

        cubeBoy.GetComponent<Player>().enabled = false;
        cubeBoy.GetComponent<BoxCollider>().enabled = false;
        cubeBoy.AddComponent<Character>();

        var anim1 = cubeBoy.transform.GetChild(0).GetComponent<BodyAnimator>();
        if (anim1 != null)
            anim1.enabled = false;

        var anim2 = cubeBoy.transform.GetChild(0).GetComponent<AnimalAnimator>();
        if (anim2 != null)
            anim2.enabled = false;
    }

    public void Destroy()
    {
        if (cubeBoy != null)
            Destroy(cubeBoy);

    }
}