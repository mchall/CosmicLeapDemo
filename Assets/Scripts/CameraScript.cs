using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Vector3 pos;

    public bool GameFinished { get; set; }

    void Start()
    {
        pos = transform.position;

        Camera.main.GetComponent<GlitchEffect>().enabled = UserData.Instance.GlitchEffect;
        Camera.main.GetComponent<OLDTVTube>().enabled = UserData.Instance.Quality && UserData.Instance.GlitchEffect;
    }

    void OnPostRender()
    {
        if (GameFinished)
        {
            if (UserData.Instance.FinishedLevel)
            {
                transform.position = pos;
                transform.localRotation = Quaternion.Euler(25, -45, 0);
            }
            GameFinished = false;
        }
    }
}