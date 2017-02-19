using UnityEngine;

public class EpilogueSpawn : MonoBehaviour
{
    GameObject cubeBoy;
    public float Delay;

    void Start()
    {
        GenerateCubeBoy();
    }

    public void Reset()
    {
        Destroy(cubeBoy);
        GenerateCubeBoy();
    }

    public void Destroy()
    {
        Destroy(cubeBoy);
    }

    private void GenerateCubeBoy()
    {
        var index = Common.Instance.Random(2, 38);

        string path = "CubeBoys/cubeBoy" + index;
        var resource = Resources.Load(path) as GameObject;
        cubeBoy = Instantiate(resource);
        cubeBoy.transform.position = transform.position;
        cubeBoy.transform.localRotation = transform.localRotation;
        cubeBoy.transform.localScale = transform.localScale;
        cubeBoy.transform.SetParent(transform.parent);

        if (index == 28)
        {
            var euler = transform.localRotation.eulerAngles;
            cubeBoy.transform.localRotation = Quaternion.Euler(euler.x, euler.y - 90, euler.z);
        }

        cubeBoy.GetComponent<Player>().enabled = false;
        cubeBoy.GetComponent<BoxCollider>().enabled = false;

        var anim1 = cubeBoy.transform.GetChild(0).GetComponent<BodyAnimator>();
        if (anim1 != null)
            anim1.AnimateLegs = false;

        var anim2 = cubeBoy.transform.GetChild(0).GetComponent<AnimalAnimator>();
        if (anim2 != null)
            anim2.enabled = false;
    }
}