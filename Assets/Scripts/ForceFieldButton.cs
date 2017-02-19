using UnityEngine;

public class ForceFieldButton : MonoBehaviour
{
    Animator anim;

    public Planet home;
    public ForceField forceField;
    public float hover = 0.1f;

    void Start()
    {
        anim = GetComponent<Animator>();
        home.Position(transform, hover);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Obstacle")
        {
            StartCoroutine(forceField.SwitchState());
            anim.Play("ButtonPushed");
        }
    }
}