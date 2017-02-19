using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LaserBeam : MonoBehaviour
{
    public float laserWidth = 1.0f;
    public float noise = 1.0f;
    public float maxLength = 50.0f;
    public Color color = Color.red;

    LineRenderer lineRenderer;
    AudioHelper audioHelper;
    AudioSource audioSource;
    int length;
    float vertexLength;
    Vector3[] position;
    Transform host;
    Vector3 offset;

    GameObject red;
    GameObject green;

    public GameObject end;

    void Start()
    {
        audioHelper = Camera.main.GetComponent<AudioHelper>();

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetWidth(laserWidth, laserWidth);
        host = transform;
        offset = new Vector3(0, 0, 0);

        green = transform.GetChild(0).gameObject;
        red = transform.GetChild(1).gameObject;

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = UserData.Instance.SoundVolume;
        audioSource.clip = audioHelper.electricity;
        if (end != null)
        {
            audioSource.Play();
        }
        else
        {
            green.SetActive(false);
        }
    }

    void Update()
    {
        if (enabled)
        {
            if (end == null)
            {
                audioSource.Stop();
                lineRenderer.enabled = enabled = false;
                green.SetActive(false);
                red.SetActive(false);
                return;
            }
            RenderLaser();
        }
    }

    public IEnumerator SwitchState()
    {
        if (end != null)
        {
            lineRenderer.enabled = enabled = !enabled;
            yield return new WaitForSeconds(0.1f);
            lineRenderer.enabled = enabled = !enabled;
            yield return new WaitForSeconds(0.1f);
            lineRenderer.enabled = enabled = !enabled;

            if (enabled)
                audioSource.Play();
            else
                audioSource.Stop();

            green.SetActive(enabled);
            red.SetActive(!enabled);
        }
    }

    void RenderLaser()
    {
        UpdateLength();

        var directionVector = (end.transform.position - host.position).normalized;

        lineRenderer.SetColors(color, color);
        for (int i = 0; i < length; i++)
        {
            offset.x = host.position.x + i * directionVector.x + Random.Range(-noise, noise);
            offset.y = i * directionVector.y + Random.Range(-noise, noise) + host.position.y;
            position[i] = offset;
            position[0] = host.position;

            lineRenderer.SetPosition(i, position[i]);
        }
    }

    void UpdateLength()
    {
        RaycastHit hit;
        Physics.Raycast(host.position, end.transform.position - host.position, out hit);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                hit.collider.GetComponent<Player>().Die();
            }

            if (hit.collider.tag == "Obstacle")
            {
                var movingObstacle = hit.collider.GetComponent<MovingObstacle>();
                if (movingObstacle != null)
                {
                    movingObstacle.Die();
                }
                else
                {
                    audioHelper.ObstacleHit(hit.collider.transform.position);
                    Destroy(hit.collider.gameObject);
                }
            }

            length = (int)Mathf.Round(hit.distance) + 1;

            position = new Vector3[length];
            lineRenderer.SetVertexCount(length);
            return;
        }

        length = (int)maxLength;
        position = new Vector3[length];
        lineRenderer.SetVertexCount(length);
    }
}