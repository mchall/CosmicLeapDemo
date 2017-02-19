using UnityEngine;

public class AudioHelper : MonoBehaviour
{
    public AudioClip die1;
    public AudioClip die2;
    public AudioClip jump1;
    public AudioClip jump2;
    public AudioClip jumpbig1;
    public AudioClip jumpbig2;
    public AudioClip pellet1;
    public AudioClip shield;
    public AudioClip shield2;
    public AudioClip explode1;
    public AudioClip explode2;
    public AudioClip explode3;
    public AudioClip fire;
    public AudioClip swoosh;
    public AudioClip electricity;
    public AudioClip blastoff;
    public AudioClip hit;
    public AudioClip select;
    public AudioClip sun;
    public AudioClip teleport1;
    public AudioClip teleport2;
    public AudioClip blip;
    public AudioClip step1;
    public AudioClip step2;
    public AudioClip step3;
    public AudioClip step4;
    public AudioClip step5;
    public AudioClip beep;
    public AudioClip talk1;
    public AudioClip talk2;

    AudioSource source;
    System.Random random;

    void Start()
    {
        source = Camera.main.GetComponent<AudioSource>();
        random = new System.Random();
    }

    public void Die()
    {
        var val = random.Next(0, 2);
        if (val == 0)
            source.PlayOneShot(die1, UserData.Instance.SoundVolume);
        else
            source.PlayOneShot(die2, UserData.Instance.SoundVolume);
    }

    public void Jump()
    {
        var val = random.Next(0, 2);
        if (val == 0)
            source.PlayOneShot(jump1, UserData.Instance.SoundVolume);
        else
            source.PlayOneShot(jump2, UserData.Instance.SoundVolume);
    }

    public void BigJump()
    {
        var val = random.Next(0, 2);
        if (val == 0)
            source.PlayOneShot(jumpbig1, UserData.Instance.SoundVolume);
        else
            source.PlayOneShot(jumpbig2, UserData.Instance.SoundVolume);
    }

    public AudioClip ExplodeClip()
    {
        var val = random.Next(0, 3);
        if (val == 0)
            return explode1;
        else if (val == 1)
            return explode2;
        else
            return explode3;
    }

    public void PlayExplosion()
    {
        var clip = ExplodeClip();
        source.PlayOneShot(clip, UserData.Instance.SoundVolume);
    }

    public void Pellet()
    {
        source.PlayOneShot(pellet1, UserData.Instance.SoundVolume);
    }

    public void ShieldOn()
    {
        source.PlayOneShot(shield, UserData.Instance.SoundVolume);
    }

    public void ShieldOff()
    {
        source.PlayOneShot(shield2, UserData.Instance.SoundVolume);
    }

    public void BlastOff()
    {
        source.PlayOneShot(blastoff, UserData.Instance.SoundVolume);
    }

    public void ObstacleHit(Vector3 point)
    {
        AudioSource.PlayClipAtPoint(hit, point, UserData.Instance.SoundVolume);
    }

    public void Select()
    {
        source.PlayOneShot(select, UserData.Instance.SoundVolume);
    }

    public void Sun()
    {
        source.PlayOneShot(sun, UserData.Instance.SoundVolume);
    }

    public void Teleport()
    {
        var val = random.Next(0, 2);
        if (val == 0)
            source.PlayOneShot(teleport1, UserData.Instance.SoundVolume);
        else
            source.PlayOneShot(teleport2, UserData.Instance.SoundVolume);
    }

    public void Blip()
    {
        source.PlayOneShot(blip, UserData.Instance.SoundVolume);
    }

    public void Step()
    {
        var stepVolume = 0.3f;

        var val = random.Next(0, 5);
        if (val == 0)
            source.PlayOneShot(step1, UserData.Instance.SoundVolume * stepVolume);
        else if (val == 1)
            source.PlayOneShot(step2, UserData.Instance.SoundVolume * stepVolume);
        else if (val == 2)
            source.PlayOneShot(step3, UserData.Instance.SoundVolume * stepVolume);
        else if (val == 3)
            source.PlayOneShot(step4, UserData.Instance.SoundVolume * stepVolume);
        else
            source.PlayOneShot(step5, UserData.Instance.SoundVolume * stepVolume);
    }

    public void Beep()
    {
        source.PlayOneShot(beep, UserData.Instance.SoundVolume);
    }

    public void Talk(int val)
    {
        if (val == 0)
            source.PlayOneShot(talk1, UserData.Instance.SoundVolume / 4);
        else
            source.PlayOneShot(talk2, UserData.Instance.SoundVolume / 4);
    }
}