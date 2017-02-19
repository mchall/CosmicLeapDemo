using UnityEngine;

public class LaserBeamButton : MonoBehaviour
{
    Animator anim;

    public Planet home;
    public LaserBeam beam1;
    public LaserBeam beam2;
    public LaserBeam beam3;
    public LaserBeam beam4;
    public LaserBeam beam5;

    public float hover = 0f;

    void Start()
    {
        anim = GetComponent<Animator>();
        home.Position(transform, hover);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Obstacle")
        {
            if (beam1 != null)
                StartCoroutine(beam1.SwitchState());

            if (beam2 != null)
                StartCoroutine(beam2.SwitchState());

            if (beam3 != null)
                StartCoroutine(beam3.SwitchState());

            if (beam4 != null)
                StartCoroutine(beam4.SwitchState());

            if (beam5 != null)
                StartCoroutine(beam5.SwitchState());

            anim.Play("ButtonPushed");
        }
    }
}