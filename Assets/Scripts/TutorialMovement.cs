using UnityEngine;

public class TutorialMovement : MonoBehaviour
{
    bool activated;

    public bool stop = true;
    public GameObject myText;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;

    void Start()
    {
        if (UserData.Instance.HasFinishedLevel("1-2"))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!activated && other.gameObject.tag == "Player")
        {
            if (stop)
                other.gameObject.GetComponent<Player>().Stop();

            text1.SetActive(false);
            text2.SetActive(false);
            text3.SetActive(false);

            myText.SetActive(true);
            activated = true;
        }
    }
}